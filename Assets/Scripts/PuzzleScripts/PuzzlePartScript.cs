using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PuzzlePartScript : MonoBehaviour
{
    [Header("PuzzlePart Variables")]
    public List<Material> puzzlePartMats;
    public enum puzzlePartColorPicker { White, Red, Green, Blue, Purple }; //what color is our orb?
    public puzzlePartColorPicker puzzlePartColor = puzzlePartColorPicker.White; //put it in the editor
    [HideInInspector] public int puzzlePartColorInt;
    [HideInInspector] public MeshRenderer puzzlePartRend;
    [HideInInspector] public Material targetMat;

    public virtual void Activate(int activateColor, bool isActivated, GameObject source)
    {
        Debug.Log("No activate on " + gameObject);
    }

    protected virtual void Start()
    {
        puzzlePartRend = gameObject.GetComponent<MeshRenderer>();
        SetColor((int)puzzlePartColor);
    }

    protected virtual void Update()
    {
        puzzlePartRend.material.Lerp(puzzlePartRend.material, targetMat, 0.2f);
    }

    /// <summary>
    /// Updates the color of this puzzlePart
    /// </summary>
    /// <param name="colorNum">The int value for the color to set. 0 = white, 1 = red, etc.</param>
    public virtual void SetColor(int colorNum)
    {
        targetMat = puzzlePartMats[colorNum];

        //cast the enum to an int
        puzzlePartColorInt = colorNum;
    }

    public virtual int GetColor()
    {
        return puzzlePartColorInt;
    }
}
