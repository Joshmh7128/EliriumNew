using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{

    [Tooltip("The orb detection trigger on this pedestal.")] public Collider orbDetect;
    [Tooltip("The list of puzzle objects that this pedestal activates")] public List<PuzzlePartScript> puzzleParts;

    public List<Material> orbMats;
    public enum pedColorList { None, Red, Green, Blue, Orange }; //what color is our orb?
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
        if (other.CompareTag("Orb"))
        {
            //Debug.Log("Orb!");
            foreach (PuzzlePartScript activated in puzzleParts)
            {
                activated.Activate(other.GetComponent<OrbScript>().orbColorInt);
            }
            SetColor(other.GetComponent<OrbScript>().orbColorInt);
        }
        else
        {
            //Debug.Log("Not orb!");
        }
    }

    public void SetColor(int colorNum)
    {
        targetMat = orbMats[colorNum];

        //cast the enum to an int
        pedColorInt = colorNum;
    }
}
