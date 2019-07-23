using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour {
	
	public Rigidbody playerRb;
	
	public Ground ground;

	public float strength;

	public Vector3 direction;

	public bool isWindZone;

	public bool isInWindZone;

	private bool oneTimeVelocityZero;
	// Use this for initialization
	void Start ()
	{
		playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
		isWindZone = false;
		isInWindZone = false;
		oneTimeVelocityZero = true;
		ground.IsMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0)return;
		if(isWindZone)
			Wind();
		if (!ground.IsMove)
		{
			
			if (isInWindZone)
				isWindZone = true;
		}
	}

	public void Wind()
	{
		if (oneTimeVelocityZero)
		{
			playerRb.velocity = Vector3.zero;
			oneTimeVelocityZero = false;
		}
			
		playerRb.AddForce(direction*strength);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			if (!ground.IsMove)
			{
				isWindZone = true;
			}

			isInWindZone = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			if (!ground.IsMove)
			{
				isWindZone = false;
				isInWindZone = false;
			}
		}
	}
}
