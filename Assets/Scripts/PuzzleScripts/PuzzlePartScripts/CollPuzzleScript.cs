using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollPuzzleScript : MonoBehaviour
{
    [Tooltip("Does the order that the sources activate in matter?")] public bool inOrder = false;
    [Tooltip("The sources that activate this puzzlePart")] public List<GameObject> sources;
    [HideInInspector] public List<GameObject> usedSources;
    [Tooltip("The list of puzzle objects that this Collector activates")] public List<PuzzlePartScript> puzzleParts;
    /// <summary>
    /// The number of times this Collector has been activated, used to keep track of 
    /// </summary>
    [HideInInspector] public int numActivations;

    private void Start()
    {
        usedSources = new List<GameObject>();
        numActivations = 0;
    }

    public void Activate(int activateColor, bool isActivated, GameObject source)
    {
        if (sources.Contains(source))
        {
            if (inOrder && isActivated)
            {
                if (sources[numActivations] == source && !usedSources.Contains(source))
                {
                    numActivations++;
                    usedSources.Add(source);
                    source.gameObject.GetComponent<CondPuzzleScript>().SetColor(activateColor);
                    if (numActivations == sources.Count)
                    {
                        numActivations = 0;
                        usedSources = new List<GameObject>();
                        foreach (PuzzlePartScript activated in puzzleParts)
                        {
                            activated.Activate(activateColor, isActivated, gameObject);
                        }
                    }
                }
                else
                {
                    numActivations = 0;
                    usedSources = new List<GameObject>();
                }
            }
            else if (isActivated && !usedSources.Contains(source))
            {
                numActivations++;
                usedSources.Add(source);
                source.gameObject.GetComponent<CondPuzzleScript>().SetColor(activateColor);
                if (numActivations == sources.Count)
                {
                    numActivations = 0;
                    usedSources = new List<GameObject>();
                    foreach (PuzzlePartScript activated in puzzleParts)
                    {
                        activated.Activate(activateColor, isActivated, gameObject);
                    }

                }
            }
            StartCoroutine(resetColors(activateColor, isActivated));
        }
    }

    public IEnumerator resetColors(int activateColor, bool isActivated)
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject cond in sources)
        {
            if (cond.GetComponent<CondPuzzleScript>() && !usedSources.Contains(cond))
            {
                cond.GetComponent<CondPuzzleScript>().SetColor(0);
            }
        }
    }
}
