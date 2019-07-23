using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightLeft : MonoBehaviour
{
    // Use this for initialization
    public Rigidbody rb;

    public float speed;

    public DOTweenPath doTweenPath;

    //public bool isMove;

    private Vector3 direction;

    public Ground ground;

    private bool castOnce;

    private bool castOnce2;
    
    //SetWAyPoints With Script
    public float duration;

    public PathType pathType = PathType.Linear;

    public Tween tween;
    
    public Vector3[] wayPoints;

    void Start()
    {
        Moving();
        ground.IsMove = true;
        castOnce = true;
        SetWayPoint(wayPoints);
        tween = transform.DOPath(wayPoints, duration, pathType);
        tween.SetLoops(-1).SetEase(Ease.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        if (!ground.IsMove)
        {
            if (castOnce)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = true;
//                doTweenPath.DOPause();
                transform.DOPause();
//                Destroy(waypointMover);
                castOnce = false;
                castOnce2 = true;
            }
        }
        else
        {
            if (castOnce2)
            {
                transform.DOPlay();
                rb.isKinematic = false;
                castOnce = true;
                castOnce2 = false;
            }
        }

//        else
//        {
//            Moving();
//        }
    }

    public void Moving()
    {
        direction = transform.InverseTransformDirection(rb.velocity);
        direction.x = speed;
        rb.velocity = transform.TransformDirection(direction);
    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        //Debug.Log("Collision Called");
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            //Debug.Log("Change Direction");
//            speed = (-speed);
//            Moving();
//        }
//    }

    public void SetPosition(Transform posGameObject, Transform target)
    {
        var tempLocation = posGameObject.InverseTransformDirection(posGameObject.transform.position);
        var tempOwnLocation = posGameObject.InverseTransformDirection(target.position);

        tempLocation.x = tempOwnLocation.x + 2f;

        posGameObject.transform.position = posGameObject.TransformDirection(tempLocation);
    }

    public void SetWayPoint(Vector3[] waypoints)
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            var tempLocation = transform.InverseTransformDirection(transform.position);
            tempLocation += waypoints[i];

            waypoints[i] = transform.TransformDirection(tempLocation);
        }
    }
}