using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ground_Scripts
{
    public class GroundsManager : MonoBehaviour
    {
        //Name Of The GameObjects
        [SerializeField] private string[] gameObjectsName;

        [SerializeField] private string[] gameObjectWithSpecialThings;


        public int[] GroundsMap;

        public GameObject Player;

        public GameObject[] grounds;

        public int[] groundSteps;
        //public List<Rotate> rotates;

        //public List<RightLeft> rightLefts;

        //public List<SizeMinMax> sizeMinMaxes;

        //public List<UpDown> upDowns;

        //public List<UpDownBallStop> upDownBallStops;

        //public List<UpDownBallStop2> upDownBallStop2s;

        //public BoxCollider[] FeildsCollider;

        public Transform tempLocation;

        public Vector3 previousLocation;

        public int[] sidesForGrounds;

        private int tempLocationRotation;

        //public float width;


        //Count How Many gameObjets Are Stoped
        public int stopCount;

        //for destroy gameObject Start From This Value
        public int destroyCounter;

        public int startObjectToDestroyFrom;

        //private bool[] stopedGrounds;

        //public GameObject[] FieldGameObjects;

        private CreatGrounds creatGrounds;


        //detect when gravity changed
        private bool isGravityChange;

        // Use this for initialization
        private void Start()
        {
            //Controll The Gravity At Start
            Physics.gravity = new Vector3(0f, -9.8f, 0f);

            //Instantiate(Grounds[GroundsMap[0]], new Vector3(0,0,0), Quaternion.identity);
            //tempLocation = new Vector3(0, Grounds[0].transform.GetChild(2).position.y,0);
            tempLocation.position = Player.transform.position;
            //            tempLocation.position -= new Vector3(1, 0, 0);
            tempLocation.position -= new Vector3(0, 5, 0);
            tempLocationRotation = 0;
            //width = GroundCollider.size.x;
            //stopedGrounds = new bool[GroundsMap.Length];
            //FieldGameObjects = new GameObject[GroundsMap.Length];
            //---------------------counting steps-------------------------//
            int steps = 0;
            for (int i = 0; i < GroundsMap.Length; i++)
            {
                steps += groundSteps[GroundsMap[i]];
            }

            //---------------------------End------------------------------//

            creatGrounds = new CreatGrounds(GroundsMap.Length, steps);
            isGravityChange = false;
            CreateGrounds();
            stopCount = 0;
            destroyCounter = 0;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void CreateGrounds()
        {
            GameObject tempObj;

            int groundScriptCounter = 0;

            for (var i = 0; i < GroundsMap.Length; i++)
            {
                tempObj = Instantiate(grounds[GroundsMap[i]], tempLocation.position, new Quaternion(0, 0, 0, 0));
                float boxColliderTemp = tempObj.GetComponent<BoxCollider>().bounds.size.x;
                //--------------------------Sing GameObejcts and Script to Class To Stop Them----------------//
                creatGrounds.fieldGameObjects[i] = tempObj;

                for (int j = 0; j < groundSteps[GroundsMap[i]]; j++)
                {
                    creatGrounds.groundScripts[groundScriptCounter] =
                        tempObj.transform.Find("MovingObj" + j).GetComponent<Ground>();
                    groundScriptCounter++;
                }


                //-------------------------End  Sing GameObejcts and Script to Class To Stop Them-----------------------------------------//

                tempLocation.transform.Rotate(new Vector3(0, tempLocationRotation, 0));

                previousLocation = tempLocation.InverseTransformDirection(tempLocation.position);

                //Debug.Log(tempLocation.position);

                //                Debug.Log(tempObj.GetComponent<BoxCollider>().bounds.size.x);

                previousLocation += new Vector3(boxColliderTemp / 2, 0f, 0f);

                tempLocation.position = tempLocation.TransformDirection(previousLocation);

                //Debug.Log(tempLocation.position);

                tempObj.transform.position = tempLocation.position;

                previousLocation += new Vector3(boxColliderTemp / 2, 0f, 0f);

                tempLocation.position = tempLocation.TransformDirection(previousLocation);

                //tempObj.transform.Rotate(new Vector3(0, tempLocationRotation, 0));

                //tempObj.transform.rotation = tempLocation.rotation;


                //--------------------Actions For GameObject-------------------------//
                tempObj.transform.Rotate(new Vector3(0, tempLocationRotation, 0));

                ActionsForGameObject(tempObj);

                CheckSpecialThingsForSomeGameObjects(gameObjectsName[GroundsMap[i]]);


                //--------------------Actions For GameObject------------------------//

                //if (isGravityChange)
                //{
                //    tempLocation.Rotate(new Vector3(180, 0, 0));

                //    tempObj.transform.Rotate(new Vector3(180, 0, 0));

                //    isGravityChange = false;
                //}

                //Debug.Log(tempLocation.position);

                //}
                tempLocation.Rotate(new Vector3(0, -tempLocationRotation, 0));
                tempLocation.position = new Vector3(tempLocation.position.x,
                    tempObj.transform.Find("EndGround").transform.position.y, z: tempLocation.position.z);
                //Debug.Log(tempObj.transform.Find("EndGround").transform.rotation.x);
                //if (tempObj.transform.Find("EndGround").transform.rotation.x > 0)
                //{
                //    //tempLocation.rotation = new Quaternion(1, tempLocation.rotation.y, tempLocation.rotation.z, tempLocation.rotation.w);
                //    isGravityChange = true;
                //    tempLocation.Rotate(new Vector3(-180, 0, 0));
                //    //tempObj.transform.Rotate(new Vector3(-180, 0, 0));
                //}

                //else
                //    tempLocation.rotation = new Quaternion(0, tempLocation.rotation.y, tempLocation.rotation.z, tempLocation.rotation.w);

                tempLocationRotation = CreateRandomSideForGrounds();
            }
        }

        
        public void DestroyFieldGameObject()
        {
            if (startObjectToDestroyFrom <= destroyCounter)
                Destroy(creatGrounds.fieldGameObjects[destroyCounter - startObjectToDestroyFrom]);
            destroyCounter++;
        }

        public void GroundStopMove()
        {
            creatGrounds.groundScripts[stopCount].IsMove = false;

            stopCount++;
        }

        public class CreatGrounds
        {
            public GameObject[] fieldGameObjects;

            public Ground[] groundScripts;

            public CreatGrounds(int gameObjectNumbers, int steps)
            {
                fieldGameObjects = new GameObject[gameObjectNumbers];
                groundScripts = new Ground[steps];
            }
        }

        public int CreateRandomSideForGrounds()
        {
            int randomCount = Random.Range(2000, 70000);

            randomCount = randomCount % 2;
            //            Debug.Log("RandomCount" + randomCount);
            //            Debug.Log("SidesForGround     " + sidesForGrounds[randomCount]);
            return sidesForGrounds[randomCount];
        }

        public int CheckAngle(int rotation, int rotate)
        {
            while (rotation < 1 && rotation > -1)
            {
                rotation = rotation / rotate;
            }

            if (rotation == 1 || rotation == -1)
                return rotation;
            return 0;
        }

        public void CheckSpecialThingsForSomeGameObjects(string objName)
        {
            for (int i = 0; i < gameObjectWithSpecialThings.Length; i++)
            {
                if (gameObjectWithSpecialThings[i].Equals(objName))
                    StartCoroutine(objName);
            }
        }

        //---------------------------Special Things For GameObjects  Functions -----------------------------------//
        public void ChangeGravity()
        {
            isGravityChange = !isGravityChange;
        }

        IEnumerator ChangeGravityField()
        {
            ChangeGravity();
            Debug.Log("Change Gravity Called");
            yield return new WaitForSeconds(0f);
        }

        //----------------------------- End   Special Things For GameObjects  Functions-------------------------------//

        public void ActionsForGameObject(GameObject gameObject)
        {
            if (isGravityChange)
                gameObject.transform.Rotate(new Vector3(180, 0, 0));
        }
    }
}