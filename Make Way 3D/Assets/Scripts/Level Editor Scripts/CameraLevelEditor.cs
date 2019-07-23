using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevelEditor : MonoBehaviour {

	// Use this for initialization
	public float rate;

	public MoveCameraButton up;

	public MoveCameraButton down;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (up.isDown)
		{
			Vector3 temp = transform.position;
			temp.y += rate;
			transform.position = temp;
		}
		if (down.isDown)
		{
			Vector3 temp = transform.position;
			temp.y -= rate;
			transform.position = temp;
		}
		
	}
}
