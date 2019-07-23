using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForcingCollider : MonoBehaviour
{

	[SerializeField] private PlayerMove _playerMove;

	public bool stopForcingBall;
	// Use this for initialization
	void Start ()
	{
		_playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			_playerMove.stopForcingBall = stopForcingBall;
		}
	}
}
