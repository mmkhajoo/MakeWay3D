using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopObject : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

	public ButtonManager buttonManager;

	public bool castOnce;

	private void Start()
	{
		castOnce = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		castOnce = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{

		if (castOnce)
		{
			buttonManager.StopMovingObj();
			castOnce = false;
		}
	}
}
