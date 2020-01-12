using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PuzzlePartScript : MonoBehaviour
{
    public enum puzzlePartMode { Lerp, Anim}
    [Tooltip("The puzzle mode that this puzzlePart uses. ")]public puzzlePartMode mode;
    public enum orbColorList { None, Red, Green, Blue, Orange }; //what color is our orb?

    [HideInInspector]public List<Vector3> lerpActions;

    [ConditionalField("mode", false, puzzlePartMode.Lerp)] public Vector3 whiteAction, redAction, greenAction, blueAction, orangeAction;

    [ConditionalField("mode", false, puzzlePartMode.Anim)] public Animator animator;


    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject pedestal;
    private Vector3 target;

    private void Start()
    {
        target = transform.position;
        if (mode == puzzlePartMode.Lerp)
        {
            lerpActions.Add(whiteAction);
            lerpActions.Add(redAction);
            lerpActions.Add(greenAction);
            lerpActions.Add(blueAction);
            lerpActions.Add(orangeAction);
        }

    }

    public void Activate(int OrbColor)
    {
        if (mode == puzzlePartMode.Lerp)
        {
            if (lerpActions[OrbColor] != null)
            {
                //Play animation with key OrbColor from dict actions
                Debug.Log("Move to position" + OrbColor);
                //animator.SetTrigger(actions[OrbColor]);
                if (lerpActions[OrbColor] == new Vector3(-999, -999, -999))
                {
                    target = transform.position;
                }
                else
                {
                    StartCoroutine(Move(OrbColor));
                }

            }
        }
        else if (mode == puzzlePartMode.Anim)
        {
            animator.SetTrigger(OrbColor);
        }
        else
        {
            Debug.Log("invalid puzzlePartMode on part " + gameObject.name);
        }
            
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, 0.01f);
    }

    IEnumerator Move(int OrbColor)
    {
        yield return new WaitForSeconds(1);
        target = lerpActions[OrbColor];
    }
}
