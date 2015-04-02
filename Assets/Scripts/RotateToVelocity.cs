using UnityEngine;
using System.Collections;

public class RotateToVelocity: MonoBehaviour
{
	void FixedUpdate()
	{
		transform.forward += Vector3.Lerp(transform.forward, Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))),Time.deltaTime * 20);
	}

}