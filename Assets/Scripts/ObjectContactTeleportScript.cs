using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContactTeleportScript : MonoBehaviour
{
    // where are we going to?
    public Vector3 gotoPosition;

    private void OnTriggerEnter(Collider obj)
    {
        // teleport any object that touches this to it's new position
        if (obj.CompareTag("Orb") || obj.CompareTag("Player"))
        {
            obj.transform.position = gotoPosition; // set position
            obj.attachedRigidbody.velocity = new Vector3(0, 0, 0); // slow that boy down
        }
    }
}
