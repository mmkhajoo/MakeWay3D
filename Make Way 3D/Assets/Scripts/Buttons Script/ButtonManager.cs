using System.Collections;
using System.Collections.Generic;
using Ground_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public GroundsManagerFromLevelEditor groundsManager;

    public bool permissionToStopObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StopMovingObj()
    {
        //Debug.Log("PermissionToStop :  "+permissionToStopObject);
        if(permissionToStopObject)
        {
            groundsManager.GroundStopMove();
            permissionToStopObject = false;
        }
        //GameObject[] rotators = GameObject.FindGameObjectsWithTag("Rotator");
        //Debug.Log("Count : " + rotators.Length);
        //Rotate rotator = null;
        //for(int i=0; i<rotators.Length;i++)
        //{
        //    rotator = rotators[i].GetComponent<Rotate>();
        //    if(rotator.isMove)
        //    {
        //        Debug.Log("Stoping Rotate : " + i);
        //        break;
        //    }
        //}
        //if (rotator != null)
        //{
        //    rotator.isMove = false;
        //}
    }

    public void Restart()
    {
        SceneManager.LoadScene("Core");
    }

    public void TimeScale1()
    {
        Time.timeScale = 1;
    }
}
