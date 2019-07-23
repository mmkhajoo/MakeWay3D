using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBallStop2 : MonoBehaviour
{

    public Rigidbody rb;

    public PlayerMove playerMove;

    public float speed;

    public GameObject upGround;

    public GameObject movingObject;

    public float maxUp;

    public float maxDown;

    //public bool isMove;

    public bool ballMoved;

    public UpDownBallStop ballStop;

    public Ground ground;

    private void Start()
    {
        maxUp = upGround.transform.position.y + maxUp;

        maxDown = upGround.transform.position.y - maxDown;

        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(Time.timeScale == 0)return;
        if (ground.IsMove)
        {
            rb.isKinematic = false;
            Moving();
        }
        else
        {
            if (!ballStop.isPlayerMove) return;
            if (ballMoved) return;
            //Debug.Log("Player Come To Move Again!!");
            rb.velocity = new Vector3(0, 0, 0);
            rb.isKinematic = true;
            playerMove.isMove = true;
            ballMoved = true;
        }
    }

    private void Moving()
    {
        transform.InverseTransformDirection(transform.position);
        if ((movingObject.transform.position.y > maxUp) && (speed >= 0))
        {
            speed = (-speed);
        }
        else if ((movingObject.transform.position.y <= maxDown) && (speed < 0))
        {
            speed = (-speed);
        }
        transform.TransformDirection(transform.position);
        rb.velocity = new Vector3(0, speed, 0);
    }
}
