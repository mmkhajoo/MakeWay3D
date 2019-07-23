using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBoxColor : MonoBehaviour {

    public float speed;

    public int startColor, endColor;

    public Color[] colors;

    public Material material;

    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator ChangeColor(Color startColor , Color endColor) {
        for(float i=0;i<=1;i=i+0.1f)
        {
            material.SetColor("_Tint", Color.Lerp(startColor, endColor, i * speed));
            material.color = Color.Lerp(startColor, endColor, i * speed);
            yield return new WaitForSeconds(0.1f);
        }

    }

    public void Button()
    {
        StartChangeColor(startColor , endColor);
        Debug.Log("Change Color Started");
    }

    private void StartChangeColor(int start , int end)
    {
        StartCoroutine(ChangeColor(colors[start], colors[end]));
    }
}
