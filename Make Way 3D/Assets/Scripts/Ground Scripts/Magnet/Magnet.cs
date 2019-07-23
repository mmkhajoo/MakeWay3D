using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

	
    // Use this for initialization
    public Rigidbody groundRb;

    public FixedJoint fixedJoint;

    public Ground ground;

    public bool stopForcingBall;
    
    [SerializeField] private PlayerMove _playerMove;

    private bool castOnce;

    private bool ismagnetOn;

    public float strengthMagnet;

    private Transform playerTransform;

    private Rigidbody playerRb;

    private bool castOnceForStart;


    void Start()
    {
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        ground.IsMove = true;
        castOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (ismagnetOn)
        {
            playerRb.AddForce((transform.position - playerTransform.position)*strengthMagnet * Time.smoothDeltaTime);
        }
        if (!ground.IsMove)
        {
            RemoveJoint();
            ground.IsMove = true;
            ismagnetOn = false;
            playerRb.velocity = Vector3.zero;
            castOnceForStart = true;
        }
        else
        {
            if (!castOnceForStart) return;
            ismagnetOn = false;
            castOnce = true;
            castOnceForStart = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (castOnce)
            {
                fixedJoint = other.gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = groundRb;
//			hitTargetPosition.target = FixTargetPosition(other.transform.position);
                _playerMove.stopForcingBall = stopForcingBall;
                castOnce = false;
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ismagnetOn = true;
        }
    }


    public void RemoveJoint()
    {
        Destroy(fixedJoint);
    }

    
}
