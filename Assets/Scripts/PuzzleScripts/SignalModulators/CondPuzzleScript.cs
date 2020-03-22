using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondPuzzleScript : PuzzlePartScript
{
    [Header("Conduit Variables")]
    public bool collecting;
    [Tooltip("The list of puzzle objects that this Conduit activates")] public List<PuzzlePartScript> puzzleParts;

    public override void Activate(int activateColor, bool isActivated, GameObject source)
    {
        if (puzzlePartMats.Count > 0)
        {
            foreach (PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(activateColor, isActivated, gameObject);
            }

            if (!collecting)
            {
                if (isActivated)
                {
                    SetColor(activateColor);
                }
                else
                {
                    SetColor(0);
                }
            }
        }

    }
}
