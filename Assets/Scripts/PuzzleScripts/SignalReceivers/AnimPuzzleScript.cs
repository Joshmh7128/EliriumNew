using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPuzzleScript : MonoBehaviour
{
    /// <summary>
    /// The animator attached to this puzzlePart
    /// </summary>
    public Animator animator;
    /// <summary>
    /// The list of booleans in the animator, used to trigger animations
    /// </summary>
    [HideInInspector] public List<string> animBoolList;

    // Start is called before the first frame update
    void Start()
    {
        // Adds the animation bools to the list for internal use
        animBoolList.Add("white");
        animBoolList.Add("red");
        animBoolList.Add("green");
        animBoolList.Add("blue");
        animBoolList.Add("purple");
    }

    public void Activate(int activateColor, bool isActivated)
    {
        if (isActivated) // Using animation system and ball entering pedestal
        {
            // Passes the information to the animator attached to this puzzlePart
            animator.SetBool(animBoolList[activateColor], true);
        }
        else if (!isActivated) // Using animation system and ball leaving pedestal
        {
            // Passes teh information to the animator attached to this puzzlePart
            animator.SetBool(animBoolList[activateColor], false);
        }
    }
}
