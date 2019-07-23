using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateObjectButtons : MonoBehaviour {

	// Use this for initialization
	public GameObject objectButtonPrefab;
	
	public List<string> nameObjects = new List<string>();
	
	
	public List<string> nickNameObjects = new List<string>();

	public ResourcesManager resourcesManager;

	public LevelCreator levelCreator;

	
	void Start () {
		for (int i = 0; i < resourcesManager.levelGameObjects.Count; i++)
		{
			nameObjects.Add(resourcesManager.levelGameObjects[i].obj_id);
			nickNameObjects.Add(resourcesManager.levelGameObjects[i].nickName);
			
			GameObject temp = Instantiate(objectButtonPrefab, transform.position, Quaternion.identity);
			
			Button tempButton =   temp.GetComponent<Button>();
		
			string tempString = resourcesManager.levelGameObjects[i].obj_id;

			UnityEngine.Events.UnityAction action1 = () => { levelCreator.PassGameObjectToPlaceFromButton(tempString); };
			 
			tempButton.onClick.AddListener(action1);

			temp.GetComponentInChildren<Text>().text = resourcesManager.levelGameObjects[i].nickName;
			 
			temp.transform.SetParent(transform);
			temp.transform.localScale = Vector3.one;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Salam()
	{
	}
	
}
