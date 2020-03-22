using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheckPointScript : MonoBehaviour
{
    public Vector3 playerPos; //where will we move the player to?
    public Orb_PuzzleScript[] puzzleOrbs;  //our orbs per checkpoint
    public int currentPart;
    public int myPart;


    // Start is called before the first frame update
    void Start()
    {   
        //set the player's position to the first position recorded in the area
        playerPos= GameObject.Find("Player").GetComponent<Transform>().position;
    }

    void UpdatePlayerPos()
    {
        //set the player's position to the place they're standing when they activate the checkpoint
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
    }

    public void ResetPos()
    {
        if (currentPart == myPart)
        {
            //go through our array of orbs and reset every one of their positions
            foreach (Orb_PuzzleScript orbScript in puzzleOrbs)
            {
                orbScript.ResetPos();
            }

            GameObject.Find("Player").GetComponent<Transform>().position = playerPos;
        }
    }

    void UpdateCurrentPart()
    {
        foreach (PuzzleCheckPointScript p in GameObject.Find("Checkpoint List").GetComponent<CheckPointList>().puzzleCheckPointList)
        {   //update all our puzzle scripts so that they all know what part the player is up to
            p.currentPart = currentPart;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        currentPart = myPart; //set the current part to our part
        UpdateCurrentPart();  //make every single script say that this part is our current part
        UpdatePlayerPos();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetPos();
        }
    }
}
