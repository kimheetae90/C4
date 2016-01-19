using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class CLevelToolManager : SceneManager {

	public CComboBox waveComboBox;

	CChecker selectedChecker;
	int chapter = 0;
	int stage = 0;
	int wave = 0;

	List<CGridInfo> stageInfoList;
	Dictionary<int,int> markerPerIDDic;
	Dictionary<int,int> idPerMarkerDic;
	List<int> levelToolInfoIDList;

	XmlDocument xmlDoc;
	XMLLoader xmlLoader;
	XmlNode rootNode;
	XmlNodeList statgeNodeList;
	XmlNodeList levelToolInfoNodeList;
	TextAsset textAsset;

	DataLoadHelper dataLoadHelper;
	List<MonsterInfo> monsterList;

	protected override void Awake()
	{
		base.Awake ();
		xmlDoc = new XmlDocument ();
		stageInfoList = new List<CGridInfo> ();
		markerPerIDDic = new Dictionary<int, int> ();
		idPerMarkerDic = new Dictionary<int, int> ();
		levelToolInfoIDList = new List<int> ();
		xmlLoader = new XMLLoader();
		gameState = GameState.LevelTool_Cam;
		dataLoadHelper = GameObject.FindObjectOfType<DataLoadHelper> ();
		monsterList = dataLoadHelper.GetMonsterList ();
	}

	// Update is called once per frame
	void Update () {
		UpdateState ();
		InputKey ();
	}

	public override void DispatchInputData (InputData _inputData)
	{
		if (_inputData.downRootGameObject == null)
			return;

		switch(_inputData.keyState)
		{
		case InputData.KeyState.Up:
			if (_inputData.upRootGameObject.CompareTag ("Grid") && _inputData.clickButton == InputData.ClickButton.Left) 
			{
				GameMessage selectMessage = GameMessage.Create(MessageName.LevelTool_ClickMarker);
				selectMessage.Insert("clickGrid",_inputData.upRootGameObject);
				selectMessage.Insert("selectedChecker",selectedChecker);
				ClickMarker(_inputData.upRootGameObject);
				Broadcast(selectMessage);
			}
			else if (_inputData.upRootGameObject.CompareTag ("Grid") && _inputData.clickButton == InputData.ClickButton.Right) 
			{
				GameMessage selectMessage = GameMessage.Create(MessageName.LevelTool_RemoveMarker);
				selectMessage.Insert("clickGrid",_inputData.upRootGameObject);
				RemoveMarker(_inputData.upRootGameObject);
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
		case MessageName.LevelTool_ClearChecker:
			selectedChecker = null;
			break;
		case MessageName.LevelTool_ChangeMark:
			ChangeMarkToID((int)_gameMessage.Get("mark"),(int)_gameMessage.Get("id"));
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
		else if(Input.GetKeyDown (KeyCode.LeftArrow))
		{
			Camera.main.transform.Translate(new Vector3(-10,0,0));
		}
		else if(Input.GetKeyDown (KeyCode.RightArrow))
		{
			Camera.main.transform.Translate(new Vector3(10,0,0));
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
			DrawGridPerWave (wave);
		}
	}

	void SetStageInfo()
	{
		string fileName = "Data/" + "Stage" + chapter.ToString() + "_" + stage.ToString();
		xmlDoc = new XmlDocument ();
		textAsset = (TextAsset)Resources.Load(fileName);

		wave = 1;

		if (textAsset == null) 
		{
			NewStageInfo();
		}
		else 
		{
			LoadStageInfo();
		}
	}

	void LoadStageInfo()
	{
		stageInfoList.Clear ();

		xmlDoc.LoadXml (textAsset.text);
		string stageNo = "Stage" + chapter.ToString () + "_" + stage.ToString ();
		rootNode = xmlDoc.SelectSingleNode(stageNo + "Data");
		statgeNodeList = rootNode.SelectNodes(stageNo);
		levelToolInfoNodeList = rootNode.SelectNodes ("LevelToolInfo");

		stageInfoList = new List<CGridInfo> ();
		CGridInfo stageInfo;

		
		CLevelToolInfo levelToolInfo;
		
		GameMessage freshMsg = GameMessage.Create (MessageName.LevelTool_ClearCheckerInfo);
		Broadcast (freshMsg);

		foreach (XmlNode node in levelToolInfoNodeList) {
			levelToolInfo = new CLevelToolInfo ();
			int tempID = int.Parse (node ["id"].InnerText);
			int tempMark = int.Parse (node ["mark"].InnerText);
			if(!markerPerIDDic.ContainsKey(tempID))
			{
				levelToolInfoIDList.Add(tempID);
				markerPerIDDic.Add(tempID,tempMark);
			}
			
			if(!idPerMarkerDic.ContainsKey(tempMark))
			{
				idPerMarkerDic.Add(tempMark,tempID);
			}
			
			DrawMonsterPanel(tempMark,tempID);
		}

		foreach (XmlNode node in statgeNodeList) {
			stageInfo = new CGridInfo ();
			
			stageInfo.wave = int.Parse (node ["wave"].InnerText);
			stageInfo.line = int.Parse (node ["line"].InnerText);
			stageInfo.time = int.Parse (node ["time"].InnerText);
			stageInfo.checker = markerPerIDDic[int.Parse (node ["id"].InnerText)];
			
			stageInfoList.Add (stageInfo);
		}


		DrawGridPerWave (1);
	}

	void DrawMonsterPanel(int _mark, int _id)
	{
		GameMessage drawMonsterPanelMsg = GameMessage.Create (MessageName.LevelTool_DrawMonsterPanel);
		drawMonsterPanelMsg.Insert ("mark", _mark);
		drawMonsterPanelMsg.Insert ("id",_id);
		Broadcast (drawMonsterPanelMsg);
	}

	void DrawGridPerWave(int _wave)
	{
		GameMessage freshMsg = GameMessage.Create (MessageName.LevelTool_ClearGridBoard);
		Broadcast (freshMsg);
		
		waveComboBox.ButtonChange (_wave);
		
		foreach (CGridInfo node in stageInfoList) 
		{
			if(node.wave == _wave)
			{
				GameMessage stageInfoMsg = GameMessage.Create(MessageName.LevelTool_SetGridToStageInfo);
				stageInfoMsg.Insert("line",node.line);
				stageInfoMsg.Insert("time",node.time);
				stageInfoMsg.Insert("mark",node.checker);
				Broadcast(stageInfoMsg);
			}
		}
	}

	void NewStageInfo()
	{
		waveComboBox.ButtonChange (1);
		stageInfoList.Clear ();
		GameMessage freshMsg = GameMessage.Create (MessageName.LevelTool_ClearGridBoard);
		Broadcast (freshMsg);
	}

	void ClickMarker(GameObject _clickObject)
	{
		CGrid tempGrid = _clickObject.GetComponent<CGrid> ();
		int tempLine = tempGrid.line;
		int tempTime = tempGrid.time;
		int tempID = idPerMarkerDic [selectedChecker.checkNum];

		CGridInfo newStageInfo = stageInfoList.Find (x=>(x.line == tempLine)&&(x.time == tempTime));

		if (newStageInfo == null) 
		{
			newStageInfo = new CGridInfo();
			newStageInfo.checker = selectedChecker.checkNum;
			newStageInfo.line = tempLine;
			newStageInfo.time = tempTime;
			newStageInfo.wave = wave;

			stageInfoList.Add(newStageInfo);
		} 
		else 
		{
			newStageInfo.checker = selectedChecker.checkNum;
		}
	}

	void RemoveMarker(GameObject _clickObject)
	{
		CGrid tempGrid = _clickObject.GetComponent<CGrid> ();
		int tempLine = tempGrid.line;
		int tempTime = tempGrid.time;
		int tempID = idPerMarkerDic [tempGrid.marker];

		CGridInfo newStageInfo = stageInfoList.Find (x=>(x.line == tempLine)&&(x.time == tempTime));
		if (newStageInfo != null) 
		{
			stageInfoList.Remove(newStageInfo);
		}

	}

	public void SaveStageInfo()
	{

		if (textAsset == null) 
		{
			rootNode = xmlDoc.CreateElement("","Stage" + chapter.ToString() + "_"+stage.ToString() +"Data","");
			xmlDoc.AppendChild(rootNode);
		}

		rootNode.RemoveAll ();

		foreach (int node in levelToolInfoIDList) 
		{
			XmlElement toolInfoElement = xmlDoc.CreateElement ("LevelToolInfo");
			XmlElement idElement = xmlDoc.CreateElement ("id");
			XmlElement markElement = xmlDoc.CreateElement ("mark");
			idElement.InnerText = node.ToString ();
			markElement.InnerText = markerPerIDDic[node].ToString();
			toolInfoElement.AppendChild (idElement);
			toolInfoElement.AppendChild (markElement);
			xmlDoc.DocumentElement.InsertAfter (toolInfoElement, rootNode.LastChild);
		}

		foreach (CGridInfo node in stageInfoList) 
		{
			XmlElement stageInfoElement = xmlDoc.CreateElement ("Stage"+chapter.ToString()+"_"+stage.ToString());
			XmlElement waveElement = xmlDoc.CreateElement ("wave");
			XmlElement lineElement = xmlDoc.CreateElement ("line");
			XmlElement timeElement = xmlDoc.CreateElement ("time");
			XmlElement idElement = xmlDoc.CreateElement ("id");
			waveElement.InnerText = node.wave.ToString ();
			lineElement.InnerText = node.line.ToString ();
			timeElement.InnerText = node.time.ToString ();
			idElement.InnerText = idPerMarkerDic[node.checker].ToString();
			stageInfoElement.AppendChild (waveElement);
			stageInfoElement.AppendChild (lineElement);
			stageInfoElement.AppendChild (timeElement);
			stageInfoElement.AppendChild (idElement);
			xmlDoc.DocumentElement.InsertAfter (stageInfoElement, rootNode.LastChild);
		}

		xmlDoc.Save ("Assets/Resources/Data/Stage"+ chapter + "_" + stage + ".xml");
	}

	public void ClickMonsterPanel(int _id)
	{
		GameMessage clickMonsterPanelMsg = GameMessage.Create (MessageName.LevelTool_ClickMonsterPanel);
		clickMonsterPanelMsg.Insert ("selectedChecker",selectedChecker.checkNum);
		clickMonsterPanelMsg.Insert ("id",_id);
		
		if (!idPerMarkerDic.ContainsKey (selectedChecker.checkNum)) {
			idPerMarkerDic.Add (selectedChecker.checkNum, _id);
			markerPerIDDic.Add (_id, selectedChecker.checkNum);
			levelToolInfoIDList.Add (_id);
		}
		else 
		{
			markerPerIDDic.Remove(idPerMarkerDic[selectedChecker.checkNum]);
			levelToolInfoIDList.Remove(idPerMarkerDic[selectedChecker.checkNum]);
			idPerMarkerDic.Remove(selectedChecker.checkNum);
		}

		Broadcast (clickMonsterPanelMsg);
	}

	void ChangeMarkToID(int _mark,int _id)
	{
		idPerMarkerDic.Add(_mark, _id);
		markerPerIDDic.Add(_id,_mark);
		levelToolInfoIDList.Add(_id);
	}
}
