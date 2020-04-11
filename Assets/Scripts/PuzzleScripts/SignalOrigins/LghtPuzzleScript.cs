using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LghtPuzzleScript : PuzzlePartScript
{
    [HideInInspector] public LineRenderer lineRend;

    private RaycastHit lightHit;
    [Header("Light variables")]
    public bool isLens = false;
    private bool isActive = true;
    [Range(1,50)]public float rayLength = 15;

    private GameObject lastHit;

    protected override void Start()
    {
        lineRend = gameObject.GetComponent<LineRenderer>();

        base.Start();
        if (isLens)
        {
            isActive = false;
            lineRend.enabled = false;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isActive)
        {
            if (Physics.Raycast(transform.position, transform.forward, out lightHit, rayLength, 1) && lastHit == null)
            {
                PuzzlePartScript hitScript = lightHit.transform.gameObject.GetComponent<PuzzlePartScript>();
                //Debug.DrawRay(transform.position, transform.forward * lightHit.distance, Color.red);
                if (hitScript != null && lightHit.transform.tag != "LightBlocker")
                {
                    hitScript.Activate(PuzzlePartColorInt, true, gameObject);
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
                        lastHit.GetComponent<PuzzlePartScript>().Activate(PuzzlePartColorInt, false, gameObject);
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
                        lastHit.GetComponent<PuzzlePartScript>().Activate(PuzzlePartColorInt, false, gameObject);
                    }
                    lastHit = null;
                }

                Debug.DrawRay(transform.position, transform.forward * rayLength, Color.yellow);
                lineRend.SetPosition(1, transform.InverseTransformPoint(transform.position + transform.forward * rayLength));

            }
        }        
    }

    /// <summary>
    /// Watch this space.
    /// </summary>
    /// <param name="activateColor">The color of the signal entering the puzzlePart.</param>
    /// <param name="isActivated">The boolean value of the signal. True = signal starts, False = signal stops.</param>
    /// <param name="source">The GameObject sending the signal.</param>
    public override void Activate(int activateColor, bool isActivated, GameObject source)
    {
        if (isLens)
        {
            if (isActivated)
            {
                lineRend.enabled = true;
                isActive = true;
            }
            else
            {
                lineRend.enabled = false;
                isActive = false;
            }
        }
    }

    /// <summary>
    /// Updates the color of this emitter
    /// </summary>
    /// <param name="colorNum">The color to update to</param>
    public override void SetColor(int colorNum)
    {
        base.SetColor(colorNum);

        lineRend.material = puzzlePartMats[colorNum];
    }
}
