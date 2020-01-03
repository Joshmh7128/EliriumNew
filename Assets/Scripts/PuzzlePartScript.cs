using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePartScript : MonoBehaviour
{
    [Tooltip("List of animations that this puzzlePart can do, based on what color orb is used")] public List<Animation> actions;
    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject pedestal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(int OrbColor)
    {
        if (actions[OrbColor] != null)
        {
            //Play animation with key OrbColor from dict actions
            Debug.Log("playAnimation" + OrbColor);
        }
        else
        {
            Debug.Log("No animation " + OrbColor);
        }
    }
}
