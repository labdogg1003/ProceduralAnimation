using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour
{
	public GameObject Lifter;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision other)
	{

			Physics.IgnoreCollision(Lifter.GetComponent<SphereCollider>(), GetComponent<SphereCollider>());

	}
}
