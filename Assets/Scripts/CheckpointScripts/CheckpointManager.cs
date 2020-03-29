using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector3 playerPosition;
    public List<Vector3> orbPositions = new List<Vector3>();

    public PlayerController player;
    private List<Orb_PuzzleScript> orbs = new List<Orb_PuzzleScript>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        UpdateOrbs();

        playerPosition = player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckpointLoad();
        }
    }

    public void CheckpointLoad()
    {
        player.ResetPos(playerPosition);

        for (int i = 0; i < orbs.Count; i++)
        {
            if (orbs[i] != null)
            {
                orbs[i].ResetPos(orbPositions[i]);
            }
            
        }
    }

    public void UpdateOrbs()
    {
        orbs = new List<Orb_PuzzleScript>();
        orbPositions = new List<Vector3>();
        Orb_PuzzleScript[] orbArray = FindObjectsOfType<Orb_PuzzleScript>();
        Debug.Log(orbArray.Length);
        for (int i = 0; i < orbArray.Length; i++)
        {
            orbs.Add(orbArray[i]);
            orbPositions.Add(orbs[i].transform.position);
        }
    }

    public void UpdatePlayerPos(Vector3 pos)
    {
        playerPosition = pos;
    }

    public void UpdateOrbPos(Vector3 pos, Orb_PuzzleScript orb)
    {
        if (orbs.Contains(orb))
        {
            orbPositions[orbs.IndexOf(orb)] = pos;
        }
    }
}
