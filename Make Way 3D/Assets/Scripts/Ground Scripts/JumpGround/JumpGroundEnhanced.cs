using System.Collections;
using UnityEngine;

public class JumpGroundEnhanced : MonoBehaviour
{
    public PlayerMove playerMove;

    public Rigidbody groundRb;

    public Rigidbody playerRb;

    public Ground ground;

    public float castTime;

    public float speedRate;

    public bool stopTheBallOnce;

    public bool isBallMove;

    public bool withNextStop;

    public Vector3 nextStopPosition;

    private bool castOnceForSetStartPosition;

    private bool castOnce;

    public bool X, Y, Z;

    public Vector3 startPosition;

    private GameManager _gameManager;

    private bool permissionCheckGameOver;


//    private Vector3 tempPosition;

    // Use this for initialization
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        nextStopPosition = nextStopPosition + transform.localPosition;
        startPosition = transform.position;
        castOnceForSetStartPosition = true;
        castOnce = true;
        ground.IsMove = true;
        permissionCheckGameOver = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (withNextStop)
        {
            StartCoroutine(StopObjectWithNextStop());
        }

        if (!ground.IsMove)
        {
            if (!castOnce) return;
            if (withNextStop)
                JumpWithNextStopAndIsBallMove();
            else
                Jump();
            castOnceForSetStartPosition = true;
            castOnce = false;
        }

        else
        {
            if (castOnceForSetStartPosition)
            {
                transform.position = startPosition;
                castOnceForSetStartPosition = false;
                castOnce = true;
            }
        }
    }

    public void Jump()
    {
        StartCoroutine(JumpWithVelocity());
    }

    public void JumpWithNextStopAndIsBallMove()
    {
        if (!isBallMove)
        {
            playerMove.stopForcingBall = true;
        }

        groundRb.isKinematic = false;
        var temp = transform.InverseTransformDirection(groundRb.velocity);
        temp = Vector3.up * speedRate;
        groundRb.velocity = transform.TransformDirection(temp);
    }

    IEnumerator JumpWithVelocity()
    {
        if (!isBallMove)
        {
            playerMove.stopForcingBall = true;
            playerRb.velocity = Vector3.zero;
        }

        groundRb.isKinematic = false;
        var temp = transform.InverseTransformDirection(groundRb.velocity);
        temp = Vector3.up * speedRate;
        groundRb.velocity = transform.TransformDirection(temp);
        yield return new WaitForSeconds(castTime);
        groundRb.isKinematic = true;
        yield return new WaitForSeconds(1f);
        permissionCheckGameOver = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (stopTheBallOnce)
            {
                playerRb.velocity = Vector3.zero;
                stopTheBallOnce = false;
            }

            if (permissionCheckGameOver)
                if (!ground.IsMove)
                {
                    _gameManager.GameOver();
                }

//            if(isBallMove)
//                playerMove.stopForcingBall = false;
        }
    }

    public Vector3 SetNextStopPosition(Vector3 nextStop)
    {
        var tempLocation = transform.InverseTransformDirection(transform.position);
        tempLocation += nextStop;
        return transform.TransformDirection(tempLocation);
    }


    public IEnumerator StopObjectWithNextStop()
    {
        if (X)
        {
            if (transform.localPosition.x >= nextStopPosition.x)
            {
                groundRb.isKinematic = true;
                yield return new WaitForSeconds(1f);
                permissionCheckGameOver = true;
            }
        }
        else if (Y)
        {
            if (transform.localPosition.y >= nextStopPosition.y)
            {
                groundRb.isKinematic = true;
                yield return new WaitForSeconds(1f);
                permissionCheckGameOver = true;
            }
        }
        else if (Z)
        {
            if (transform.localPosition.z >= nextStopPosition.z)
            {
                groundRb.isKinematic = true;
                yield return new WaitForSeconds(1f);
                permissionCheckGameOver = true;
            }
        }
    }
}