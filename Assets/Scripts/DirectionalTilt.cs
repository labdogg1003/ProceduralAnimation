using UnityEngine;
using System.Collections;

public class DirectionalTilt : MonoBehaviour
{
	public float speed = 5.0f;

	Quaternion temp;
	Vector3 v;
	float horizontal;
	float vertical;
	public float tiltAngle;  
	Quaternion characterRotation;    //Stores the characters rotation
	float moveSpeed = 6;
	public float smooth = 1f;             //Turn smoothing


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Receives user input
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		
		//Controls change based on camera position (This is for debugging purposes)
		Vector3 forward = Camera.main.transform.TransformDirection (Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		
		Vector3 right = new Vector3 (forward.z, 0, -forward.x);
		
		//The direction the player is moving
		Vector3 inputVec = horizontal * right + vertical * forward;
		inputVec *= moveSpeed;
		
		//Calculates the tilt based on user input
		Quaternion target = Quaternion.Euler ((Mathf.Abs(vertical) + Mathf.Abs(horizontal)) * tiltAngle, 0, 0);
		
		if (inputVec != Vector3.zero) 
		{
			//Smoothing rotates the character and assigns the new rotation to CharacterRotation
			characterRotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (inputVec, Vector3.up), Time.deltaTime * smooth);
			transform.rotation = characterRotation * target;
		}

	}
}
