using Ground_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPassed : MonoBehaviour {

    // Use this for initialization
    public GroundsManagerFromLevelEditor groundsManager;

    public ButtonManager buttonManager;
    void Start()
    {
        groundsManager = GameObject.FindGameObjectWithTag("GroundsManager").GetComponent<GroundsManagerFromLevelEditor>();

        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ObjectPassed  Trigger")
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Object Passed");
            //Debug.Log("Player : " + other.gameObject.tag);
            
            //Debug.Log("StopCount : " + groundsManager.stopCount);
            if (buttonManager.permissionToStopObject)
            {
                
                groundsManager.GroundStopMove();
                buttonManager.permissionToStopObject = false;


            }
            groundsManager.DestroyFieldGameObject();
            groundsManager.ChangeGameProgress();
        }
    }
}
