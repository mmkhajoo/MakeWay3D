using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour {

    public Rigidbody playerrb;

    public float jumpRate;


	// Use this for initialization
	void Start () {
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Jump Called");
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("add Force Called");
            Vector3 temp = Vector3.up * jumpRate;
            playerrb.AddForce(-temp*Mathf.Sign(Physics.gravity.y), ForceMode.Impulse);
            //Debug.Log("Add Force :" + playerrb.velocity.magnitude);
        }

    }
}
