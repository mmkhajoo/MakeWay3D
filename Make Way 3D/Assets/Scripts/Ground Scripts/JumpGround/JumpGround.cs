using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class JumpGround : MonoBehaviour
{
	public Rigidbody groundRb;

	public Ground ground;

	public float castTime;

	public float speedRate;

	private float startJump;

	private float currentY;

	private Vector3 temp;

	private bool castOncForSetStartPosition;
	
	
	
	// Use this for initialization
	void Start ()
	{
		startJump = transform.position.y;
		temp = transform.position;
		ground.IsMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0)return;
		if (!ground.IsMove)
		{
			Jump();
			ground.IsMove = true;
			castOncForSetStartPosition = true;
		}
		else
		{
			if (!castOncForSetStartPosition) return;
			StartCoroutine(BackToStartPosition());
			castOncForSetStartPosition = false;

		}
	}

	public void Jump()
	{
		StartCoroutine(JumpWithVelocity());
//		currentY = Mathf.Lerp(transform.position.y,jumpRange , speedRate * Time.deltaTime);
//
//		temp.y = currentY;
//
//		transform.position = temp;


	}

	IEnumerator JumpWithVelocity()
	{
		groundRb.isKinematic = false;
		var temp = transform.InverseTransformDirection(groundRb.velocity);
		temp = Vector3.up * speedRate;
		groundRb.velocity = transform.TransformDirection(temp);
		yield return new WaitForSeconds(castTime);
		groundRb.isKinematic = true;
	}
	
	private IEnumerator BackToStartPosition()
	{
		yield return new WaitForSeconds(3f);
		transform.position = temp;
	}
}
