using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    public GameObject nodePrefab;

    public int sizeX;
    public int sizeZ;
    public int offset;

    public Node[,] grid;

    public SubNode[,] subGrid;


//static instance------------------


    // Use this for initialization
    void Start()
    {
        CreateGrid();
        CreateMouseOrTouchCollision();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateGrid()
    {
        grid = new Node[sizeX, sizeZ];

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                float posX = x * offset;
                float posZ = z * offset;

                GameObject temp = Instantiate(nodePrefab, new Vector3(posX, 0f, posZ), Quaternion.identity);
                temp.transform.parent = transform.GetChild(0).transform;
                //NodeObject------------
                //---------------Sign Node----------------------//
                Node node = new Node {posX = x, posZ = z, quad = temp};
                //----------------End Sign Node-----------------//
                grid[x, z] = node;
            }
        }
    }
    
    public void CreateSubGrid()
    {
        subGrid = new SubNode[sizeX*2, sizeZ*2];

        for (int x = 0; x < sizeX*2; x++)
        {
            for (int z = 0; z < sizeZ*2; z++)
            {
                float posX = x * offset;
                float posZ = z * offset;

                GameObject temp = Instantiate(nodePrefab, new Vector3(posX, 0f, posZ), Quaternion.identity);
                temp.transform.parent = transform.GetChild(0).transform;
                //NodeObject------------
                //---------------Sign Node----------------------//
                Node node = new Node {posX = x, posZ = z, quad = temp};
                //----------------End Sign Node-----------------//
                grid[x, z] = node;
            }
        }
    }

    public void CreateMouseOrTouchCollision()
    {
        GameObject temp = new GameObject {name = "MouseORTouchCollider"};
        temp.AddComponent<BoxCollider>();
        temp.GetComponent<BoxCollider>().size = new Vector3(sizeX * offset, 0.1f, sizeZ * offset);
        temp.transform.position = new Vector3(sizeX * offset / 2 - offset / 2.0f, 0,
            sizeZ * offset / 2 - offset / 2.0f);
    }


    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float worldX = worldPosition.x;
        float worldZ = worldPosition.z;

        worldX /= offset;
        worldZ /= offset;

        int x = Mathf.RoundToInt(worldX);
        int z = Mathf.RoundToInt(worldZ);

        if (x > sizeX)
            x = sizeX;
        else if (x < 0)
            x = 0;
        
        if (z > sizeZ)
            z = sizeZ;
        else if (z < 0)
            z = 0;


        return grid[x, z];



    }

   
    
}