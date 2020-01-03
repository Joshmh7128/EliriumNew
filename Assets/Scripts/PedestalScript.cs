using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{

    [Tooltip("The orb detection trigger on this pedestal.")] public Collider orbDetect;
    [Tooltip("The list of puzzle objects that this pedestal activates")] public List<PuzzlePartScript> puzzleParts;

    // Start is called before the first frame update
    void Start()
    {
        if (puzzleParts.Count == 0)
        {
            Debug.Log("No puzzle parts!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            //Debug.Log("Orb!");
            foreach (PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(other.GetComponent<OrbScript>().orbColorInt);
            }
        }
        else
        {
            //Debug.Log("Not orb!");
        }
    }
}
