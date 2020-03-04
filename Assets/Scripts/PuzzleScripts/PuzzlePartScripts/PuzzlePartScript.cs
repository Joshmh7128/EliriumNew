using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PuzzlePartScript : MonoBehaviour
{
    /// <summary>
    /// Determines what puzzle mode this puzzlePart is in. Lerp moves an object through transform.position, Animation sets bools in an animator, and Conduit passes the signal to one or more other puzzleParts.
    /// </summary>
    public enum puzzlePartMode { Lerp, Anim, Cond }
    /// <summary>
    /// The puzzlePartMode being used by this puzzlePart
    /// </summary>
    [Tooltip("The puzzle mode that this puzzlePart uses. ")] public puzzlePartMode mode;
    /// <summary>
    /// The pedestal that activates this puzzlePart
    /// </summary>
    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject source;

    [ConditionalField("mode", false, puzzlePartMode.Lerp)] public LerpPuzzleScript lerpScript;
    [ConditionalField("mode", false, puzzlePartMode.Anim)] public AnimPuzzleScript animScript;
    [ConditionalField("mode", false, puzzlePartMode.Cond)] public CondPuzzleScript condScript;

    public void Activate(int activateColor, bool isActivated)
    {
        if (mode == puzzlePartMode.Lerp)
        {
            lerpScript.Activate(activateColor, isActivated);
        }
        else if (mode == puzzlePartMode.Anim)
        {
            animScript.Activate(activateColor, isActivated);
        }
        else if (mode == puzzlePartMode.Cond)
        {
            condScript.Activate(activateColor, isActivated);
        }
    }
}
