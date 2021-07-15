using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPosition : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject parent;

    public GameObject CoinPrefab;

    public Vector3 coinTargetPosition;

    public int minXPosition, maxXPosition, minYPosition, maxYPosition;
    
    
    void Start()
    {
        coinTargetPosition = new Vector3(CreatRandomRange(maxXPosition,minXPosition),CreatRandomRange(maxYPosition,minYPosition),0);

        GameObject temp = Instantiate(CoinPrefab, coinTargetPosition, Quaternion.identity);
        
        temp.transform.SetParent(parent.transform);

        temp.transform.localPosition = coinTargetPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private int CreatRandomRange(int max, int min)
    {
        int randomCount = Random.Range(2000, 70000);

        randomCount = randomCount % (max - min + 1);

        return min + randomCount;

    }
}
