using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMinMax : MonoBehaviour {

    public float Rate;

    public float Scale;

    public float speed;

    //public float smooth;
	
	//public bool isMove;

    private Vector3 temp;

    public Ground ground;

	// Use this for initialization
	void Start () {
        temp = transform.localScale;
        Scale = transform.localScale.x;
        ground.IsMove = true;
    }

    // Update is called once per frame
    void Update () {
        if(Time.timeScale == 0)return;
        if(ground.IsMove)
        {
            Move();
        }
		
	}

	private void Move()
    {
        temp.x = Mathf.Lerp(0, Scale, speed);
        transform.localScale = temp;
        speed += Rate;
        if (speed >= 1 || speed < 0)
        {
            Rate = (-Rate);
        }
    }
}
