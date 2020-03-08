using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigPuzzleScript : MonoBehaviour
{

    [Tooltip("The orb detection trigger on this pedestal.")] public Collider orbDetect;
    [Tooltip("The list of puzzle objects that this pedestal activates")] public List<PuzzlePartScript> puzzleParts;

    public List<Material> trigMats;
    public enum pedColorList { None, Red, Green, Blue, Purple }; //what color is our orb?
    public pedColorList pedColor; //put it in the editor
    public int pedColorInt;
    public MeshRenderer pedRend;
    private Material targetMat;

    // Start is called before the first frame update
    void Start()
    {
        SetColor((int)pedColor);
    }

    // Update is called once per frame
    void Update()
    {
        pedRend.material.Lerp(pedRend.material, targetMat, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Orb_PuzzleScript>())
        {
            //Debug.Log("Orb in");
            foreach (PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(other.GetComponent<Orb_PuzzleScript>().orbColorInt, true, gameObject);
            }
            SetColor(other.GetComponent<Orb_PuzzleScript>().orbColorInt);
        }
        else
        {
            //Debug.Log("Not orb!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Orb_PuzzleScript>())
        {
            //Debug.Log("Orb out");
            foreach(PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(other.GetComponent<Orb_PuzzleScript>().orbColorInt, false, gameObject);
            }

            SetColor(0);
        }
    }

    public void Activate(int activateColor, bool isActivated)
    {
        if (isActivated)
        {
            SetColor(activateColor);
        }
        else
        {
            SetColor((int)pedColor);
        }

    }

    /// <summary>
    /// Updates the color of this pedestal
    /// </summary>
    /// <param name="colorNum">The color to update to</param>
    public void SetColor(int colorNum)
    {
        targetMat = trigMats[colorNum];

        // Cast the enum to an int
        pedColorInt = colorNum;
    }
}
