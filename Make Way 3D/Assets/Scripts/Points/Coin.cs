using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

public class Coin : MonoBehaviour
{

    public UiManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ObscuredPrefs.SetInt("Coin" , ObscuredPrefs.GetInt("Coin",0)+1);
            
            uiManager.SetTextToObject(ObscuredPrefs.GetInt("Coin").ToString(),uiManager.coinText);
            
            Destroy(gameObject);
        }
    }
}
