using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform holdPointGoto;
    
    public void LateUpdate()
    {
      /* if (holdPointGoto != null)
       transform.position = Vector3.Lerp(transform.position, holdPointGoto.position, Time.deltaTime * 50f);
       transform.rotation = Quaternion.Lerp(transform.rotation, holdPointGoto.rotation, Time.deltaTime * 1f);*/
    }
}
