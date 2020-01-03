using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public Transform holdPoint;        //get the point at which we'll be held
    public Rigidbody orbRigidBody; //get the orb's rigidbody
    public bool isHeld;         //are we being held?
    public Material redMat, greenMat, blueMat, orangeMat;
    public enum orbColorList{ None, Red, Green, Blue, Orange }; //what color is our orb?
    public orbColorList orbColor; //put it in the editor
    public int orbColorInt;
    public MeshRenderer orbRend;
    public Vector3 startPos;

    private void Awake()
    {
        transform.parent = null;
        orbRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public void Start()
    {   //define where the object is going to go if it's held
        holdPoint = GameObject.Find("Hold Point Player").GetComponent<Transform>();

        #region //Color definition
        if (orbColor == orbColorList.Red)
        {
            orbRend.material = redMat;
        }

        if (orbColor == orbColorList.Green)
        {
            orbRend.material = greenMat;
        }

        if (orbColor == orbColorList.Blue)
        {
            orbRend.material = blueMat;
        }

        if (orbColor == orbColorList.Orange)
        {
            orbRend.material = orangeMat;
        }
        #endregion

        //cast the enum to an int
        orbColorInt = (int)orbColor;

        //set our starting position to the very first position recorded by the game
        startPos = transform.position;
    }

    public void FixedUpdate()
    {
        if (isHeld)
        {
            Vector3 direction = holdPoint.position - orbRigidBody.position;
            orbRigidBody.velocity = (direction * Time.fixedDeltaTime * 1000f);
        }
    }

    public void ResetPos()
    {
        transform.position = startPos;
        orbRigidBody.velocity = new Vector3(0, 0, 0);
    }

}
