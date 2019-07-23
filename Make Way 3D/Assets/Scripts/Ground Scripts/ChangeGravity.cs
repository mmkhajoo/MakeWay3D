using UnityEngine;

public class ChangeGravity : MonoBehaviour {

	// Use this for initialization
	public CameraMove cameraMove;
	void Start ()
	{
		cameraMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
	}
	
	

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Physics.gravity = new Vector3(Physics.gravity.x, -Physics.gravity.y, Physics.gravity.z);
            cameraMove.offset.y = -cameraMove.offset.y;
        }
    }


}
