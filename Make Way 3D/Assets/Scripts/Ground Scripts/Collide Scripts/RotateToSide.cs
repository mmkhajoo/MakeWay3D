using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateToSide : MonoBehaviour
{
    // Start is called before the first frame update
//    public GameObject ballPlayer, startPoint, endPoint;
//
//    public float speedToRotate;
//
//    public float acceleration;
//
//    private bool isStart;
//
//    public float rotationStartPoint, rotationEndPoint;
//
//    private Vector3 temp;
    public bool isLeftSide;

    public Transform rotationCenter;

    public float rotationValue;

    public float posX, posZ;

    public float rotationRadius, angularSpeed, angle;

    public bool isRotate;

    public PlayerMove playerMove;


    private Vector3 temp;

    private bool castOnce;


    void Start()
    {
        castOnce = true;
        isRotate = false;
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotate)
        {
            if (angle >= 1.5f)
            {
//                playerMove.stopForcingBall = false;
                isRotate = false;
                return;
            }

            posZ = Mathf.Sin(angle) * rotationRadius;

            posX = Mathf.Cos(angle) * rotationRadius;

            temp = rotationCenter.position + rotationCenter.TransformDirection(new Vector3(posX, 0, posZ));
            playerMove.transform.position = new Vector3(temp.x, playerMove.transform.position.y, temp.z);


            angle = angle + Time.deltaTime * angularSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (!castOnce)
            {
                return;
            }

            isRotate = true;
//            playerMove.stopForcingBall = true;
           
            StartCoroutine(ChangeBallRotation(other));
            
            castOnce = false;
        }
    }

    public IEnumerator ChangeBallRotation(Collider other)
    {
        for (int i = 0; i < 45; i++)
        {
            other.transform.Rotate(Vector3.up * rotationValue);
            yield return new WaitForEndOfFrame();
        }
    }
}