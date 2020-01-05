using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePartScript : MonoBehaviour
{
    [Tooltip("List of animation names that this puzzlePart can do, based on what color orb is used")] public List<Vector3> actions;
    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject pedestal;
    private Vector3 target;

    private void Start()
    {
        target = transform.position;
    }

    public void Activate(int OrbColor)
    {
        if (actions[OrbColor] != null)
        {
            //Play animation with key OrbColor from dict actions
            Debug.Log("playAnimation" + OrbColor);
            //animator.SetTrigger(actions[OrbColor]);
            target = actions[OrbColor];
        }
        else
        {
            Debug.Log("No animation " + OrbColor);
        }
    }

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target, 0.01f);
    }
}
