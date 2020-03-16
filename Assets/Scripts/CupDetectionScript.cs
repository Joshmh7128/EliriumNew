using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupDetectionScript : MonoBehaviour
{
    //define all of our possible orb colors so that we can send them to our puzzle manager
    public enum heldColorList { None, Red, Green, Blue, Orange }; //MAKE SURE THIS LIST IS UPDATED TO THE SAME ONE AS THE ONE IN THE ORB SCRIPT!
    public heldColorList heldColor; //put it in the editor as a standing list
    public int heldColorInt;       //make sure we have an int for our list as well so we can get it from the other
    //stuff to make sure the ball functions properly and doesn't spam reactants
    public int setWaitAmount;
    public int waitAmount;
    public int countAmount;
    public int localWaitAmount;
    public bool isReacting;
    public bool cupActive;
    //Here is our reactant list of all the objects that we will be activating
    public List<ReactantScript> ReactantList;
    
    void FixedUpdate()
    {
        if (waitAmount <= localWaitAmount)
        {
            waitAmount += countAmount;
        }

        if (waitAmount >= localWaitAmount)
        {
            if (isReacting)
            {
                Debug.Log("reaction triggered...");
                React();
            }
            else
            {/*
                Debug.Log("deaction triggered...");
                Deact();
                */
            }
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Orb"))
        {   //get the casted Int from our orbscript, then turn that Int back into the proper list
            heldColorInt = col.gameObject.GetComponent<Orb_PuzzleScript>().puzzlePartColorInt;
            heldColor = (heldColorList)heldColorInt;
            cupActive = true; //is the cup holding something?
            isReacting = true; //are we reacting or deacting?
            React();
            /*
            countAmount = 1;
            localWaitAmount = setWaitAmount;*/
        }
    }

    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Orb"))
        {
            //heldColor = heldColorList.None;
            cupActive = false;   //the cup is not holding 
            isReacting = false;  //is the cup reacting or deacting
            countAmount = 1;     //how much to count per fixed update 
            localWaitAmount = setWaitAmount;
            Debug.Log("deaction triggered...");
            Deact();
        }
    }

    public void React()
    {
        Debug.Log("reacting...");
        foreach (ReactantScript reactant in ReactantList)
        {   //for every single react-able object, make sure we run the react script in our abstract class located in reactant script
            reactant.React(heldColorInt);
        }
        waitAmount = 0;
    }

    public void Deact()
    {
        Debug.Log("deacting...");
        foreach (ReactantScript reactant in ReactantList)
        {   //for every single react-able object, make sure we run the deact function in our abstract class located in reactant script
            reactant.Deact();
        }
        //reset our wait amount
        waitAmount = 0;

    }
}
