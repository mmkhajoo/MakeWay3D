using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningDetection : MonoBehaviour {

	// Use this for initialization

	public GameManager gameManager;

	public PlayerMove playerMove;

	private void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Permission To Stop Called");
		Debug.Log("Player : " + other.gameObject.tag);
		if (!other.gameObject.tag.Equals("Player")) return;
		gameManager.Win = true;
		playerMove.isMove = false;
		gameManager.WinFunction();
	}
}
