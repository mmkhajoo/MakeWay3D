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

	public ChangeColors changeColors;

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
		ObscuredPrefs.SetInt("Score", 0);
		uiManager.SetTextToObject(ObscuredPrefs.GetInt("Score").ToString(),uiManager.scoreValueText);
		uiManager.SetTextToObject(ObscuredPrefs.GetInt("Coin").ToString(),uiManager.coinText);
		uiManager.SetTextToObject(ObscuredPrefs.GetInt("LevelNumber").ToString(),uiManager.levelNumberText);
		uiManager.SetTextToObject((ObscuredPrefs.GetInt("LevelNumber")-1).ToString(),uiManager.previousLevelNumber);
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
	    groundsManagerFromLevelEditor.SetBallPosition();
	    cameraMove.gameover = false;
	    isGameStart = true;
	    StartCoroutine(ChangeColor());
	    Time.timeScale = 1.5f;
    }

    public void PauseGame()
    {
	    Time.timeScale = 0f;
    }

    public IEnumerator ChangeColor()
    {
	    byte temp = 0;
	    byte temp2 = 0;
	    while (true)
	    {
		    yield return new WaitForSeconds(10f);
		    while (temp == temp2)
		    {
			    temp2 = (byte)CreatRandomRange(changeColors.materialColors.Length-1, 0);
		    }

		    temp = temp2;
		    changeColors.MainChangeColor(temp);
	    }
    }
    
    
    private int CreatRandomRange(int max, int min)
    {
	    int randomCount = Random.Range(2000, 70000);

	    randomCount = randomCount % (max - min + 1);

	    return min + randomCount;

    }
    
    

}
