using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private bool isMove;

    public bool IsMove
    {
        get
        {
            return isMove;
        }

        set
        {
            isMove = value;
        }
    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
