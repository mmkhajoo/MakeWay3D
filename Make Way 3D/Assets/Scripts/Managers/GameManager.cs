using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;
using Ground_Scripts;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;

public class GameManager : MonoBehaviour {

    // Use this for initialization
//    public GroundsManagerFromLevelEditor groundsManager;

    public UiManager uiManager;

    public CameraMove cameraMove;

    public GroundsManagerFromLevelEditor groundsManagerFromLevelEditor;

    public bool Win;
    
    public bool isGameStart;
    void Awake()
    {
	    Application.targetFrameRate = 60;
    }
	void Start () {
		uiManager.SetTextToObject(ObscuredPrefs.GetInt("LevelNumber").ToString(),uiManager.levelNumberText);
		PauseGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
	    if(Win) return;
	    uiManager.ReviveFunction();
//        uiManager.GameOver();
	    cameraMove.GameOver();
    }

    public void WinFunction()
    {
	  //nothing yet
	  Debug.Log("Win");
	    uiManager.WinFunction();
	    
    }

    public void NextLevel()
    {
	    ObscuredPrefs.SetInt("LevelNumber",ObscuredPrefs.GetInt("LevelNumber")+1);
	    SceneManager.LoadScene("Core");
    }

    public void StartGame()
    {
//	    groundsManagerFromLevelEditor.SetBallPosition();
	    cameraMove.gameover = false;
	    Time.timeScale = 1.5f;
    }

    public void PauseGame()
    {
	    Time.timeScale = 0f;
    }
    
    

}
