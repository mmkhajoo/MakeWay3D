using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BrakeScript : MonoBehaviour , IPointerUpHandler , IPointerDownHandler
{

	public bool isBrake;
	public Rigidbody rd;
	
	public void OnPointerUp(PointerEventData eventData)
	{
		isBrake = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		rd.velocity = new Vector3(0,rd.velocity.y,0);
        //Debug.Log("BrakeScript Called");
		isBrake = true;
	}
}
