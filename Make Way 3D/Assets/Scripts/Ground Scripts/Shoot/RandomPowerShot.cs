using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerShot : MonoBehaviour
{


	public HitTargetPosition hitTargetPosition;

	public float maxPower;

	public float minPower;

	public float speed;

	public float acceleration;

	public float power;

	public Transform sliderBar;

	public bool isStart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isStart)
			SetPower();
	}

	public void SetPower()
	{
		power = Mathf.SmoothStep(maxPower, minPower, speed);
		if (speed >= 1 || speed <= 0)
			acceleration = -acceleration;
		hitTargetPosition.speed = power;
		sliderBar.localScale = new Vector3((power-minPower) / (maxPower-minPower),1f,1f);
		speed += acceleration;

	}
}
