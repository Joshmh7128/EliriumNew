using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour
{
    public Vector3 launchAngle;
    public float thrust;

    public void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.CompareTag("Player")) || (col.gameObject.CompareTag("Orb")))
        {
            col.attachedRigidbody.AddForce(launchAngle * thrust, ForceMode.Force);
        }
    }
}
