using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitterScript : MonoBehaviour
{

    public List<Material> orbMats;
    public enum lightColorList { None, Red, Green, Blue, Orange }; //what color is our orb?
    public lightColorList lightColor; //put it in the editor
    public int lightColorInt;
    public MeshRenderer pedRend;
    private Material targetMat;

    private RaycastHit lightHit;
    [Range(1,50)]public float rayLength = 15;

    private GameObject lastHit;

    // Start is called before the first frame update
    void Start()
    {
        SetColor((int)lightColor);
    }

    // Update is called once per frame
    void Update()
    {
        pedRend.material.Lerp(pedRend.material, targetMat, 0.2f);

        if (Physics.Raycast(transform.position, transform.forward, out lightHit, rayLength, 1) && lastHit == null)
        {
            PuzzlePartScript hitScript = lightHit.transform.gameObject.GetComponent<PuzzlePartScript>();
            //Debug.DrawRay(transform.position, transform.forward * lightHit.distance, Color.red);
            if (hitScript != null)
            {
                hitScript.Activate(lightColorInt, true);
            }
            lastHit = lightHit.transform.gameObject;
        }
        else if (Physics.Raycast(transform.position, transform.forward, out lightHit, rayLength, 1) && lastHit != null)
        {
            if (lightHit.transform.gameObject != lastHit)
            {
                if (lastHit.GetComponent<PuzzlePartScript>())
                {
                    lastHit.GetComponent<PuzzlePartScript>().Activate(lightColorInt, false);
                }
                lastHit = null;
            }
            Debug.DrawRay(transform.position, transform.forward * lightHit.distance, Color.blue);
        }
        else
        {
            if (lastHit != null)
            {
                if (lastHit.GetComponent<PuzzlePartScript>())
                {
                    lastHit.GetComponent<PuzzlePartScript>().Activate(lightColorInt, false);
                }
                lastHit = null;
            }
                
            Debug.DrawRay(transform.position, transform.forward * rayLength, Color.yellow);
            
        }
    }

    /// <summary>
    /// Updates the color of this emitter
    /// </summary>
    /// <param name="colorNum">The color to update to</param>
    public void SetColor(int colorNum)
    {
        targetMat = orbMats[colorNum];

        // Cast the enum to an int
        lightColorInt = colorNum;
    }
}
