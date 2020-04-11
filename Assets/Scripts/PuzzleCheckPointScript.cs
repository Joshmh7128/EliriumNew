using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheckPointScript : MonoBehaviour
{
    public Vector3 playerPos; //where will we move the player to?
    // private Orb_PuzzleScript[] puzzleOrbs;  //our orbs per checkpoint
    private List<Orb_PuzzleScript> puzzleOrbs = new List<Orb_PuzzleScript>();
    private List<Orb_PuzzleScript> storedOrbs = new List<Orb_PuzzleScript>();
    private List<Vector3> storedOrbPositions = new List<Vector3>();
    public int currentCheckpoint;
    public int thisCheckpoint;

    private PlayerController player;

    public bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {   
        //set the player's position to the first position recorded in the area
        playerPos= GameObject.Find("Player").GetComponent<Transform>().position;
        isActive = false;
    }

    void UpdatePlayerPos()
    {
        //set the player's position to the place they're standing when they activate the checkpoint
        playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
    }

    public void ResetPos()
    {
        Debug.Log("ResetPos Called");
        if (currentCheckpoint == thisCheckpoint)
        {
            if (isActive)
            {
                for (int orbNum = 0; orbNum < storedOrbs.Count; orbNum++)
                {
                    storedOrbs[orbNum].ResetPos(storedOrbPositions[orbNum]);
                }

                Debug.Log(playerPos);
                player.ResetPos(playerPos);
            }
            //GameObject.Find("Player").GetComponent<Transform>().position = playerPos;
        }
    }

    void UpdateCurrentPart()
    {
        foreach (PuzzleCheckPointScript p in GameObject.Find("Checkpoint List").GetComponent<CheckPointList>().puzzleCheckPointList)
        {   //update all our puzzle scripts so that they all know what part the player is up to
            p.currentCheckpoint = currentCheckpoint;
            p.isActive = false;
        }
        isActive = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!isActive && col.CompareTag("Player"))
        {
            Debug.Log("Smooth jazz");

            isActive = true;
            player = col.gameObject.GetComponent<PlayerController>();

            currentCheckpoint = thisCheckpoint; //set the current part to our part
            UpdateCurrentPart();  //make every single script say that this part is our current part
            UpdatePlayerPos();
        }
        else if (col.GetComponent<Orb_PuzzleScript>())
        {
            if (!storedOrbs.Contains(col.GetComponent<Orb_PuzzleScript>()))
            {
                storedOrbs.Add(col.GetComponent<Orb_PuzzleScript>());
                storedOrbPositions.Add(col.gameObject.transform.position);
                Debug.Log(col.gameObject.transform.position);
            }
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetPos();
        }
    }
}
