using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondPuzzleScript : MonoBehaviour
{
    public bool collecting;
    [Tooltip("The list of puzzle objects that this Conduit activates")] public List<PuzzlePartScript> puzzleParts;
    public List<Material> puzzlePartMats;
    private MeshRenderer condRend;
    private Material targetMat;

    // Start is called before the first frame update
    void Start()
    {
        condRend = gameObject.GetComponent<MeshRenderer>();
        targetMat = condRend.material;
    }

    public void Activate(int activateColor, bool isActivated)
    {
        foreach (PuzzlePartScript activated in puzzleParts)
        {
            activated.Activate(activateColor, isActivated, gameObject);
        }

        if (!collecting)
        {
            if (isActivated)
            {
                SetColor(activateColor);

            }
            else
            {
                SetColor(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        condRend.material.Lerp(condRend.material, targetMat, 0.2f);
    }

    public void SetColor(int colorNum)
    {
        targetMat = puzzlePartMats[colorNum];
    }
}
