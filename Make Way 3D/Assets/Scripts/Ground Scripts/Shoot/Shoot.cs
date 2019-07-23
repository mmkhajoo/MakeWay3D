using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject pointer;

    public Transform pointerToShot;

    public float pointerDistanceFromBall;

    // Use this for initialization
    public Rigidbody groundRb;

    public FixedJoint fixedJoint;

    public Ground ground;

    public HitTargetPosition hitTargetPosition;


    public float mutipleTargetDistance;

    private bool castOnce;

    public RandomPowerShot randomPowerShot;


    void Start()
    {
        hitTargetPosition.projectileBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        ground.IsMove = true;
        castOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        if (!ground.IsMove)
        {
            if (castOnce)
            {
                randomPowerShot.isStart = false;
                hitTargetPosition.ShootToTarget();
                castOnce = false;
            }
        }
        else
        {
            castOnce = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
//            if (castOnce)
//            {
//                fixedJoint = other.gameObject.AddComponent<FixedJoint>();
//                fixedJoint.connectedBody = groundRb;
//			hitTargetPosition.target = FixTargetPosition(other.transform.position);
            randomPowerShot.isStart = true;
            FixTargetPositionAndPointer(other.transform, hitTargetPosition.target, mutipleTargetDistance);
//                FixTargetPositionAndPointer(other.transform , pointerToShot , pointerDistanceFromBall);
//                pointer.SetActive(true);
//            }
        }
    }

    public void FixTargetPositionAndPointer(Transform startPosition, Transform target, float distance)
    {
        target.position = startPosition.position;

        Vector3 temp = transform.InverseTransformDirection(target.position);

        temp += Vector3.up * distance;

        temp = transform.TransformDirection(temp);

        target.position = temp;
    }
}