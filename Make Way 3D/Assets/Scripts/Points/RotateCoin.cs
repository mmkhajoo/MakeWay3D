using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{

    public int XRotate, YRotate, ZRotate;

    private Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = new Vector3(XRotate,YRotate,ZRotate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(temp);
    }
}
