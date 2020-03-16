﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LghtPuzzleScript : PuzzlePartScript
{
    [Header("Light variables")]
    public LineRenderer lineRend;

    private RaycastHit lightHit;
    [Range(1,50)]public float rayLength = 15;

    private GameObject lastHit;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Physics.Raycast(transform.position, transform.forward, out lightHit, rayLength, 1) && lastHit == null)
        {
            PuzzlePartScript hitScript = lightHit.transform.gameObject.GetComponent<PuzzlePartScript>();
            //Debug.DrawRay(transform.position, transform.forward * lightHit.distance, Color.red);
            if (hitScript != null && lightHit.transform.tag != "LightBlocker")
            {
                hitScript.Activate(puzzlePartColorInt, true, gameObject);
                lineRend.SetPosition(1, transform.InverseTransformPoint(lightHit.point));
            }
            lastHit = lightHit.transform.gameObject;
        }
        else if (Physics.Raycast(transform.position, transform.forward, out lightHit, rayLength, 1) && lastHit != null)
        {
            if (lightHit.transform.gameObject != lastHit)
            {
                if (lastHit.GetComponent<PuzzlePartScript>() && lastHit.tag != "LightBlocker")
                {
                    lastHit.GetComponent<PuzzlePartScript>().Activate(puzzlePartColorInt, false, gameObject);
                }
                lastHit = null;
            }
            Debug.DrawRay(transform.position, transform.forward * lightHit.distance, Color.blue);
            lineRend.SetPosition(1, transform.InverseTransformPoint(lightHit.point));
        }
        else
        {
            if (lastHit != null)
            {
                if (lastHit.GetComponent<PuzzlePartScript>() && lastHit.tag != "LightBlocker")
                {
                    lastHit.GetComponent<PuzzlePartScript>().Activate(puzzlePartColorInt, false, gameObject);
                }
                lastHit = null;
            }
                
            Debug.DrawRay(transform.position, transform.forward * rayLength, Color.yellow);
            lineRend.SetPosition(1, transform.InverseTransformPoint(transform.position + transform.forward*rayLength));

        }
    }

    public override void Activate(int activateColor, bool isActivated, GameObject source)
    {
        
    }

    /// <summary>
    /// Updates the color of this emitter
    /// </summary>
    /// <param name="colorNum">The color to update to</param>
    public override void SetColor(int colorNum)
    {
        targetMat = puzzlePartMats[colorNum];

        lineRend.material = puzzlePartMats[colorNum];

        // Cast the enum to an int
        puzzlePartColorInt = colorNum;
    }
}
