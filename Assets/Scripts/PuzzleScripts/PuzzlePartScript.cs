using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PuzzlePartScript : MonoBehaviour
{

    #region Inspector Variables
    [Header("PuzzlePart Variables")]
    [Tooltip("The materials for use in the SetColor() script. Materials can be found in Assets/Materials/Puzzle Materials.")] public List<Material> puzzlePartMats;
    public enum puzzlePartColorPicker { White, Red, Green, Blue, Purple }; // Base enum for the signal colors.
    [Tooltip("Editor instance of puzzlePartColorPicker. Used in signal sending/receiving and color switching.")]public puzzlePartColorPicker puzzlePartColor = puzzlePartColorPicker.White;
    #endregion

    #region Internal Variables
    [HideInInspector, Tooltip("Int value of this puzzlePart's color. Assigned in SetColor(), can be accessed through GetColor().")] public int puzzlePartColorInt;
    [HideInInspector, Tooltip("The MeshRenderer component of this GameObject.")] public MeshRenderer puzzlePartRend;
    [HideInInspector, Tooltip("The target material, set in SetColor() and used to change color in Update().")] public Material targetMat;
    #endregion

    /// <summary>
    /// Base class Activate function, overriden by relevant code in each partPuzzleScript.
    /// </summary>
    /// <param name="activateColor">The color of the signal entering the puzzlePart</param>
    /// <param name="isActivated">The boolean value of the signal. True = signal starts, False = signal stops.</param>
    /// <param name="source">The GameObject sending the signal.</param>
    public virtual void Activate(int activateColor, bool isActivated, GameObject source)
    {
        Debug.Log("No activate on " + gameObject); // Should never be reached. Overriden in all child classes.
    }

    /// <summary>
    /// Sets the puzzlePartRend variable and the color of the puzzlePart.
    /// </summary>
    protected virtual void Start()
    {
        puzzlePartRend = gameObject.GetComponent<MeshRenderer>(); // Sets the puzzlePartRend for color changes.
        SetColor((int)puzzlePartColor); // Sets the targetMat to the value set in the editor.
    }

    /// <summary>
    /// Uses material.lerp to change the color of the puzzlePart.
    /// </summary>
    protected virtual void Update()
    {
        puzzlePartRend.material.Lerp(puzzlePartRend.material, targetMat, 0.2f);
    }

    /// <summary>
    /// Updates the target color of this puzzlePart, change applied in Update().
    /// </summary>
    /// <param name="colorNum">The int value for the color to set. 0 = white, 1 = red, etc.</param>
    public virtual void SetColor(int colorNum)
    {
        targetMat = puzzlePartMats[colorNum]; // Color moves to target in Update() with material.lerp().

        puzzlePartColorInt = colorNum; // Updates colorNum for this puzzlePart.
    }

    /// <summary>
    /// Returns the int value of this puzzlePart's current color. Set in inspector, can be changed with SetColor().
    /// </summary>
    /// <returns></returns>
    public virtual int GetColor()
    {
        return puzzlePartColorInt;
    }
}
