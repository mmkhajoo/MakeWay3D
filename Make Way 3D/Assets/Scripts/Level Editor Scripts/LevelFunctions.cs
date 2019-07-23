﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using CodeStage.AntiCheat.ObscuredTypes;
public class LevelFunctions : MonoBehaviour
{
    public ResourcesManager resourcesManager;

    public GridBase gridBase;

    public LevelManager levelManager;

    public LevelManager[] levelManagers;

    public GameObject levelsPrefab;

    public GameObject levelManagerPrefab;

    public InputField levelNumber;

    private GameObject objToPlace;

    private Level_GameObject objProperties;

    public Levels levels;

    public Level_SaveLoad level;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetLevel1(int levelNumber)
    {
        level.SaveLevel();
        levels.levels[levelNumber] = level.GetSaveableLevel();
        PrefabUtility.ReplacePrefab(levels.gameObject, levelsPrefab);
    }

    public void SetLevel()
    {
        GameObject temp = Instantiate(levelManagerPrefab, transform.position, Quaternion.identity);
        temp.transform.SetParent(transform);
        LevelManager tempLevelManager = temp.GetComponent<LevelManager>();
        for (int i = 0; i < levelManager.objects.Count; i++)
        {
            tempLevelManager.objects.Add(levelManager.objects[i]);
            tempLevelManager.objectsProperties.Add(levelManager.objectsProperties[i]);
        }

        temp.name = "Level_" + levelNumber.text;
        levelManagers[int.Parse(levelNumber.text)] = tempLevelManager;
    }

    public void SaveButton()
    {
//		SetLevel();
        ObscuredPrefs.SetInt("LevelNumber",int.Parse(levelNumber.text));
        SetLevel1(int.Parse(levelNumber.text));
        PrefabUtility.ReplacePrefab(levels.gameObject, levelsPrefab);
    }


    public void Load()
    {
        ObscuredPrefs.SetInt("LevelNumber",int.Parse(levelNumber.text));
        level.saveableLevelObjects_List.Clear();
        foreach (var VARIABLE in levels.levels[int.Parse(levelNumber.text)].saveableLevelObjects_List)
        {
            level.saveableLevelObjects_List.Add(VARIABLE);
        }
        
        levelManager.Clear();
        
//        for (int i = 0; i < level.saveableLevelObjects_List.Count; i++)
//        {
//            string objID = tempLevelManager.objects[i].GetComponent<Level_GameObject>().Obj_id;
////			objToPlace = resourcesManager.GetObjBase(objID).objPrefab;
//            GameObject temp = Instantiate(resourcesManager.GetObjBase(objID).objPrefab
//                , tempLevelManager.objects[i].transform.position,
//                Quaternion.identity);
//            objProperties = temp.GetComponent<Level_GameObject>();
//            tempLevelManager.objects[i] = temp;
//            tempLevelManager.objectsProperties[i] = objProperties;
//            levelManager.objects.Add(temp);
//            levelManager.objectsProperties.Add(objProperties);
//        }
        foreach (var levelObject in level.saveableLevelObjects_List)
        {
            GameObject temp = Instantiate(resourcesManager.GetObjBase(levelObject.obj_id).objPrefab,
              new Vector3(levelObject.posX,levelObject.posY,levelObject.posZ) ,
                Quaternion.Euler(levelObject.rotX , levelObject.rotY , levelObject.rotZ));
            objProperties = temp.GetComponent<Level_GameObject>();
            gridBase.grid[levelObject.nodePosX, levelObject.nodePosZ].placedObj = objProperties;
            gridBase.grid[levelObject.nodePosX, levelObject.nodePosZ].levelObject = temp;
            
            levelManager.objects.Add(temp);
            levelManager.objectsProperties.Add(objProperties);
        }
    }
}