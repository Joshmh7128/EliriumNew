using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishDetectionScript : MonoBehaviour
{
    public PuzzleCheckPointScript PuzzleCheckPointScript;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Platform"))
        PuzzleCheckPointScript.ResetPos();
    }
}
