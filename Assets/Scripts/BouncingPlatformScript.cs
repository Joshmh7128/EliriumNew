using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatformScript : MonoBehaviour
{
    public Vector3 launchAngle;
    public Rigidbody playerBody;

    public void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            launchAngle = col.contacts[0].normal; //get the normal

            Vector3 newLaunch = Vector3.Project(playerBody.velocity, -transform.up); //the direction at which to send the player as well as the velocity

            playerBody.velocity += newLaunch * -2;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerBody = col.attachedRigidbody;
        }
    }
}
