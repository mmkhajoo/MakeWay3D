using UnityEngine;

namespace Ground_Scripts
{
    public class GroundCollector : MonoBehaviour
    {

        public GameObject feild;

        public GameObject[] feilds;

        public float lastX;
        
        
        private void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("Trigger Called For Destroy");
            //Debug.Log(collision.gameObject.tag);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}