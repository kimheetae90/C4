using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CBoardController : Controller {

	public GameObject gridPrefab;
	List<CGrid> gridList;
	Vector3 hidePosition = new Vector3(0,0,-11);
	Dictionary<int,CBrushButton> brushButtonDic;
	public Texture nullGridImage;

	void Awake()
	{
		InitGrid ();
		HideAllGrid ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		brushButtonDic = GameObject.FindObjectOfType<CBrushButtonController> ().brushButtonDic;
	}
	
	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.Tool_LoadBoard:
			LoadBord((bool)_gameMessage.Get("clearInfo"));
			break;
		case MessageName.Tool_DrawStageInfo:
			DrawStageInfo(_gameMessage.Get("StageInfo") as StageInfo);
			break;
		case MessageName.Tool_ClearBoard:
			ClearBoard();
			break;
		}
	}

	void InitGrid()
	{
		gridList = new List<CGrid> ();

		for(int j=0;j<4;j++)
		{
			for (int i=0; i < 30; i++) 
			{			
				GameObject tempGridGameObject = Instantiate(gridPrefab) as GameObject;
				tempGridGameObject.transform.Translate(new Vector3(i, 2-j,0));
				CGrid tempGrid = tempGridGameObject.GetComponent<CGrid>();
				tempGrid.line = j+1;
				tempGrid.id = 0;
				tempGrid.time = i+1;
				gridList.Add(tempGrid);
			}
		}
	}

	void HideAllGrid()
	{
		foreach (CGrid tempGrid in gridList) 
		{
			tempGrid.gameObject.transform.position = hidePosition;
		}
	}

	void LoadBord(bool clearInfo)
	{
		ClearBoard ();

		if (clearInfo) 
		{
			SetPositionMineralGrid ();
		}
		else 
		{
			SetPositionNormalGrid();
		}
	}

	void SetPositionNormalGrid()
	{
		HideAllGrid ();

		for (int i=0; i<4; i++) 
		{
			for(int j=0;j<30;j++)
			{
				gridList[i*30+j].gameObject.transform.position = new Vector3(j, 2-i,0);
			}
		}
	}

	void SetPositionMineralGrid()
	{
		HideAllGrid ();

		for (int i=0; i<3; i++) 
		{
			for(int j=0;j<30;j++)
			{
				gridList[i*30+j].gameObject.transform.position = new Vector3(j, 2-i,0);
			}
		}
	}

	void ClearBoard()
	{
		foreach (CGrid node in gridList) 
		{
			node.SetMarkerNull(nullGridImage);
		}
	}

	void DrawStageInfo(StageInfo _stageInfo)
	{
		int tempID = _stageInfo.id;
		int tempLine = _stageInfo.line;
		int tempTime = _stageInfo.time;

		gridList.Find (x => (x.line == tempLine) && (x.time == tempTime)).SetMarker(brushButtonDic[tempID].GetComponent<Image>().mainTexture,tempID);

	}
}
