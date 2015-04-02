using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidbodyController : MonoBehaviour
{
	Rigidbody rigidbody;
	CapsuleCollider capsule;
	//Animator animator;
	bool isGrounded;
	bool triggerPulled;
	float groundHit = 0.0f;
	Camera  mainCamera;

	public float speed;
	public float walkSpeed = 4.0f;
	public float runSpeed = 6.0f;
	public float sideSpeed = 3.0f;
	public float gravity = 20.0f;
	public float maxVelocityChange = 10.0f;
	public float jumpHeight = 2.0f;

	//public float runMultiplier;
	
	// Use this for initialization
	void Start ()
	{
		//Grab Rigidbody so we aren't calling it each cycle
		rigidbody = gameObject.GetComponent<Rigidbody>();

		//Get The Capsul Collider
		capsule = gameObject.GetComponent<CapsuleCollider> ();

		//Get The Animator For The Player Graphic.
		//animator = gameObject.GetComponentInChildren<Animator>(); 

		//Get The Main Camera
		mainCamera = Camera.main;

		//We Want To Manually Calculate Gravity. i.e. Be Able To Change The Value Of Gravity.
		rigidbody.useGravity = false;
		
		//Stop RigidBody From Rotating Freely and Falling down.
		rigidbody.freezeRotation = true;
	}
	
	// Update is called once per phyisics cycle
	void FixedUpdate ()
	{
		//Check If We Are On The Ground
		if(isGrounded)
		{
			//animator.SetBool ("jumping", false);
			//animator.SetBool ("falling", false);

			Vector3 targetVelocity;

			if(Input.GetButton("Run"))
			{
				speed = runSpeed;
			}
			else
			{
				speed = walkSpeed;
			}
			// Translate Input Into A Vector3
			targetVelocity = new Vector3( Input.GetAxis("Horizontal") * sideSpeed, Camera.main.transform.localEulerAngles.y, Input.GetAxis("Vertical") * speed);

			//animator.SetFloat("Speed", Input.GetAxis("Vertical") * speed);
			//animator.SetFloat("SideSpeed", Input.GetAxis("Horizontal")* sideSpeed);

			// Calculate how fast we should be moving
			targetVelocity = transform.TransformDirection(targetVelocity);
			//targetVelocity.x *= speed;
			//targetVelocity.z *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			
			//Clamp All 3 Axis So That Player Does Not Acclerate Faster Than Velocity Change.
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);


			//Don'T Allow Changes In Y Without "Jump" Being Pressed
			velocityChange.y = 0;
			
			//Attempt To Move Object
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			//Jump using "Jump" Button by adding force in y direction.
			if (Input.GetButton("Jump"))
			{
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				//animator.SetBool ("jumping", true);

			}
		}
		else
		{
			//animator.SetBool ("falling", true);
		}
		
		//Manually Add In Gravity
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		isGrounded = false;
	}
	
	float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	//Check If Player Is Grounded
	void OnCollisionStay (Collision collisionInfo)
	{
		isGrounded = true;
	}
}

