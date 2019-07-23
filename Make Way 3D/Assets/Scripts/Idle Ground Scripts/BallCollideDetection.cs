using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollideDetection : MonoBehaviour
{

	public HitTargetPosition hitTargetPosition;
	// Use this for initialization
	void Start ()
	{
		hitTargetPosition.projectileBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			hitTargetPosition.projectileBody.velocity = Vector3.zero;
			hitTargetPosition.ShootToTarget();
		}
	}
}
