using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool isHolding; //check to see if we're holding something
    public Transform cameraPos;
	private OrbScript _heldOrb;

    [Range(0,15)]public float throwPower = 5;

    void FixedUpdate()
    {
       Vector3.Lerp(transform.position, cameraPos.position, 1f);
    }

    //cast a raycast forwards and see if we collide with an orb to pickup
    void Update()
    {
        //setup our ray to detect anything aside from the player
        int layerMask = 1 << 8; //get bitmap
        layerMask = ~layerMask; //invert it
        RaycastHit hit;         //define our raycast

		//check to see if it can hit anything
		if (_heldOrb == null && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, layerMask)) // If the player is not holding a ball and the ray hits something
		{
			//Debug.Log("Hit");
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5, Color.red);

			if ((hit.collider.tag == "Orb") & (Input.GetKeyDown(KeyCode.E))) // If the thing the ray hits is a ball and the player presses e
			{   //make sure we both make the orb get held and make the camera hold the camera
				hit.collider.gameObject.GetComponent<OrbScript>().isHeld = !hit.collider.gameObject.GetComponent<OrbScript>().isHeld;
				isHolding = !isHolding;
				_heldOrb = hit.collider.gameObject.GetComponent<OrbScript>();
			}
		}
		else if (_heldOrb != null) // If the player is holding a ball
		{
			if (Input.GetKeyDown(KeyCode.E)) // If e is pressed, drop the ball
			{
				_heldOrb.isHeld = false;
				isHolding = false;
				_heldOrb = null;
			}
            else if (Input.GetKeyDown(KeyCode.Mouse0)) // If left mouse is clicked, throw the ball
            {
                _heldOrb.isHeld = false;
                _heldOrb.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 500 * throwPower);
                isHolding = false;
                _heldOrb = null;
            }
		}
		else // If the player is not holding a ball and the ray doesn't hit anything
		{
			//Debug.Log("No Hit");
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5, Color.yellow);
		}


    }
}
