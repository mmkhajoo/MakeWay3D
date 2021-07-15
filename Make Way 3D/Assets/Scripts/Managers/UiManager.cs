using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    // Use this for initialization
//Game Objects-----------Start--------------//
    public GameObject gameOverPanel;

    public GameObject winPanel;

    public GameObject revivePanel;
    
    //Game Objects---------End---------------//
    
    //Texts ----------------- Start----------------------//
    public Text levelNumberText;

    public Text previousLevelNumber;

    public Text scoreValueText;

    public Text coinText;

    //Texts ------------------------- End --------------------//
    
    //Sliders ---------------------Start ------------------------//
    public Slider gameProgressSlider;
    //Sliders ---------------------- End ------------------------//
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
    }

    public void WinFunction()
    {
        winPanel.SetActive(true);
        gameProgressSlider.gameObject.SetActive(false);
    }

    public void ReviveFunction()
    {
        revivePanel.SetActive(true);
    }


    public void SetTextToObject(String text , Text temp)
    {
        temp.text = text;
    }
    
    public IEnumerator ChangeGameProgressSlider(float start , float end)
    {
        for (float i = 0f; i <= 1f; i += 0.02f)
        {
            gameProgressSlider.value = Mathf.Lerp(start, end, i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator ChangeValueAnimation(Text text , float start , float end)
    {
        for (float i = 0f; i <= 1; i += 0.05f)
        {
            text.text = Mathf.Round(Mathf.SmoothStep(start, end, i)).ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
    

    public void ChangeGameProgressInstantly(float value)
    {
        gameProgressSlider.value = value;
    }
}
