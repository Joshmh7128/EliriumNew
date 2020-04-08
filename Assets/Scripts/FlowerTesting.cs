using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTesting : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material[] flowerMaterials; // the flower materials

    void Start()
    {
        // randomly select a material
        int choice = Random.Range(0, flowerMaterials.Length);
        // get the materials
        Material[] meshMaterials = meshRenderer.materials;
        // setup a new modifiable material set
        Material[] newMaterials = meshMaterials;
        // modify the set
        newMaterials[1] = flowerMaterials[choice];
        // set the materials
        meshRenderer.materials = newMaterials;
    }
}
