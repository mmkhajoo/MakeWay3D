using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{

    public Rigidbody rb;

    public float speed;

    //public bool isMove;

    public Vector3 temp;

    public float distance;

    public Ground ground;
    void Start()
    {
        temp = transform.position;
        ground.IsMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.timeScale == 0)return;
        if (ground.IsMove)
        {
            rb.isKinematic = false;
            Moving();
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.isKinematic = true;
        }
    }

    public void Moving()
    {
        transform.InverseTransformDirection(transform.position);
        if ((transform.position.y >= temp.y + distance) &&(speed>0f))
        {
            speed = (-speed);
        }
        else if((transform.position.y <= temp.y - distance) && (speed<0f) )
        {
            speed = (-speed);
        }
        transform.TransformDirection(transform.position);
        rb.velocity = new Vector3(0, speed, 0);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log("Collision Called");
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        //Debug.Log("Change Direction");
    //        speed = (-speed);
    //        Moving();
    //    }
    //}
}
