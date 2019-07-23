using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    // Use this for initialization
    public float rotation;

    public bool x;
    public bool y;
    public bool z;

    private Vector3 temp;

    //public bool isMove;

    public Ground ground;

    private void Start()
    {
        if(x)
            temp = Vector3.left;
        else if(y)
            temp = Vector3.down;
        else if(z)
        {
            temp = Vector3.back;
        }
        RandomRotate();
        //isMove = true;
        ground.IsMove = true;

    }

    // Update is called once per frame
    private void Update()
    {
        if(Time.timeScale == 0)return;
        //Debug.Log("Quaternion : " + transform.rotation.z);
        if (ground.IsMove)
            Rotator();
    }

    private void Rotator()
    {
        transform.Rotate(temp * rotation);
    }

    private void RandomRotate()
    {
        
        transform.Rotate(temp* CreatRandomRange(100 ,0));

    }

    private int CreatRandomRange(int max, int min)
    {
        int randomCount = Random.Range(2000, 70000);

        randomCount = randomCount % (max - min + 1);

        return min + randomCount;

    }
}
