using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class CStageToolManager : SceneManager {

	int stageNo;
	int chapterNo;
	int waveNo;
	bool clearInfo;

	int tool1ID;
	int tool1Lv;
	int tool2ID;
	int tool2Lv;
	int tool3ID;
	int tool3Lv;


	List<StageInfo> stageInfoList;
	public Text waveText;
	public Toggle clearInfoTG;

	protected override void Awake()
	{
		base.Awake ();
		stageInfoList = new List<StageInfo> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			Camera.main.transform.Translate(new Vector3(-2,0,0));
		}
		else if (Input.GetKey (KeyCode.RightArrow)) 
		{
			Camera.main.transform.Translate(new Vector3(2,0,0));
		}


		UpdateState ();
	}

	
	public override void DispatchInputData (InputData _inputData)
	{
		if (_inputData.keyState == InputData.KeyState.Up && _inputData.upRootGameObject != null) 
		{
			if(_inputData.clickButton == InputData.ClickButton.Left && _inputData.upRootGameObject.CompareTag("Tool_Grid"))
			{
				DrawGrid(_inputData.upRootGameObject);
			}
			else if(_inputData.clickButton == InputData.ClickButton.Right && _inputData.upRootGameObject.CompareTag("Tool_Grid"))
			{
				EraseGrid(_inputData.upRootGameObject);
			}
		}
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.Tool_DrawGrid:
			InsertStageInfo(_gameMessage.Get("grid") as CGrid, (int)_gameMessage.Get ("id"));
			break;
		case MessageName.Tool_EraseGrid:
			RemoveStageInfo(_gameMessage.Get("grid") as CGrid);
			break;
		case MessageName.Tool_SendDictionary:
			SendTo("CBoardController",_gameMessage);
			break;
		}

		_gameMessage.Destroy ();
	}

	protected override void UpdateState ()
	{

	}

	protected override void ChangeState (GameState _gameState)
	{

	}

	public void SetChapter(Text _chapter)
	{
		chapterNo = int.Parse(_chapter.text);
	}

	public void SetStage(Text _stage)
	{
		stageNo = int.Parse(_stage.text);
	}

	public void SetWave(int _wave)
	{
		waveNo = _wave;
		waveText.text = waveNo.ToString ();
		DrawAllStageInfo ();
	}

	public void SetClearInfo(Toggle _toggle)
	{
		clearInfo = _toggle.isOn;
	}

	public void SetNewBoard()
	{
		ClearStageInfo ();
		LoadBoard ();
	}

	public void SetLoadedBord()
	{
		LoadStageInfo ();
		LoadBoard ();
	}

	public void LoadStageInfo()
	{
		StageDataLoadHelper stageLoader = new StageDataLoadHelper ();
		stageInfoList = stageLoader.GetStageInfo (chapterNo, stageNo);
		clearInfo = stageLoader.GetClearInfo (chapterNo, stageNo);

		if (clearInfo)
			clearInfoTG.isOn = true;
		else
			clearInfoTG.isOn = false;

		SetWave (1);
	}

	public void Tool1ID(int _id)
	{
		tool1ID = _id;
	}

	public void Tool2ID(int _id)
	{
		tool2ID = _id;
	}

	public void Tool3ID(int _id)
	{
		tool3ID = _id;
	}

	public void Tool1Lv(Text _lv)
	{
		tool1Lv = int.Parse(_lv.text);
	}

	public void Tool2Lv(Text _lv)
	{
		tool2Lv = int.Parse(_lv.text);
	}

	public void Tool3Lv(Text _lv)
	{
		tool3Lv = int.Parse(_lv.text);
	}

	public void Test()
	{
		
		GameMaster.Instance.tempData.Insert ("chapterNo", chapterNo);
		GameMaster.Instance.tempData.Insert ("stageNo", stageNo);

		GameMaster.Instance.tempData.Insert ("StageInfo", stageInfoList);
		GameMaster.Instance.tempData.Insert ("ClearInfo", clearInfo);
		GameMaster.Instance.tempData.Insert ("Tool1ID", tool1ID);
		GameMaster.Instance.tempData.Insert ("Tool1Lv", tool1Lv);
		GameMaster.Instance.tempData.Insert ("Tool2ID", tool2ID);
		GameMaster.Instance.tempData.Insert ("Tool2Lv", tool2Lv);
		GameMaster.Instance.tempData.Insert ("Tool3ID", tool3ID);
		GameMaster.Instance.tempData.Insert ("Tool3Lv", tool3Lv);

		Application.LoadLevelAsync ("Play");
	}

	void DrawAllStageInfo()
	{
		GameMessage clearMsg = GameMessage.Create (MessageName.Tool_ClearBoard);
		Broadcast (clearMsg);

		foreach (StageInfo node in stageInfoList) 
		{
			if(node.wave == waveNo)
			{
				DrawStageInfo(node);
			}
		}
	}

	void DrawStageInfo(StageInfo _stageInfo)
	{
		GameMessage drawMsg = GameMessage.Create(MessageName.Tool_DrawStageInfo);
		drawMsg.Insert ("StageInfo", _stageInfo);
		Broadcast (drawMsg);
	}

	void InsertStageInfo(CGrid _grid, int id)
	{
		StageInfo tempStageInfo = new StageInfo ();
		tempStageInfo.id = id;
		tempStageInfo.line = _grid.line;
		tempStageInfo.time = _grid.time;
		tempStageInfo.wave = waveNo;

		stageInfoList.Add (tempStageInfo);
	}

	void RemoveStageInfo(CGrid _grid)
	{
		stageInfoList.Remove (stageInfoList.Find (x=>(x.line == _grid.line) && (x.time == _grid.time) && (x.wave == waveNo)));
	}

	void DrawGrid(GameObject _grid)
	{
		GameMessage drawMsg = GameMessage.Create (MessageName.Tool_DrawGrid);
		drawMsg.Insert ("grid", _grid);
		Broadcast (drawMsg);
	}

	void EraseGrid(GameObject _grid)
	{
		GameMessage eraseMsg = GameMessage.Create (MessageName.Tool_EraseGrid);
		eraseMsg.Insert ("grid", _grid);
		Broadcast (eraseMsg);
	}

	void ClearStageInfo()
	{
		chapterNo = 0;
		stageNo = 0;
		waveNo = 0;
		stageInfoList.Clear ();
	}

	void LoadBoard()
	{
		GameMessage loadBoardMsg = GameMessage.Create (MessageName.Tool_LoadBoard);
		loadBoardMsg.Insert ("clearInfo", clearInfo);
		Broadcast (loadBoardMsg);
	}
}
