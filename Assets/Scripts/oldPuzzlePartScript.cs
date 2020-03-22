using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class oldPuzzlePartScript : MonoBehaviour
{
    #region GlobalVariables
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
    [Tooltip("The pedestal that activates this puzzlePart")] public GameObject pedestal;
    #endregion

    #region LerpVariables
    #region Inspector Variables
    public enum lerpMode { Speed, Duration }

    [ConditionalField("mode", false, puzzlePartMode.Lerp)] public lerpMode lerpStyle;
    /// <summary>
    /// The list of vector3 positions this puzzlePart can lerp to
    /// </summary>
    [HideInInspector] public List<Vector3> lerpActions;
    /// <summary>
    /// The delay between the ball going in the pedestal and the puzzlePart starting to move, controllable from the editor
    /// </summary>
    [ConditionalField("mode", false, puzzlePartMode.Lerp)] [Range(0, 5)] public int lerpLagTime = 1;
    /// <summary>
    /// The speed at which this puzzlePart moves, controllable from the editor
    /// </summary>
    [ConditionalField("lerpStyle", false, lerpMode.Speed)] [Range(0, 25)] public float lerpSpeed = 1;
    /// <summary>
    /// The speed at which this puzzlePart moves, controllable from the editor
    /// </summary>
    [ConditionalField("lerpStyle", false, lerpMode.Duration)] [Range(0, 25)] public float lerpDuration = 1;
    /// <summary>
    /// One of the vector3 positions that this puzzlePart can lerp to
    /// </summary>
    [ConditionalField("mode", false, puzzlePartMode.Lerp)] public Vector3 lerpDefaultPos, redLerpAction = new Vector3(-999, -999, -999), greenLerpAction = new Vector3(-999, -999, -999), blueLerpAction = new Vector3(-999, -999, -999), orangeLerpAction = new Vector3(-999,-999,-999);
    #endregion

    #region Internal Variables
    /// <summary>
    /// The time that the lerp started, used in lerp movement
    /// </summary>
    private float lerpStartTime;
    /// <summary>
    /// The initial length of the lerp, used in lerp movement
    /// </summary>
    private float lerpLength;
    /// <summary>
    /// The orb color (in int form) that is currently in the pedestal
    /// </summary>
    private int colorExternal;
    /// <summary>
    /// The orb color (in int form) that is the current target
    /// </summary>
    private int colorInternal;
    /// <summary>
    /// The current target position, used in lerp movement
    /// </summary>
    private Vector3 target;

    private Vector3 checkTarget;
    /// <summary>
    /// The position that this puzzlePart started in, the default that it reverts to
    /// </summary>
    private Vector3 startPos;
    /// <summary>
    /// Is this puzzlePart currently lerping to a target?
    /// </summary>
    private bool isLerping;

    private Vector3 lerpStartPos;
    #endregion
    #endregion

    #region AnimVariables
    /// <summary>
    /// The animator attached to this puzzlePart
    /// </summary>
    [ConditionalField("mode", false, puzzlePartMode.Anim)] public Animator animator;
    /// <summary>
    /// The list of booleans in the animator, used to trigger animations
    /// </summary>
    [HideInInspector]public List<string> animBoolList;
    #endregion

    #region CondVariables
    [ConditionalField("mode", false, puzzlePartMode.Cond)][Tooltip("The list of puzzle objects that this pedestal activates")] public List<PuzzlePartScript> puzzleParts;

    [ConditionalField("mode", false, puzzlePartMode.Cond)] public List<Material> puzzlePartMats;
    [ConditionalField("mode", false, puzzlePartMode.Cond)] public MeshRenderer pedRend;
    private Material targetMat;
    #endregion

    private void Start()
    {
        #region LerpVariables
        if (mode == puzzlePartMode.Lerp)
        {
            // Add the lerp actions to the list for internal use
            lerpActions.Add(lerpDefaultPos);
            lerpActions.Add(redLerpAction);
            lerpActions.Add(greenLerpAction);
            lerpActions.Add(blueLerpAction);
            lerpActions.Add(orangeLerpAction);

            // Sets the default values for lerping variables, makes sure the puzzlePart doesn't move immediately
            lerpStartTime = Time.time;
            lerpLength = 0;
            target = transform.position;
            colorExternal = 0; // Indicates that there is nothing in the pedestal
            isLerping = false;

            // Sets the lerpDefaultPos to the current position
            startPos = transform.position;
            lerpActions[0] = startPos;
        }
#endregion

        #region AnimVariables
        else if (mode == puzzlePartMode.Anim)
        {
            // Adds the animation bools to the list for internal use
            animBoolList.Add("white");
            animBoolList.Add("red");
            animBoolList.Add("green");
            animBoolList.Add("blue");
            animBoolList.Add("purple");
        }
        #endregion

        #region CondVariables
        else if (mode == puzzlePartMode.Cond)
        {
            pedRend = gameObject.GetComponent<MeshRenderer>();
            targetMat = pedRend.material;
        }
        #endregion
    }

    /// <summary>
    /// The main function of this script, takes the input from the pedestal and activates the appropriate action
    /// </summary>
    /// <param name="OrbColor">The color of the orb entering/leaving the pedestal</param>
    /// <param name="inPedestal">Determines whether the orb is entering or leaving the pedestal</param>
    public void Activate(int OrbColor, bool inPedestal)
    {
#region LerpActivate
        if (mode == puzzlePartMode.Lerp) // ~~~ Using Lerp System ~~~
        {
            // Passes the information to a coroutine to allow for lag time
            // StartCoroutine(lerpMove(OrbColor, inPedestal, lerpLagTime));

            if (inPedestal == true && lerpActions[OrbColor] != new Vector3(-999, -999, -999)) // Orb is in pedestal and this object should do something
            {
                // Updates the external (current) orb in the pedestal
                colorExternal = OrbColor;
                if (transform.position == startPos) // If in the default position
                {
                    // Updates the internal (movement target) orb color
                    colorInternal = OrbColor;
                }
            }
            else // No orb is in the pedestal or the current orb has no associated action
            {
                colorExternal = 0; // Indicates that there is nothing in the pedestal
            }
        }
#endregion

#region AnimActivate
        else if (mode == puzzlePartMode.Anim && inPedestal) // Using animation system and ball entering pedestal
        {
            // Passes the information to the animator attached to this puzzlePart
            animator.SetBool(animBoolList[OrbColor], true);
        }
        else if (mode == puzzlePartMode.Anim && !inPedestal) // Using animation system and ball leaving pedestal
        {
            // Passes teh information to the animator attached to this puzzlePart
            animator.SetBool(animBoolList[OrbColor], false);
        }
        #endregion

#region CondActivate
        else if (mode == puzzlePartMode.Cond)
        {
            foreach (PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(OrbColor, inPedestal, gameObject);
            }
            if (inPedestal)
            {
                SetColor(OrbColor);

            }
            else
            {
                SetColor(0);
            }
        }
        #endregion

        // Error detection
        else
        {
            Debug.Log("invalid puzzlePartMode on part " + gameObject.name);
        }
    }

    private void Update()
    {
#region LerpUpdate
        if (mode == puzzlePartMode.Lerp)
        {
            if (Vector3.Distance(transform.position, target) > .1) // If the target is not ~= the current position
            {
                // Updates distTraveled and lerpFraction based on the time, speed, and distance variables, used in the lerp
                float distTraveled = (Time.time - lerpStartTime) * lerpSpeed;
                float lerpFraction = distTraveled / lerpLength;

                // Changes the position through a lerp
                transform.position = Vector3.Lerp(lerpStartPos, target, lerpFraction);
                // Disables new movement until this lerp is complete
                isLerping = true;
                
            }
            else if (Vector3.Distance(transform.position, checkTarget) < .1 && isLerping) // If the target is ~= the current position and the puzzlePart was lerping previously
            {
                // Enables new movmement
                isLerping = false;
            }
            else if (Vector3.Distance(transform.position, startPos) < .1 && colorExternal != 0 && lerpActions[colorExternal] != new Vector3(-999, -999, -999) && !isLerping) // At start, but something is in the pedestal
            {
                // Shift the external color (currently in the pedestal) to the internal (current target)
                colorInternal = colorExternal;

                // Sets the lerp variables to begin movement
                
                lerpLength = Vector3.Distance(transform.position, lerpActions[colorExternal]);
                if(lerpStyle == lerpMode.Duration)
                {
                    lerpSpeed = lerpLength / lerpDuration;
                }
                lerpStartPos = transform.position;
                checkTarget = lerpActions[colorInternal]; // Target determined by current orbColor
                StartCoroutine(lerpMove(lerpActions[colorInternal]));

                // Disables new movement until the lerp is complete
                isLerping = true;
            }
            else if (Vector3.Distance(transform.position, target) < .1 && Vector3.Distance(transform.position, startPos) > .1 && colorExternal != colorInternal && !isLerping) // At a position, but that color is not in the pedestal
            {
                // Sets the lerp variables to begin movement
                lerpLength = Vector3.Distance(transform.position, lerpActions[colorExternal]);
                if (lerpStyle == lerpMode.Duration)
                {
                    lerpSpeed = lerpLength / lerpDuration;
                }
                lerpStartPos = transform.position;
                checkTarget = lerpActions[0]; // Target is intial default position
                StartCoroutine(lerpMove(lerpActions[0]));

                // Disables new movment until the lerp is complete
                isLerping = true;
            }
            else
            {
                // No need to move
            }
        }
        #endregion

#region CondUpdate
        else if (mode == puzzlePartMode.Cond)
        {
            pedRend.material.Lerp(pedRend.material, targetMat, 0.2f);
        }
        #endregion
    }

    /// <summary>
    /// The coroutine used when an orb enters or leaves the pedestal in lerp mode
    /// </summary>
    /// <param name="OrbColor"></param>
    /// <param name="inPedestal"></param>
    /// <param name="lagTime"></param>
    /// <returns></returns>
    IEnumerator lerpMove(Vector3 targetPos)
    {
        yield return new WaitForSeconds(lerpLagTime);

        lerpStartTime = Time.time;
        target = targetPos;
    }

    public void SetColor(int colorNum)
    {
        targetMat = puzzlePartMats[colorNum];
    }
}
