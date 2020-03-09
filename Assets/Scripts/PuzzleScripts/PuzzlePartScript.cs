using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PuzzlePartScript : MonoBehaviour
{
    /// <summary>
    /// Determines what puzzle mode this puzzlePart is in.
    /// - Orb holds a color and activates Trig puzzleParts
    /// - Trig sends a signal when an orb enters a trigger
    /// - Lght sends a ray of light that activates anything it hits
    /// - Lerp moves an object through transform.position
    /// - Animation sets bools in an animator
    /// - Conduit passes the signal to one or more other puzzleParts
    /// - Collector waits for multiple signals and then acts like a conduit.
    /// </summary>
    public enum puzzlePartMode { _Orb, Trig, Lght, Lerp, Anim, Cond, Coll }
    /// <summary>
    /// The puzzlePartMode being used by this puzzlePart
    /// </summary>
    [Tooltip("The puzzle mode that this puzzlePart uses. ")] public puzzlePartMode mode;
    /// <summary>
    /// The pedestal that activates this puzzlePart
    /// </summary>
    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject source;

    [ConditionalField("mode", false, puzzlePartMode._Orb)] public Orb_PuzzleScript _orbScript;
    [ConditionalField("mode", false, puzzlePartMode.Trig)] public TrigPuzzleScript TrigScript;
    [ConditionalField("mode", false, puzzlePartMode.Lght)] public LghtPuzzleScript lghtScript;
    [ConditionalField("mode", false, puzzlePartMode.Lerp)] public LerpPuzzleScript lerpScript;
    [ConditionalField("mode", false, puzzlePartMode.Anim)] public AnimPuzzleScript animScript;
    [ConditionalField("mode", false, puzzlePartMode.Cond)] public CondPuzzleScript condScript;
    [ConditionalField("mode", false, puzzlePartMode.Coll)] public CollPuzzleScript collScript;

    public void Activate(int activateColor, bool isActivated, GameObject source)
    {
        if (mode == puzzlePartMode._Orb)
        {
            _orbScript.Activate(activateColor, isActivated);
        }
        else if (mode == puzzlePartMode.Trig)
        {
            TrigScript.Activate(activateColor, isActivated);
        }
        else if (mode == puzzlePartMode.Lght)
        {
            lghtScript.Activate(activateColor, isActivated);
        }
        else if (mode == puzzlePartMode.Lerp)
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
        else if (mode == puzzlePartMode.Coll)
        {
            collScript.Activate(activateColor, isActivated, source);
        }
    }

    public int getColor()
    {
        if (mode == puzzlePartMode._Orb)
        {
            return _orbScript.orbColorInt;
        }
        else if (mode == puzzlePartMode.Trig)
        {
            return TrigScript.pedColorInt;
        }
        else if (mode == puzzlePartMode.Lght)
        {
            return lghtScript.lightColorInt;
        }
        else if (mode == puzzlePartMode.Lerp)
        {
            return -1;
        }
        else if (mode == puzzlePartMode.Anim)
        {
            return -1;
        }
        else if (mode == puzzlePartMode.Cond)
        {
            return -1;
        }
        else if (mode == puzzlePartMode.Coll)
        {
            return -1;
        }
        else
        {
            return -1;
        }
    }
}
