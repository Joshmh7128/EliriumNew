using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PuzzlePartScript : MonoBehaviour
{
    public enum puzzlePartMode { Lerp, Anim }
    [Tooltip("The puzzle mode that this puzzlePart uses. ")] public puzzlePartMode mode;
    public enum orbColorList { None, Red, Green, Blue, Orange }; //what color is our orb?

    #region LerpVariables
    // ~~~ Inspector Variables ~~~
    [HideInInspector] public List<Vector3> lerpActions;
    [ConditionalField("mode", false, puzzlePartMode.Lerp)] [Range(0, 5)] public int lerpLagTime = 1;
    [ConditionalField("mode", false, puzzlePartMode.Lerp)] [Range(0, 10)] public float lerpSpeed = 0.05f;
    [ConditionalField("mode", false, puzzlePartMode.Lerp)] public Vector3 defaultPos, redAction, greenAction, blueAction, orangeAction;

    // ~~~ Internal Variables ~~~
    private float lerpStartTime;
    private float lerpLength;
    private int colorExternal;
    private int colorInternal;
    private Vector3 target;
    private Vector3 startPos;
    private bool isMoving;
    #endregion

    #region AnimVariables
    [ConditionalField("mode", false, puzzlePartMode.Anim)] public Animator animator;
    [HideInInspector]public List<string> triggerList;
    #endregion

    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject pedestal;
    

    private void Start()
    {
        target = transform.position;
        #region LerpVariables
        if (mode == puzzlePartMode.Lerp)
        {
            lerpActions.Add(defaultPos);
            lerpActions.Add(redAction);
            lerpActions.Add(greenAction);
            lerpActions.Add(blueAction);
            lerpActions.Add(orangeAction);

            lerpStartTime = Time.time;
            lerpLength = 0;
            startPos = transform.position;
            lerpActions[0] = startPos;
            colorExternal = 0; // Indicates that there is nothing in the pedestal
            isMoving = false;
        }
        #endregion
        else if (mode == puzzlePartMode.Anim)
        {
            triggerList.Add("white");
            triggerList.Add("red");
            triggerList.Add("green");
            triggerList.Add("blue");
            triggerList.Add("orange");
        }

    }

    public void Activate(int OrbColor, bool inPedestal)
    {
        if (mode == puzzlePartMode.Lerp) // ~~~ Using Lerp System ~~~
        {
            StartCoroutine(Move(OrbColor, inPedestal, lerpLagTime));
        }




        else if (mode == puzzlePartMode.Anim && inPedestal) // ~~~ Using Animation System ~~~
        {
            animator.SetBool(triggerList[OrbColor], true);
        }
        else if (mode == puzzlePartMode.Anim && !inPedestal)
        {
            animator.SetBool(triggerList[OrbColor], false);
        }
        else
        {
            Debug.Log("invalid puzzlePartMode on part " + gameObject.name);
        }
            
    }

    private void Update()
    {
        if (mode == puzzlePartMode.Lerp)
        {
            if (Vector3.Distance(transform.position, target) > .1)
            {
                //Debug.Log("Move from " + transform.position + " to " + target);
                //Debug.Log("move");
                float distTraveled = (Time.time - lerpStartTime) * lerpSpeed;
                float lerpFraction = distTraveled / lerpLength;

                transform.position = Vector3.Lerp(transform.position, target, lerpFraction);
                isMoving = true;
                
            }
            else if (Vector3.Distance(transform.position, target) < .1 && isMoving)
            {
                isMoving = false;
            }
            else if (Vector3.Distance(transform.position, startPos) < .1 && colorExternal != 0 && lerpActions[colorExternal] != new Vector3(-999, -999, -999) && !isMoving) // At start, but something is in the pedestal
            {
                //Debug.Log("Move from start in update");
                colorInternal = colorExternal;

                lerpStartTime = Time.time;
                lerpLength = Vector3.Distance(transform.position, lerpActions[colorExternal]);
                target = lerpActions[colorExternal];

                isMoving = true;
            }
            else if (Vector3.Distance(transform.position, target) < .1 && Vector3.Distance(transform.position, startPos) > .1 && colorExternal != colorInternal && !isMoving)
            {
                //Debug.Log("Move to start in update");

                lerpStartTime = Time.time;
                lerpLength = Vector3.Distance(transform.position, lerpActions[colorExternal]);
                target = lerpActions[0];

                isMoving = true;
            }
            else
            {
                //Debug.Log("Wait");
            }
        }
    }

    IEnumerator Move(int OrbColor, bool inPedestal, float lagTime)
    {
        //Debug.Log("wait for it");
        yield return new WaitForSeconds(lagTime);
        //Debug.Log("start the move");
        if (inPedestal == true && lerpActions[OrbColor] != new Vector3(-999, -999, -999)) // Orb is in pedestal and this object should do something
        {
            //Debug.Log("ball" + OrbColor);
            colorExternal = OrbColor;
            if (transform.position == startPos)
            {
                //Debug.Log("Starting move");
                colorInternal = OrbColor;
                
            }
        }
        else
        {
            //Debug.Log("clear pedestal");
            colorExternal = 0; // Indicates that there is nothing in the pedestal
        }
    }
}
