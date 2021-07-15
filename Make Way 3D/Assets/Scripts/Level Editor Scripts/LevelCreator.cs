using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
	public GridBase gridBase;
	public InterfaceManager ui;
	public ResourcesManager resourcesManager;
	public LevelManager levelManager;
	
	
	
	//place obj variables
	private bool hasObject;
	private GameObject objectToPlace = null;
	private GameObject cloneObject = null;
	private GameObject tempActualObject;
	private Level_GameObject objProperties;
	private Vector3 touchPosition;
	private Vector3 worldPosition;
	private bool deleteObject;
	
	
	// Use this for initialization
	void Start ()
	{
		cloneObject = null;
		objectToPlace = null;
	}
	
	// Update is called once per frame
	void Update () {
		PlaceObject();
		DeleteObjects();
	}

	private void UpdateTouchPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			touchPosition = hit.point;
		}
	}

	private void PlaceObject()
	{
		if (hasObject)
		{
			UpdateTouchPosition();

			Node currentNod = gridBase.NodeFromWorldPosition(touchPosition);

			worldPosition = currentNod.quad.transform.position;

			if (cloneObject == null)
			{
				cloneObject = Instantiate(objectToPlace,worldPosition,objectToPlace.transform.rotation);
				objProperties = cloneObject.GetComponent<Level_GameObject>();
			}
			else
			{
				cloneObject.transform.position = worldPosition; 
				// Save Position Of Last GameObject was in Node in Level Manager List Objects
				//touch Position
//				Input.GetTouch(0).phase == TouchPhase.Ended)
				if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
				{
					if (currentNod.placedObj != null)
					{
						for(int i= 0 ; i<levelManager.objects.Count;i++)
						{
							if (levelManager.objects[i] == currentNod.levelObject)
							{
								Destroy(currentNod.placedObj.gameObject);
								currentNod.levelObject = null;
								currentNod.placedObj = null;
								
								GameObject actualObjectPlacedAgain = Instantiate(objectToPlace, worldPosition, cloneObject.transform.rotation);
								tempActualObject = actualObjectPlacedAgain;
								objProperties = actualObjectPlacedAgain.GetComponent<Level_GameObject>();

								objProperties.gridPosX = currentNod.posX;
								objProperties.gridPosZ = currentNod.posZ;
								currentNod.placedObj = objProperties;
								currentNod.levelObject = actualObjectPlacedAgain;
					
								levelManager.objects[i] = tempActualObject;
								levelManager.objectsProperties[i] = objProperties;
								
								if (levelManager.objects.Count != 0)
								{	
									Vector3 temp = actualObjectPlacedAgain.transform.position;
									temp.y = levelManager.objects[i - 1]
										.transform.Find("EndGround").transform.position.y;
									actualObjectPlacedAgain.transform.position = temp;
								}

								break;
							}
						}
						
					}
					else
					{
						GameObject actualObjectPlaced = Instantiate(objectToPlace, worldPosition, cloneObject.transform.rotation);
						tempActualObject = actualObjectPlaced;
						objProperties = actualObjectPlaced.GetComponent<Level_GameObject>();

						objProperties.gridPosX = currentNod.posX;
						objProperties.gridPosZ = currentNod.posZ;
						currentNod.placedObj = objProperties;
						currentNod.levelObject = actualObjectPlaced;
						
						levelManager.objects.Add(actualObjectPlaced);
						levelManager.objectsProperties.Add(objProperties);
					
						if (levelManager.objects.Count > 1)
						{	
							for(int i= 0 ; i<levelManager.objects.Count;i++)
							{
								if (levelManager.objects[i] == currentNod.levelObject)
								{
									Vector3 temp = actualObjectPlaced.transform.position;
									temp.y = levelManager.objects[i - 1]
										.transform.Find("EndGround").transform.position.y;
									actualObjectPlaced.transform.position = temp;
									break;
								}
							}
							
						}
					
					}
					
					CloseAll();

				}
				
				//change Rotation
//				if (Input.)
//				{
//					objProperties.ChangeRotation();
//				}
			}
			
		}
		else
		{
			if (cloneObject != null)
			{
				Destroy(cloneObject);
				cloneObject = null;
			}
		}
	}

	public void ChangeRotationButton()
	{
		objProperties.ChangeRotation();
	}

	private void DeleteObjects()
	{
		if (deleteObject)
		{
			UpdateTouchPosition();

			Node currentNode = gridBase.NodeFromWorldPosition(touchPosition);
			
			//-----position touch-------//
//			Input.GetTouch(0).phase == TouchPhase.Began)
			if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
			{
				if (!currentNode.placedObj.Equals(null))
				{
					for(int i= 0 ; i<levelManager.objects.Count;i++)
					{
						if (levelManager.objects[i] == currentNode.levelObject)
						{
							levelManager.objects.Remove(levelManager.objects[i]);
							levelManager.objectsProperties.Remove(levelManager.objectsProperties[i]);
						}
					}
					Destroy(currentNode.placedObj.gameObject);
					currentNode.placedObj = null;
				}
				
			}
		}
	}

	public void PassGameObjectToPlaceFromButton(string objID)
	{
		if (cloneObject != null)
		{
			Destroy(cloneObject);
		}
		
		CloseAll();
		hasObject = true;
		cloneObject = null;
		objectToPlace = resourcesManager.GetObjBase(objID).objPrefab;

	}

	public void DeleteGameObjectButton()
	{
		CloseAll();
		deleteObject = true;
	}



	private void CloseAll()
	{
		hasObject = false;
		deleteObject = false;
	}
	
	public void MoveObject(GameObject temp)
	{
		switch (temp.name)
		{
			case "Up":
			{
				Debug.Log("Up Called");
				tempActualObject.transform.position = new Vector3(worldPosition.x
					,tempActualObject.transform.position.y,worldPosition.z +gridBase.offset / 2);
				break;
			}
			
			case "Down":
			{
				
				tempActualObject.transform.position = new Vector3(worldPosition.x
					,tempActualObject.transform.position.y,worldPosition.z -gridBase.offset / 2);
				break;
			}
			
			case "Left":
			{
				
				tempActualObject.transform.position = new Vector3(worldPosition.x-gridBase.offset / 2
					,tempActualObject.transform.position.y,worldPosition.z );
				break;
			}
			
			case "Right":
			{
				
				tempActualObject.transform.position = new Vector3(worldPosition.x+gridBase.offset / 2
					,tempActualObject.transform.position.y,worldPosition.z );
				break;
			}

			case "DownRight":
			{
				tempActualObject.transform.position = new Vector3(worldPosition.x+gridBase.offset / 2
					,tempActualObject.transform.position.y,worldPosition.z-gridBase.offset / 2 );
				break;
			}
			
			case "DownLeft":
			{
				tempActualObject.transform.position = new Vector3(worldPosition.x-gridBase.offset / 2
					,tempActualObject.transform.position.y,worldPosition.z-gridBase.offset / 2 );
				break;
			}
			
			case "UpRight":
			{
				tempActualObject.transform.position = new Vector3(worldPosition.x+gridBase.offset / 2
					,tempActualObject.transform.position.y,worldPosition.z+gridBase.offset / 2 );
				break;
			}
			
			case "UpLeft":
			{
				tempActualObject.transform.position = new Vector3(worldPosition.x-gridBase.offset / 2
					,tempActualObject.transform.position.y,worldPosition.z+gridBase.offset / 2 );
				break;
			}
			
			case "Mid":
			{

				tempActualObject.transform.position = new Vector3(worldPosition.x
					,tempActualObject.transform.position.y,worldPosition.z );
				break;
			}
			
		}
	}
	
	
}
