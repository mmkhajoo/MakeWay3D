using System;
using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using Random = UnityEngine.Random;
using CodeStage.AntiCheat.ObscuredTypes;

namespace Ground_Scripts
{
    public class GroundsManagerFromLevelEditor : MonoBehaviour
    {
        public GameManager gameManager;

        public UiManager uiManager;

        public CameraMove cameraMove;

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

        public CreatGrounds creatGrounds;


        //detect when gravity changed
        private bool isGravityChange;


        //Level Come From LevelEditor
        [SerializeField] public SaveableLevel_SaveLoad levelSaveLoad;

        public Levels levels;

        public ResourcesManager resourcesManager;


        // how much distance for start
        public float startDistanceBallForFirstGround;

        // Use this for initialization
        private void Start()
        {
            Player.transform.position = Vector3.forward;
            levelSaveLoad = levels.levels[ObscuredPrefs.GetInt("LevelNumber", 0)];
//            ObscuredPrefs.SetInt("LevelNumber",0);
            //Controll The Gravity At Start
//            Physics.gravity = new Vector3(0f, -9.8f, 0f);


            //---------------------counting steps-------------------------//
            int steps = 0;

            foreach (var VARIABLE in levelSaveLoad.saveableLevelObjects_List)
            {
                steps += resourcesManager.GetObjBase(VARIABLE.obj_id).steps;
            }

            //---------------------------End------------------------------//
            creatGrounds = new CreatGrounds(levelSaveLoad.saveableLevelObjects_List.Count, steps);
            isGravityChange = false;
            CreateGrounds();
            stopCount = 0;
            destroyCounter = 0;
            if (!ObscuredPrefs.GetBool("Revive"))
            {
//                SetBallPosition(Player, creatGrounds.fieldGameObjectsList[0]);
                gameManager.isGameStart = false;
                SetBallPosition();
                cameraMove.SetCameraPosition();
                // Set Camera Position
            }
            else
            {
                gameManager.isGameStart = true;
                destroyCounter = ObscuredPrefs.GetInt("DestroyCounter");
                uiManager.ChangeGameProgressInstantly(((destroyCounter - 1f)/(creatGrounds.fieldGameObjectsList.Count - 1f))*1f);
                Debug.Log("DestroyCounter" + destroyCounter);
//                SetBallPosition(Player,
//                    creatGrounds.fieldGameObjectsList[destroyCounter]);
                SetBallPosition();
                cameraMove.SetCameraPosition();
                stopCount = ObscuredPrefs.GetInt("Steps");
                //Set Camera Position
                ObscuredPrefs.SetBool("Revive", false);
            }
        }

        public void SetBallPosition()
        {
            if (gameManager.isGameStart)
            {
                SetBallPosition(Player, creatGrounds.fieldGameObjectsList[destroyCounter]);
                Debug.Log("ResumeGame Called");
            }
            else
            {
                SetBallPosition(Player, creatGrounds.fieldGameObjectsList[0]);
            }
        }


        // Update is called once per frame
        void Update()
        {
        }

        public void CreateGrounds()
        {
            GameObject tempObj;

            int groundScriptCounter = 0;

            foreach (var levelObject in levelSaveLoad.saveableLevelObjects_List)
            {
                tempObj = Instantiate(resourcesManager.GetObjBase(levelObject.obj_id).objPrefab,
                    new Vector3(levelObject.posX, levelObject.posY, levelObject.posZ),
                    Quaternion.Euler(levelObject.rotX, levelObject.rotY, levelObject.rotZ));
                creatGrounds.fieldGameObjectsList.Add(tempObj);

                for (int j = 0; j < resourcesManager.GetObjBase(levelObject.obj_id).steps; j++)
                {
                    creatGrounds.groundScripts[groundScriptCounter] =
                        tempObj.transform.Find("MovingObj" + j).GetComponent<Ground>();
                    groundScriptCounter++;
                }

//                 ActionsForGameObject(tempObj);
//
//                 CheckSpecialThingsForSomeGameObjects(levelObject.obj_id);
            }
        }

        public void SetBallPosition(GameObject player, GameObject startField)
        {
//            Vector3 temp = startField.GetComponent<Transform>().position;
            Vector3 temp;
//           Debug.Log(startField.transform.position);
//           Debug.Log(startField.name);
            temp = startField.transform.InverseTransformDirection(startField.transform.position);

//          Debug.Log(temp);
            temp.x -= startDistanceBallForFirstGround;

            temp.y += 5f;
        //    Debug.Log(tempLocation.position);

            //                Debug.Log(tempObj.GetComponent<BoxCollider>().bounds.size.x);

            temp = startField.transform.TransformDirection(temp);

//            Debug.Log(temp);

            player.transform.position = temp;
//            Debug.Log(player.transform.position);
        }

        public void DestroyFieldGameObject()
        {
            if (startObjectToDestroyFrom <= destroyCounter)
                Destroy(creatGrounds.fieldGameObjectsList[destroyCounter - startObjectToDestroyFrom]);
            destroyCounter++;
        }

        public void GroundStopMove()
        {
            creatGrounds.groundScripts[stopCount].IsMove = !creatGrounds.groundScripts[stopCount].IsMove;

            stopCount++;
        }

        public void ChangeGameProgress()
        {
            StartCoroutine(uiManager.ChangeGameProgressSlider(((destroyCounter - 1f)/(creatGrounds.fieldGameObjectsList.Count - 1f))*1f,
                ((destroyCounter)/(creatGrounds.fieldGameObjectsList.Count - 1f))*1f
                ));
        }

        [Serializable]
        public class CreatGrounds
        {
            public GameObject[] fieldGameObjects;

            public List<GameObject> fieldGameObjectsList;

            public Ground[] groundScripts;

            public CreatGrounds(int gameObjectNumbers, int steps)
            {
                fieldGameObjectsList = new List<GameObject>();
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
                    StartCoroutine(objName + "Field");
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
            {
                gameObject.transform.Rotate(new Vector3(180, 0, 0));
//                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
//                    -gameObject.transform.position.y,
//                    gameObject.transform.position.z);
            }
        }
    }
}