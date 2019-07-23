using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGround : MonoBehaviour
{
    public GameObject pointer;
    public Transform pointerToShot;

    public float pointerDistanceFromBall;

    // Use this for initialization
    public Rigidbody groundRb;

    public FixedJoint fixedJoint;

    public Ground ground;

    public HitTargetPosition hitTargetPosition;

    public float speedRotationRate;

    public float mutipleTargetDistance;

    public bool isShouldRotate;

    private bool isRotate;


    public bool stopForcingBall;

    [SerializeField] private PlayerMove _playerMove;

    private bool castOnce;

    private bool castOnceForStart;

    private Quaternion transformRotation;

    public Vector3 rotation;


    void Start()
    {
        isRotate = false;
        hitTargetPosition.projectileBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        transformRotation.eulerAngles = rotation;
        ground.IsMove = true;
        castOnceForStart = true;
        castOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (isRotate)
        {
            Rotator();
        }

        if (!ground.IsMove)
        {
            RemoveJoint();
            hitTargetPosition.ShootToTarget();
            ground.IsMove = true;
            pointer.SetActive(false);
            isRotate = false;
            castOnce = true;
            castOnceForStart = true;
        }
        else
        {
            if (!castOnceForStart) return;
            StartCoroutine(BackToStartRotation());
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
                FixTargetPositionAndPointer(other.transform, hitTargetPosition.target, mutipleTargetDistance);
                FixTargetPositionAndPointer(other.transform, pointerToShot, pointerDistanceFromBall);
                pointer.SetActive(true);
                _playerMove.stopForcingBall = stopForcingBall;
                if (isShouldRotate)
                    isRotate = true;
                castOnce = false;
            }
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

    public void RemoveJoint()
    {
        Destroy(fixedJoint);
    }

    private void Rotator()
    {
        transform.Rotate(Vector3.back * speedRotationRate);
    }

    private IEnumerator BackToStartRotation()
    {
        yield return new WaitForSeconds(2f);
        transform.rotation = transformRotation;
    }
    
}