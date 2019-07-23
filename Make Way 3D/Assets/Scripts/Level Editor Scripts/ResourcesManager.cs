using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public List<LevelGameObjectBase> levelGameObjects = new List<LevelGameObjectBase>();

    // Use this for initialization
    void Start()
    {
    }

    public LevelGameObjectBase GetObjBase(string objId)
    {
        foreach (var levelObj in levelGameObjects)
            if (objId.Equals(levelObj.obj_id))
            {
                return levelObj;
            }

        return null;
    }
}

[Serializable]
public class LevelGameObjectBase
{
    public string obj_id;
    public string nickName;
    public int steps;
    public GameObject objPrefab;
}