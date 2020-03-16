using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffEnvironmentScript : ReactantScript
{   //our four checks
    //public Orb_PuzzleScript.orbColorList checkColorAlpha;
    //public Orb_PuzzleScript.orbColorList checkColorBeta;
    //public Orb_PuzzleScript.orbColorList checkColorGamma;
    //public Orb_PuzzleScript.orbColorList checkColorDelta;
    //is it on?
    public bool isOn;
    //first use?
    public bool firstUse;
    public bool canStop;

    public Animator StateAnimator;

    public void Update()
    {
        if (isOn)
        {
            if (!StateAnimator.GetCurrentAnimatorStateInfo(0).IsName("MAIN_LOOP"))
            {
                canStop = true;
                StateAnimator.Play("MAIN_LOOP");
            }
        }
    }


    public override void React(int color)
    {
        isOn = true;
    }

    public override void Deact()
    {
        isOn = false;
    }
    IEnumerator WaitForAnim(float clipLength)
    {   //if this is the first use, add extra clip time and make sure we never do this again
        if (firstUse) { clipLength += 8f; firstUse = false; }
        Debug.Log("Animation Length is " + clipLength);
        yield return new WaitForSeconds(clipLength);
        Debug.Log("Animation has completed! State is now " + isOn);
    }
}
