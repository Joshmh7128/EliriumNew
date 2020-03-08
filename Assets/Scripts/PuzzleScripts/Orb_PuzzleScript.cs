using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_PuzzleScript : MonoBehaviour
{
    public Transform holdPoint;        //get the point at which we'll be held
    public Rigidbody orbRigidBody; //get the orb's rigidbody
    public Vector3 startPos;

    public bool isHeld;         //are we being held?

    public List<Material> orbMats;
    public enum orbColorList{ None, Red, Green, Blue, Purple }; //what color is our orb?
    public orbColorList orbColor; //put it in the editor
    public int orbColorInt;

    public MeshRenderer orbRend;
    private Material targetMat;

    public void Start()
    {
        transform.parent = null;
        orbRigidBody = gameObject.GetComponent<Rigidbody>();

        //define where the object is going to go if it's held
        holdPoint = GameObject.Find("Hold Point Player").GetComponent<Transform>();

        SetColor((int)orbColor);

        //set our starting position to the very first position recorded by the game
        startPos = transform.position;
    }

    public void Update()
    {
        orbRend.material.Lerp(orbRend.material, targetMat, 0.2f);
    }

    public void FixedUpdate()
    {
        if (isHeld)
        {
            Vector3 direction = holdPoint.position - orbRigidBody.position;
            orbRigidBody.velocity = (direction * Time.fixedDeltaTime * 1000f);
        }
    }

    public void Activate(int activateColor, bool isActivated)
    {
        if (isActivated)
        {
            SetColor(activateColor);
        }
    }

    public void ResetPos()
    {
        transform.position = startPos;
        orbRigidBody.velocity = new Vector3(0, 0, 0);
    }

    public void SetColor(int colorNum)
    {
        targetMat = orbMats[colorNum];

        //cast the enum to an int
        orbColorInt = colorNum;
    }

}
