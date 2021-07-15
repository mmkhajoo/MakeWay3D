using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSide : MonoBehaviour
{
    // Start is called before the first frame update
    public RotateToSide rotateToSide;
    
    public CameraMove cameraMove;
    
    private bool castOnce;
    void Start()
    {
        castOnce = true;
        cameraMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (!castOnce)
            {
                return;
            }
            cameraMove.CheckSide(rotateToSide.isLeftSide);
            castOnce = false;
        }
    }
}
