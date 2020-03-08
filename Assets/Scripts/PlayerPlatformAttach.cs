using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformAttach : MonoBehaviour
{
    public GameObject Player;
	private List<GameObject> _children;

    private void Awake()
    {

    }

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

	private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player || other.GetComponent<Orb_PuzzleScript>())
        {
			if (other.transform.parent == null)
			{
				other.transform.parent = transform;
			}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player || other.GetComponent<Orb_PuzzleScript>())
        {
			if(other.transform.parent == transform)
				other.transform.parent = null;
        }
    }
}
