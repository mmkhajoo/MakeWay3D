using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallDirection : MonoBehaviour {

	// Use this for initialization
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Permission To Stop Called");
		//Debug.Log("Player : " + other.gameObject.tag);
		if(other.gameObject.tag.Equals("Player"))
		{
			other.gameObject.transform.rotation = transform.rotation;
		}
	}
}
