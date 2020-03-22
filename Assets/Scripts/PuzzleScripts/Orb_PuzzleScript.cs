using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb_PuzzleScript : PuzzlePartScript
{
    [HideInInspector] public Transform holdPoint;        //get the point at which we'll be held
    [HideInInspector] public Rigidbody orbRigidBody; //get the orb's rigidbody
    [HideInInspector] public Vector3 startPos;

    [HideInInspector] public bool isHeld;         //are we being held?

    protected override void Start()
    {
        base.Start();

        transform.parent = null;
        orbRigidBody = gameObject.GetComponent<Rigidbody>();

        //define where the object is going to go if it's held
        holdPoint = GameObject.Find("Hold Point Player").GetComponent<Transform>();

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

    public override void Activate(int activateColor, bool isActivated, GameObject source)
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
}
