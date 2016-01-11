using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class CLevelToolManager : SceneManager {

	CChecker selectedChecker;
	int chapter = 0;
	int stage = 0;
	int wave = 0;
	List<List<CGrid>> gridInfo;

	List<CGrid> currentGrid;
	List<StageInfo> stageInfoList;

	StageDataLoadHelper stageDataLoadHelper;

	protected override void Awake()
	{
		base.Awake ();
		stageInfoList = new List<StageInfo> ();
		stageDataLoadHelper = new StageDataLoadHelper ();
		gridInfo = new List<List<CGrid>> ();
		gameState = GameState.LevelTool_Cam;
		for (int i=0; i<5; i++) 
		{
			List<CGrid> tempGrid = new List<CGrid>();
			gridInfo.Add(tempGrid);
		}
		currentGrid = gridInfo [0];
	}

	// Update is called once per frame
	void Update () {
		UpdateState ();
		InputKey ();
	}

	public override void DispatchInputData (InputData _inputData)
	{
		if (_inputData.selectedGameObject == null)
			return;

		switch(_inputData.keyState)
		{
		case InputData.KeyState.Up:
			if (_inputData.selectedGameObject.CompareTag ("Grid") && _inputData.clickButton == InputData.ClickButton.Left) 
			{
				GameMessage selectMessage = GameMessage.Create(MessageName.LevelTool_ClickMarker);
				selectMessage.Insert("clickGrid",_inputData.selectedGameObject);
				selectMessage.Insert("selectedChecker",selectedChecker);
				Broadcast(selectMessage);
			}
			else if (_inputData.selectedGameObject.CompareTag ("Grid") && _inputData.clickButton == InputData.ClickButton.Right) 
			{
				GameMessage selectMessage = GameMessage.Create(MessageName.LevelTool_RemoveMarker);
				selectMessage.Insert("clickGrid",_inputData.selectedGameObject);
				Broadcast(selectMessage);
			}
			break;
		}
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.LevelTool_ChangeChecker:
			selectedChecker = _gameMessage.Get("selectedChecker") as CChecker;
			ChangeState(GameState.LevelTool_Checker);
			break;
		case MessageName.LevelTool_SelectStageInfo:
			SetStageValue((string)_gameMessage.Get("infoClass"), (int)_gameMessage.Get("value"));
			break;
		}

		_gameMessage.Destroy ();
	}

	protected override void UpdateState ()
	{
	}

	protected override void ChangeState (GameState _gameState)
	{
		gameState = _gameState;
	}


	void InputKey()
	{
		GameMessage inputMessage;

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)1);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)2);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)3);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)4);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)5);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)6);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)7);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)8);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)9);
			Broadcast (inputMessage);
		} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
			inputMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert ("CheckerNum", (int)10);
			Broadcast (inputMessage);
		}
	}

	public void ChangeChapter(string _chapter)
	{
		chapter = int.Parse (_chapter);
		Debug.Log (chapter);
	}

	public void ChangeStage(string _stage)
	{
		stage = int.Parse (_stage);
		Debug.Log (stage);
	}

	void SetStageValue(string _class, int _value)
	{
		if (_class == "chapter") 
		{
			chapter = _value;
			SetStageInfo();
		}
		else if (_class == "stage") 
		{
			stage = _value;
			SetStageInfo();
		}
		else if (_class == "wave") 
		{
			wave = _value;
			GameMessage changeWaveMsg = GameMessage.Create(MessageName.LevelTool_ChangeWave);
			changeWaveMsg.Insert("currentWave",currentGrid);

			currentGrid = gridInfo[_value-1];
		}
	}

	void SetStageInfo()
	{
		string fileName = "Data/" + "Stage" + chapter.ToString() + "_" + stage.ToString();
		XmlDocument xmlDoc = new XmlDocument();
		
		TextAsset textAsset = (TextAsset)Resources.Load(fileName);

		if (textAsset == null) 
		{
			CreateStageInfo();
		}
		else 
		{
			LoadStageInfo();
		}
	}

	void LoadStageInfo()
	{
		stageInfoList.Clear ();
		ClearWavePerGridInfo ();
		stageInfoList = stageDataLoadHelper.GetStageInfo (chapter, stage);

		//set wave info

		//draw grid
	}

	void CreateStageInfo()
	{
		stageInfoList.Clear ();
		ClearWavePerGridInfo ();

		//draw grid to 0
	}

	void ClearWavePerGridInfo()
	{
		foreach (List<CGrid> tempList in gridInfo) 
		{
			tempList.Clear();
		}
	}
}
