using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CCheckerController : Controller {

	List<CChecker> checkerList;
	CChecker selectedChecker;

	void Awake()
	{
		checkerList = new List<CChecker> ();
		object[] tempList = GameObject.FindObjectsOfType<CChecker> ();
		foreach (CChecker temp in tempList) 
		{
			checkerList.Add(temp);
		}
		selectedChecker = checkerList [0];
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.LevelTool_ChangeChecker:
			ClickChecker((int)_gameMessage.Get("CheckerNum"));
			break;
		case MessageName.LevelTool_ClearGridBoard:
			ClearChecker();
			break;
		}
	}

	void ClearChecker()
	{
		selectedChecker.ChangeToNonSelectedColor ();
		GameMessage clearCheckerMsg = GameMessage.Create (MessageName.LevelTool_ClearChecker);
		SendGameMessage (clearCheckerMsg);
	}

	public void ClickChecker(int num)
	{
		selectedChecker.ChangeToNonSelectedColor ();
		CChecker tempChecker = checkerList.Find (x => x.checkNum == num);
		selectedChecker = tempChecker;
		selectedChecker.ChangeToSelectedColor ();
		GameMessage selectCheckerMessage = GameMessage.Create (MessageName.LevelTool_ChangeChecker);
		selectCheckerMessage.Insert ("selectedChecker", selectedChecker);
		SendGameMessage (selectCheckerMessage);
	}
}
