using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBallStop : MonoBehaviour
{

    public UpDownBallStop2 ballStop2;

    public PlayerMove playerMove;

    public Rigidbody rb;

    public float speed;

    public GameObject downGround;

    public float maxUp;

    public float maxDown;

    //public bool isMove;

    public bool isPlayerMove;

    public Ground ground;

    private Vector3 startPosition;

    private bool castOnce;
    void Start()
    {
        maxUp = downGround.transform.position.y + maxUp;

        maxDown = downGround.transform.position.y - maxDown;

        startPosition = transform.position;

        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        ground.IsMove = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Time.timeScale == 0)return;
        if (ground.IsMove)
        {
            if (castOnce)
            {
                transform.position = startPosition;
                castOnce = false;
            }
            isPlayerMove = false;
            ballStop2.ground.IsMove = false;
            Moving();
        }
        else
        {
            if (isPlayerMove) return;
            //Debug.Log("Ball Stop 2 Called");
            ballStop2.ground.IsMove = true;
            playerMove.isMove = false;
            isPlayerMove = true;
            castOnce = true;
        }

    }

    private void Moving()
    {
        //Must Be Checked
        transform.InverseTransformDirection(transform.position);
        if ((transform.position.y > maxUp) && (speed >= 0))
        {
            speed = (-speed);
        }
        else if ((transform.position.y <= maxDown) && (speed < 0))
        {
            speed = (-speed);
        }
        transform.TransformDirection(transform.position);
        rb.velocity = new Vector3(0, speed, 0);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Collision Called");
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        Debug.Log("Change Direction");
    //        speed = (-speed);
    //        Moving();
    //    }
    //}
}
