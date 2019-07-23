using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    // Use this for initialization

    public GameObject gameOverPanel;

    public GameObject winPanel;

    public GameObject revivePanel;

    public Text levelNumberText;
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
    }

    public void ReviveFunction()
    {
        revivePanel.SetActive(true);
    }


    public void SetTextToObject(String text , Text temp)
    {
        temp.text = text;
    }
}
