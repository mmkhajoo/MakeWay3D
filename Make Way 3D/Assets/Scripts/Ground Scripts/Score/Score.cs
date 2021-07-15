using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Level_GameObject levelGameObject;

    public GameObject target;

    public String[] scoreType;

    private float[] scoreRange;

    public UiManager uiManager;

    private int previousScore;

//    public bool dontUseTrigger;

    // Start is called before the first frame update
    void Start()
    {
        levelGameObject = transform.parent.gameObject.GetComponent<Level_GameObject>();
        uiManager = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
        SetRangeScores();

        SetGameObject();
    }


    public void SetRangeScores()
    {
        scoreRange = new float[levelGameObject.scores.Length - 1];

        float period = (levelGameObject.rangeNumbers[1] - levelGameObject.rangeNumbers[0]) /
                       (levelGameObject.scores.Length - 2);

        float start = levelGameObject.rangeNumbers[0];
        for (int i = 0; i < scoreRange.Length; i++)
        {
            scoreRange[i] = start;
            start += period;
        }

        for (int i = 0; i < scoreRange.Length; i++)
        {
            Debug.Log("ScoreRange[" + i + "] : " + scoreRange[i]);
        }
    }

    public void CalculateScore(float temp)
    {
        if (temp <= scoreRange[0])
        {
            previousScore = ObscuredPrefs.GetInt("Score");
            ObscuredPrefs.SetInt("Score", ObscuredPrefs.GetInt("Score") + levelGameObject.scores[0]);
            Debug.Log("Score in " + 0);
            return;
        }


        for (int i = 1; i < scoreRange.Length; i++)
        {
            if ((temp > scoreRange[i - 1]) && (temp <= scoreRange[i]))
            {
                previousScore = ObscuredPrefs.GetInt("Score");

                ObscuredPrefs.SetInt("Score", ObscuredPrefs.GetInt("Score") + levelGameObject.scores[i]);
                Debug.Log("Score in " + i);
            }
        }

        if (temp > scoreRange[scoreRange.Length - 1])
        {
            previousScore = ObscuredPrefs.GetInt("Score");

            ObscuredPrefs.SetInt("Score",
                ObscuredPrefs.GetInt("Score") + levelGameObject.scores[levelGameObject.scores.Length - 1]);
            Debug.Log("Score in " + levelGameObject.scores[levelGameObject.scores.Length - 1]);
            return;
        }
    }

    public void SetGameObject()
    {
        if (levelGameObject.isBall)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }


    public float TypeGameObject(String typeName, GameObject temp)
    {
        switch (typeName)
        {
            case "X":
            {
                return GetObjectPosition(temp).x;
            }

            case "Y":
            {
                return GetObjectPosition(temp).y;
            }

            case "RotationY":
            {
                return temp.transform.rotation.y * 180f;
            }

            case "RotationZ":
            {
                return temp.transform.rotation.z * 180f;
            }

            case "ScaleX":
            {
                return temp.transform.localScale.x;
            }

            default:
            {
                Debug.Log("Type Not Found");
                return 0f;
            }
        }
    }

    public Vector3 GetObjectPosition(GameObject temp)
    {
        if (levelGameObject.isBall)
        {
            Vector3 tempPosition = temp.transform.position - transform.parent.position;
            Debug.Log(transform.parent.InverseTransformDirection(tempPosition));
            return transform.parent.InverseTransformDirection(tempPosition);
        }

        return temp.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
//            if (!dontUseTrigger)
//            {
            CalculateScore(TypeGameObject(levelGameObject.scoreType, target));
            Debug.Log(ObscuredPrefs.GetInt("Score"));
//                dontUseTrigger = !dontUseTrigger;
            ChangeScore();
//            }
        }
    }


    public void ChangeScore()
    {
        StartCoroutine(uiManager.ChangeValueAnimation(uiManager.scoreValueText, previousScore,
            ObscuredPrefs.GetInt("Score")));
    }
}