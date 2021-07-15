using System.Collections;
using System.Collections.Generic;
using Ground_Scripts;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.SceneManagement;

public class ReviveBall : MonoBehaviour
{
    public GameObject player;

    public PlayerMove playerMove;

    public Rigidbody playerRb;

    public GameObject startButton;

    public CameraMove cameraMove;

    public GroundsManagerFromLevelEditor groundsManager;

    public ResourcesManager resourcesManager;

    public ButtonManager buttonManager;
    // Use this for initialization

    public void Revive()
    {
        int steps=0;
        
        groundsManager.SetBallPosition(player,
            groundsManager.creatGrounds.fieldGameObjectsList[groundsManager.destroyCounter]);
        
        Debug.Log(resourcesManager.GetObjBase(groundsManager.levelSaveLoad
                      .saveableLevelObjects_List[groundsManager.destroyCounter-1].obj_id).steps+ " - " + resourcesManager.GetObjBase(groundsManager.levelSaveLoad
                      .saveableLevelObjects_List[groundsManager.destroyCounter].obj_id).steps);
        
//        resourcesManager.GetObjBase(groundsManager.levelSaveLoad
//            .saveableLevelObjects_List[groundsManager.destroyCounter-1].obj_id).steps
//        for (int i = 0;
//            i < resourcesManager.GetObjBase(groundsManager.levelSaveLoad
//                .saveableLevelObjects_List[groundsManager.destroyCounter].obj_id).steps;
//            i++)
//        {
//            groundsManager.creatGrounds.groundScripts[groundsManager.destroyCounter+ i].IsMove = true;
//        }
        
        
        for (int i = resourcesManager.GetObjBase(groundsManager.levelSaveLoad
                .saveableLevelObjects_List[groundsManager.destroyCounter].obj_id).steps;
            i >0 ;
            i--)
        {
            groundsManager.creatGrounds.groundScripts[groundsManager.stopCount-i].IsMove = true;
        }

        for (int i = 0; i < groundsManager.destroyCounter; i++)
        {
            steps += resourcesManager.GetObjBase(groundsManager.levelSaveLoad
                .saveableLevelObjects_List[i].obj_id).steps;
        }
        
        groundsManager.stopCount = steps;


        playerMove.stopForcingBall = false;

        cameraMove.gameObject.transform.position = player.transform.position - cameraMove.offset;

        playerRb.velocity = Vector3.zero;

        playerMove.isMove = true;
        
        buttonManager.permissionToStopObject = true;

        Time.timeScale = 0f;

        startButton.SetActive(true);
    }

    public void ReviveWithReloadScene()
    {
        int steps=0;
        ObscuredPrefs.SetInt("DestroyCounter",groundsManager.destroyCounter);
        
        for (int i = 0; i < groundsManager.destroyCounter; i++)
        {
            steps += resourcesManager.GetObjBase(groundsManager.levelSaveLoad
                .saveableLevelObjects_List[i].obj_id).steps;
        }
        ObscuredPrefs.SetInt("Steps",steps);
        ObscuredPrefs.SetBool("Revive",true);
        SceneManager.LoadScene("Core");
    }
}