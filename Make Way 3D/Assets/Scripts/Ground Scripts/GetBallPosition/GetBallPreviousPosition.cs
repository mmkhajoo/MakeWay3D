using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

public class GetBallPreviousPosition : MonoBehaviour
{

    public Ground ground;

    public GameObject ballPlayer;
    
    public bool myBool ;
    // Start is called before the first frame update
    void Start()
    {
        ballPlayer = GameObject.FindGameObjectWithTag("Player");
        MyBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ground.IsMove)
        {
            MyBool = true;
        }
    }
    
    
 
    public bool MyBool
    {
        get { return myBool ; }
        set
        {
            if( value == myBool )
                return ;
 
            myBool = value ;
            if( myBool )
            {
                transform.position = ballPlayer.transform.position;
            }    
        }    
    }

//    public void Save()
//    {
//        if (!Directory.Exists(Application.persistentDataPath + "/Level")); //Application.streamingAssetsPath for assets Folder But not Work For Build
//        {
//            Directory.CreateDirectory(Application.persistentDataPath + "/Level");
//        }
//        BinaryFormatter bf = new BinaryFormatter();
//
//        FileStream file = File.Create(Application.persistentDataPath + "/Level/Level01.txt");
//        
//        var json = JsonUtility.ToJson(//Scriptble Object);
//        bf.Serialize(file, json);
//file.Close();
//
//    }
//
//    public void Load()
//    {
//        if (!Directory.Exists(Application.persistentDataPath + "/Level")); //Application.streamingAssetsPath for assets Folder
//        {
//            Directory.CreateDirectory(Application.persistentDataPath + "/Level");
//        }
//BinaryFormatter bf = new BinaryFormatter();
//        if (Directory.Exists(Application.persistentDataPath + "/Level/Level01.txt"))
//        {
//            FileStream file = File.Open(Application.persistentDataPath + "/Level/Level01.txt", FileMode.Open);
//            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file),//Scriptble Object);
//
//        }
//    }
}
