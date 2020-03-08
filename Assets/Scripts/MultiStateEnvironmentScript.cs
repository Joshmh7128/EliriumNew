using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateEnvironmentScript : ReactantScript
{   //define our possible checks
    public Orb_PuzzleScript.orbColorList checkColorAlpha;
    public Orb_PuzzleScript.orbColorList checkColorBeta;
    public Orb_PuzzleScript.orbColorList checkColorGamma;
    public Orb_PuzzleScript.orbColorList checkColorDelta;
    //define our possible states
    public enum environmentStateList
    {
        none, stateAlpha, stateBeta, stateGamma, stateDelta
    };
    //what state do we want to be in
    public environmentStateList desiredEnvironmentState;
    public environmentStateList currentEnvironmentState;
    public environmentStateList environmentStateLastActive;
    //our animator
    public Animator StateAnimator;
    //are we in the idle position?
    public bool isIdle;
    public bool returningIdle;
    //have we used this object before?
    public bool firstUse;
    [Header("Use this to compensate for the length of the first animation that plays, the default added is 5 seconds")]
    public float firstUseAdd;

    public void Start()
    {   //when we start out we're not in a state
        currentEnvironmentState = environmentStateList.none;
        isIdle = true;
        firstUse = true;
    }

    public void FixedUpdate()
    {   //are we in our idle state?
        //if the current state of the object changes, 
        if (currentEnvironmentState != desiredEnvironmentState)
        {
            if (isIdle)
            {
                //this is where we define all of our states and ONLY how to get to them
                if (desiredEnvironmentState == environmentStateList.stateAlpha)
                {
                    StateAnimator.Play("ALPHA_GOTO", 0);
                    StartCoroutine(WaitForAnim(StateAnimator.GetCurrentAnimatorStateInfo(0).length+1, environmentStateList.stateAlpha, true));
                    isIdle = false;
                }

                //this is where we define all of our states and ONLY how to get to them
                if (desiredEnvironmentState == environmentStateList.stateBeta)
                {
                    StateAnimator.Play("BETA_GOTO", 0);
                    StartCoroutine(WaitForAnim(StateAnimator.GetCurrentAnimatorStateInfo(0).length+1, environmentStateList.stateBeta, true));
                    isIdle = false;
                }
            }

            if (isIdle == true && desiredEnvironmentState == environmentStateList.none)
            {
                ReturnToNone(currentEnvironmentState);
            }
        }   
    }

    public override void React(int color)
    {
        //this sets the desired state
        if (color == (int)checkColorAlpha)
        {
            desiredEnvironmentState = environmentStateList.stateAlpha;
        }

        if (color == (int)checkColorBeta)
        {
            desiredEnvironmentState = environmentStateList.stateBeta;
        }
    }

    public override void Deact()
    {
        desiredEnvironmentState = environmentStateList.none;
        ReturnToNone(currentEnvironmentState);
    }

    public void ReturnToNone(environmentStateList currentState)
    {
        //set all go back states
        if (currentState == environmentStateList.stateAlpha)
        {
            if (isIdle)
            isIdle = false;
            StartCoroutine(WaitForAnim(StateAnimator.GetCurrentAnimatorStateInfo(0).length, environmentStateList.none, true));
            StateAnimator.Play("ALPHA_GOBACK", 0);
        }

        if (currentState == environmentStateList.stateBeta)
        {
            if (isIdle)
            isIdle = false;
            StartCoroutine(WaitForAnim(StateAnimator.GetCurrentAnimatorStateInfo(0).length, environmentStateList.none, true));
            StateAnimator.Play("BETA_GOBACK", 0);
            
        }
    }

    //plan an animation before this, then run this to set the state
    IEnumerator WaitForAnim(float clipLength, environmentStateList setState, bool setIdle)
    {   //if this is the first use, add extra clip time and make sure we never do this again
        if (firstUse) { clipLength += (firstUseAdd +=5); firstUse = false; }
        Debug.Log("Animation Length is " + clipLength);
        yield return new WaitForSeconds(clipLength);
        Debug.Log("Animation has completed! State is now set to " + setState);
        currentEnvironmentState = setState; 
        isIdle = setIdle;
    }

}
