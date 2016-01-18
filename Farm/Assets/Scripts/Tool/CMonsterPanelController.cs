using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CMonsterPanelController : Controller {

	List<CMonsterPanel> monsterPanelList;

	void Awake()
	{
		monsterPanelList = new List<CMonsterPanel> ();
		object[] tempList = GameObject.FindObjectsOfType<CMonsterPanel> ();
		foreach (CMonsterPanel temp in tempList) 
		{
			monsterPanelList.Add(temp);
		}
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		int tempMarker;
		int tempID;

		switch(_gameMessage.messageName)
		{
		case MessageName.LevelTool_ClickMonsterPanel:
			tempMarker = (int)_gameMessage.Get("selectedChecker");
			tempID = (int)_gameMessage.Get("id");
			ResetMark(tempMarker,tempID);
			SetPanelMark(tempMarker,tempID);
			break;
		case MessageName.LevelTool_DrawMonsterPanel:
			tempMarker = (int)_gameMessage.Get("mark");
			tempID = (int)_gameMessage.Get("id");
			SetPanelMark(tempMarker,tempID);
			break;

		case MessageName.LevelTool_ClearCheckerInfo:
			ClearCheckerInfo();
			break;
		}
	}

	void ClearCheckerInfo()
	{
		foreach (CMonsterPanel node in monsterPanelList) 
		{
			node.SetNull();
		}
	}

	void ResetMark(int _mark,int tempID)
	{
		CMonsterPanel monsterPanel = monsterPanelList.Find (x => x.mark == _mark);

		if (monsterPanel != null) 
		{
			GameMessage changeMarkMsg = GameMessage.Create(MessageName.LevelTool_ChangeMark);
			changeMarkMsg.Insert("mark",_mark);
			changeMarkMsg.Insert("id",tempID);
			SendGameMessage(changeMarkMsg);
			monsterPanel.SetNull();
		}
	}

	void SetPanelMark(int _mark, int _id)
	{
		GetMonsterPanel (_id).SetMarker (_mark);
	}

	CMonsterPanel GetMonsterPanel(int _id)
	{
		return monsterPanelList.Find (x => x.id == _id);
	}

}
