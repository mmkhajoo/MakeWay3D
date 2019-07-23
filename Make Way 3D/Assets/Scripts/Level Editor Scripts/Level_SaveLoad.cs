using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_SaveLoad : MonoBehaviour
{
    // Use this for initialization

    public List<SaveableLevelObject> saveableLevelObjects_List = new List<SaveableLevelObject>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaveLevel()
    {
        Level_GameObject[] levelObjects = FindObjectsOfType<Level_GameObject>();

        saveableLevelObjects_List.Clear();

        foreach (var Object in levelObjects)
        {
            saveableLevelObjects_List.Add(Object.GetSaveableObject());
        }
    }

    public SaveableLevel_SaveLoad GetSaveableLevel()
    {
        SaveableLevel_SaveLoad temp = new SaveableLevel_SaveLoad();

        foreach (var VARIABLE in saveableLevelObjects_List)
        {
            temp.saveableLevelObjects_List.Add(VARIABLE);
        }

        temp.saveableLevelObjects_List.Reverse();
        return temp;
    }
}

[Serializable]
public class SaveableLevel_SaveLoad
{
    public List<SaveableLevelObject> saveableLevelObjects_List = new List<SaveableLevelObject>();
}