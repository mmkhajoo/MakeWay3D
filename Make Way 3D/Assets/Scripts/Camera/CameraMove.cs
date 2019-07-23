using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public GameObject ball;
    
    public bool gameover;
    
    public float lerprate;
    
    
    public Vector3 offset;

    private Quaternion rotation;

    private float desireYAngle;

    public GameManager gameManager;
    // Use this for initialization
    private void Awake()
    {
        offset = ball.transform.position - transform.position;
        gameover = false;
        Follow();
        
    }

    // Update is called once per frame4
    private void FixedUpdate()
    {
        if(Time.timeScale == 0)return;
        if (!gameover)
        {
            Follow();
        }

    }

    private void Follow()
    {
        //Debug.Log("Time.DeltaTime :     " + Time.deltaTime);
        desireYAngle = ball.transform.eulerAngles.y;
        rotation = Quaternion.Euler(transform.rotation.x, desireYAngle,transform.rotation.z);
        var pos = transform.position;
        var targetpos = ball.transform.position - rotation* offset ;
        pos = Vector3.Lerp(pos, targetpos, lerprate * Time.deltaTime);
        transform.position = pos;
        transform.LookAt(ball.transform);

    }

    public void GameOver()
    {
        gameover = true;
    }

    public void SetCameraPosition()
    {
        transform.position = ball.transform.position - offset;
    }
    
}
