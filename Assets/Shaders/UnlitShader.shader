// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/UnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Object Color", Color) = (1, 1, 1, 1)
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineWidth("Outline Width", Range(0.0, 1.0)) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" }
			LOD 100
			

			Pass
			{
				ZWrite Off
					CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				fixed4 _OutlineColor;
				fixed _OutlineWidth;
				fixed4 _Color;

			struct v2f {
				float4 pos : SV_POSITION;
				fixed3 color : COLOR0;
			};

			
			v2f vert(appdata_base v)
			{
				v2f o;
				float4 modVertex = v.vertex;
				modVertex.xyz += _OutlineWidth * v.vertex;
				o.pos = UnityObjectToClipPos(modVertex);
				o.color = _OutlineColor;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return fixed4(i.color, 1);
			}
			ENDCG
		}

		Pass
		{
			ZWrite On
			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}

			Lighting Off

			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
			}

			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
    }
			Fallback "Diffuse"
}
