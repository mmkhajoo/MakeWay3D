using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public BrakeScript brakeScript;

    public Rigidbody rb;

    public float magnitude;

    public float acceleration_smooth;

    public float speed;

    public float startSpeed;

    public float maxSpeed;

    public float startSmooth;

    public float smooth;

    public float maxSmooth;

    public bool isMove;

    public GameObject feild;

    public BoxCollider2D feildCollider;

    public Vector3 temp;

    private float tempVlocity;

    private bool StartInvoke;


    public float jumpRate;

    public float fallMultiplier;

    public float forceDownLessThanThisVlocity;

    private Vector3 moveDirection;

    //--------------------GameOver-------------------------//

    private bool checkGameOverWithStopedBall;

    public float velocity_Manitude_LessThanThis_GameOver;

    public GameManager gameManager;

    // Use this for initialization
    
    //Stop Add Velocity To Ball To Thr Ball
    public bool stopForcingBall;
    private void Start()
    {
        //CancelInvoke("MovePlayer");
        //CreateStartFeild();
        //isMove = true;
        //InvokeRepeating("StartMove",0.5f,0.1f);
//        transform.InverseTransformDirection(transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Time.timeScale == 0)return;
        if (isMove)
        {
            //            if (StartInvoke) return;
            //            InvokeRepeating("MovePlayer", 0f, 0.01f);
            //            StartInvoke = true;
            //Debug.Log("IsBrake" + brakeScript.isBrake);
//            Debug.Log("Rb.velocity.magnitude =  " + rb.velocity.magnitude);
            if (!stopForcingBall)
            {
                MovePlayer();
            }
            else
            {
                smooth = startSmooth;
            }
        }
        else
        {
            /// need to fix
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            if (rb.velocity.y == 0f)
            {
                gameManager.GameOver();
            }

//            speed = startSpeed;
            //Debug.Log("Brake in Player Script");
            //            StartInvoke = false;
        }

        if (rb.velocity.y < forceDownLessThanThisVlocity)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //Debug.DrawRay(transform.position, Vector3.down, Color.red);
        //Debug.DrawRay(transform.position-new Vector3(0,-0.2f,0), Vector3.back, Color.red);
        //Debug.DrawRay(transform.position- new Vector3(0, -0.2f, 0), Vector3.forward, Color.red);
        //if ((!Physics.Raycast(transform.position, Vector3.down, 25f))
        //    && (!Physics.Raycast(transform.position - new Vector3(0, -0.2f, 0), Vector3.back, 50f))
        //    && (!Physics.Raycast(transform.position - new Vector3(0, -0.2f, 0), Vector3.forward, 50f))
        //    && (!Physics.Raycast(transform.position - new Vector3(0, -0.2f, 0), Vector3.left, 50f))
        //    && (!Physics.Raycast(transform.position - new Vector3(0, -0.2f, 0), Vector3.right, 50f)))
        //{
        //    gameManager.GameOver();
        //}
    }

    public void LateUpdate()
    {
        if(Time.timeScale == 0)return;
        InteractRaycast();
        
//        if (!brakeScript.isBrake && (rb.velocity.magnitude < velocity_Manitude_LessThanThis_GameOver))
//        {
//            if (!checkGameOverWithStopedBall)
//            {
//                checkGameOverWithStopedBall = true;
//                StartCoroutine(CheckGameOver());
//            }
//
//            //gameManager.GameOver();
//        }
        if ((rb.velocity.magnitude <= velocity_Manitude_LessThanThis_GameOver))
        {
//            Debug.Log("GameOverWith Velocity Called");
            if (!checkGameOverWithStopedBall)
            {
                if (!stopForcingBall)
                {
                    checkGameOverWithStopedBall = true;
                    StartCoroutine(CheckGameOver());
                }
            }

            //gameManager.GameOver();
        }
    }

    private void InteractRaycast()
    {
        var playerPosition = transform.position;
        var downDirection = Vector3.down;

        var interactionRay = new Ray(playerPosition, downDirection);

        RaycastHit interactionRaycastHit;

        var interactionRayLength = 100f;

        var hitFound = Physics.Raycast(interactionRay, out interactionRaycastHit, interactionRayLength);
        if(!hitFound)
        {
           gameManager.GameOver();
        }
        
    }

    private void MovePlayer()
    {
        //Debug.Log("RB Magnitude : " + rb.velocity.magnitude);
        //        Debug.Log("Time :  " + Time.deltaTime);
        //if (rb.velocity.magnitude < magnitude)
        //{
        //    var velocity = new Vector3(speed, 0, 0);
        //    //Debug.Log("Speed" + speed);
        //    velocity.x = Mathf.Lerp(velocity.x, velocity.x + speed, smooth*Time.deltaTime);
        //    rb.AddForce(velocity);

        //    speed += acceleration;
        //}
        moveDirection = transform.InverseTransformDirection(rb.velocity);

        tempVlocity = Mathf.Lerp(moveDirection.x, maxSpeed, smooth * Time.deltaTime);

//        Debug.Log(Time.deltaTime);
//        
//        Debug.Log("smooth" + smooth * Time.deltaTime);
//
//        Debug.Log("TempVelocity" + tempVlocity);

        moveDirection.x = tempVlocity;
        moveDirection.z = 0;
        //rb.velocity = new Vector3(tempVlocity, rb.velocity.y, rb.velocity.z);

        rb.velocity = transform.TransformDirection(moveDirection);

        smooth += acceleration_smooth;

        //Debug.Log("Speed :  " + speed);
        if (smooth > maxSmooth)
            smooth = maxSmooth;
    }

    public void CreateStartFeild()
    {
        temp = transform.position;
        temp.y -= 2;
        var width = feildCollider.size.x;

        temp.x += width / 2;
        Instantiate(feild, temp, Quaternion.identity);

        temp.x += width;
        Instantiate(feild, temp, Quaternion.identity);

        temp.x += width;
        Instantiate(feild, temp, Quaternion.identity);
    }

    public void StartMove()
    {
        rb.velocity = new Vector3(speed, 0, 0);
    }

    public void SlowBall()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        speed = startSpeed;
    }

    //private void OnCollisionEnter(Collider other)
    //{
    //    Debug.Log("Collision Jump Called");
    //    if (other.gameObject.tag == "JumpObject")
    //    {
    //        Debug.Log("add Force Called");
    //        Vector2 temp = new Vector2(0, jumpRate);
    //        rb.AddForce(temp, ForceMode.Impulse);
    //        Debug.Log("Add Force :" + rb.velocity.magnitude);
    //    }

    //}
    //public void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision Jump Called");
    //    Debug.Log("Object Cllide : " + collision.gameObject.tag);
    //    if (collision.gameObject.tag == "JumpObject")
    //    {
    //        Debug.Log("add Force Called");
    //        Vector2 temp = new Vector2(0, jumpRate);
    //        rb.AddForce(temp, ForceMode.Impulse);
    //        Debug.Log("Add Force :" + rb.velocity.magnitude);
    //    }
    //}

    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 100; i++)
        {
            if ((rb.velocity.magnitude <= velocity_Manitude_LessThanThis_GameOver))
                gameManager.GameOver();
            //yield return new WaitForSeconds(0.2f);
        }

        checkGameOverWithStopedBall = false;
    }

    public void Gameover()
    {
    }
}