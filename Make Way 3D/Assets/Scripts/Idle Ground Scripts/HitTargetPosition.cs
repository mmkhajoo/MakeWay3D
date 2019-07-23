using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTargetPosition : MonoBehaviour
{


	public Transform target;
	
	public float speed;

	public Rigidbody projectileBody;
	
//	public bool start;

	private void Start()
	{
//		ShootToTarget();
	}

//	private void Update()
//	{
//		if (start)
//		{
//			projectileBody.velocity = Vector3.zero;
//			ShootToTarget();
//			start = false;
//		}
//			
//	}

	public void ShootToTarget()
	{
		Vector3 toTarget = target.position - transform.position;
		
		// Set up the terms we need to solve the quadratic equations.
		float gSquared = Physics.gravity.sqrMagnitude;
		float b = speed * speed + Vector3.Dot(toTarget, Physics.gravity);    
		float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

// Check whether the target is reachable at max speed or less.
		if(discriminant < 0f) {
			Debug.Log("Cant Reach The Target");
			// Target is too far away to hit at this speed.
			// Abort, or fire at max speed in its general direction?
		}

		float discRoot = Mathf.Sqrt(discriminant);

// Highest shot with the given max speed:
		float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);

// Most direct shot with the given max speed:
		float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);


//		float middle = 
// Lowest-speed arc available:
		float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f/gSquared));

		float T = T_min;// choose T_max, T_min, or some T in-between like T_lowEnergy

// Convert from time-to-hit to a launch velocity:
			Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

// Apply the calculated velocity (do not use force, acceleration, or impulse modes)
		projectileBody.AddForce(velocity, ForceMode.VelocityChange);
	}
}
