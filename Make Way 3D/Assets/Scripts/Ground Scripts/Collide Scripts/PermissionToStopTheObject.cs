using System;
using Ground_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionToStopTheObject : MonoBehaviour
{
    // Use this for initialization

    public ButtonManager buttonManager;

    public Rigidbody playerRb;

    public PlayerMove playerMove;

    public Transform playerTransform;

    public bool isChangeBallRotation;

    public bool isPermissionToStop;

    private void Awake()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag.Equals("Player"))
        {
            if (isChangeBallRotation)
            {
                playerTransform.rotation = transform.rotation;

            }

            if (isPermissionToStop)
            {
                 buttonManager.permissionToStopObject = true;
                 playerMove.StartParticle();
            }
               
            isPermissionToStop = false;
        }
        BackTheBallToMiddle();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (isChangeBallRotation)
            {
                playerTransform.rotation = transform.rotation;

            }

            if (isPermissionToStop)
            {
                buttonManager.permissionToStopObject = true;
                playerMove.StartParticle();
            }
        }
    }

    public void BackTheBallToMiddle()
    {
        var tempLocation = playerTransform.InverseTransformDirection(playerRb.transform.position);
        var tempOwnLocation = playerTransform.InverseTransformDirection(transform.position);
        
        tempLocation.z = tempOwnLocation.z;

        playerRb.transform.position = playerTransform.TransformDirection(tempLocation);
    }
}