using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ChangeColors : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Colors[] materialColors;
//
    public Colors[] skyBoxColors;

    public Material[] materials;

    public Material skyBoxMaterial;

    public String[] colorNames;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color[] colors , Material[] materials)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            StartCoroutine(ChangeColorIEnumerator(materials[i].color, colors[i], materials[i],colorNames[0]));
        }
    }

    public void ChangeSkyColor(Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            StartCoroutine(ChangeColorIEnumerator(skyBoxMaterial.GetColor(colorNames[i+1]), colors[i], skyBoxMaterial,colorNames[i+1]));
        }
    }

    public void MainChangeColor(int i)
    {
        ChangeColor(materialColors[i].colors,materials);
        ChangeSkyColor(skyBoxColors[i].colors);
    }


    public IEnumerator ChangeColorIEnumerator(Color startColor , Color endColor , Material material , String colorName)
    {
        for (float i = 0; i <=1; i+=0.01f)
        {
            var tempColor = Color.Lerp(startColor, endColor, i);
            material.SetColor(colorName,tempColor);
            yield return  new WaitForEndOfFrame();
        }
    }

}
[Serializable]
public class Colors
{
    public String ColorName;
    public Color[] colors;
}


