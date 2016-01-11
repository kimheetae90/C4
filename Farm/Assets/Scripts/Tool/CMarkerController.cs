using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CMarkerController : Controller {

	List<CGrid> markerList;

	List<Texture> markerTexture;

	void Awake()
	{
		markerTexture = new List<Texture> ();
		markerList = new List<CGrid> ();
		object[] tempList = GameObject.FindObjectsOfType<CGrid> ();
		foreach (CGrid temp in tempList) 
		{
			markerList.Add(temp);
		}

		Texture tempTexture;

		for (int i=0; i<=10; i++) 
		{
			string textureName = "marker" + i.ToString();
			tempTexture = Resources.Load ("Tool/" + textureName) as Texture;
			markerTexture.Add(tempTexture);
		}
	}

	protected override void Start ()
	{
		base.Start ();
	}
	
	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		GameObject selectedGridGameObject;
		switch (_gameMessage.messageName) 
		{
		case MessageName.LevelTool_ClickMarker:
			CChecker checker = _gameMessage.Get ("selectedChecker") as CChecker;
			selectedGridGameObject = _gameMessage.Get("clickGrid") as GameObject;
			if(checker != null) ClickMarker(checker.checkNum,selectedGridGameObject);
			break;
		case MessageName.LevelTool_RemoveMarker:
			selectedGridGameObject = _gameMessage.Get("clickGrid") as GameObject;
			RemoveMarker(selectedGridGameObject);
			break;
		}
	}


	void ClickMarker(int _checkNum, GameObject _selectedGrid)
	{
		CGrid tempGrid = _selectedGrid.GetComponent<CGrid> ();
		tempGrid.SetMarker(markerTexture[_checkNum],_checkNum);
	}

	void RemoveMarker(GameObject _selectedGrid)
	{
		CGrid tempGrid = _selectedGrid.GetComponent<CGrid> ();
		tempGrid.SetMarkerNull (markerTexture[0]);
	}

}
