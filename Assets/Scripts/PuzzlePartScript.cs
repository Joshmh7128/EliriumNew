using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePartScript : MonoBehaviour
{
    [Tooltip("List of animation names that this puzzlePart can do, based on what color orb is used")] public List<string> actions;
    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject pedestal;
    [Tooltip("The animator attached to this puzzlePart")] public Animator animator;

    public void Activate(int OrbColor)
    {
        if (actions[OrbColor] != null)
        {
            //Play animation with key OrbColor from dict actions
            Debug.Log("playAnimation" + OrbColor);
            animator.SetTrigger(actions[OrbColor]);
        }
        else
        {
            Debug.Log("No animation " + OrbColor);
        }
    }
}
