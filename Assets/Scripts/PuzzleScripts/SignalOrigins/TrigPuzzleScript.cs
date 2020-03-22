using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigPuzzleScript : PuzzlePartScript
{
    [Header("Trigger Variables")]
    [Tooltip("The orb detection trigger on this pedestal.")] public Collider orbDetect;
    [Tooltip("The list of puzzle objects that this pedestal activates")] public List<PuzzlePartScript> puzzleParts;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Orb_PuzzleScript>())
        {
            //Debug.Log("Orb in");
            foreach (PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(other.GetComponent<Orb_PuzzleScript>().puzzlePartColorInt, true, gameObject);
            }
            SetColor(other.GetComponent<Orb_PuzzleScript>().puzzlePartColorInt);
        }
        else
        {
            //Debug.Log("Not orb!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Orb_PuzzleScript>())
        {
            //Debug.Log("Orb out");
            foreach(PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(other.GetComponent<Orb_PuzzleScript>().puzzlePartColorInt, false, gameObject);
            }

            SetColor(0);
        }
    }

    public override void Activate(int activateColor, bool isActivated, GameObject source)
    {
        if (isActivated)
        {
            SetColor(activateColor);
        }
        else
        {
            SetColor((int)puzzlePartColor);
        }

    }
}
