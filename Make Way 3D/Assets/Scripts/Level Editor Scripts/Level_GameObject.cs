using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Level_GameObject : MonoBehaviour
{
    // Use this for initialization
    public string Obj_id;

    public int gridPosX;

    public int gridPosZ;

    public GameObject modelVisualization;

    public Vector3 worldPositionOffset;

    public Vector3 worldRotation;


    public float rotateDegrees;

    public void UpdateNode(Node[,] grid)
    {
        Node node = grid[gridPosX, gridPosZ];

        Vector3 worldPosition = node.quad.transform.position;

        transform.rotation = Quaternion.Euler(worldRotation);

        transform.position = worldPosition;
    }

    public void ChangeRotation()
    {
        Vector3 eulerAngels = transform.eulerAngles;
        
        eulerAngels += new Vector3(0,rotateDegrees,0);
        
        transform.localRotation = Quaternion.Euler(eulerAngels);
    }

    public SaveableLevelObject GetSaveableObject()
    {
        SaveableLevelObject saveObj = new SaveableLevelObject();

        saveObj.obj_id = Obj_id;
        saveObj.nodePosX = gridPosX;
        saveObj.nodePosZ = gridPosZ;
        saveObj.posX = transform.position.x;
        saveObj.posY = transform.position.y;
        saveObj.posZ = transform.position.z;

        worldRotation = transform.localEulerAngles;
 
        saveObj.rotX = worldRotation.x;
        saveObj.rotY = worldRotation.y;
        saveObj.rotZ = worldRotation.z;

        return saveObj;
    }
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
   
}
[Serializable]
public class SaveableLevelObject
{
    public string obj_id;

    public int nodePosX;

    public int nodePosZ;

    public float posX;
    public float posY;
    public float posZ;

    public float rotX;
    public float rotY;
    public float rotZ;

}

