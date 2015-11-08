using UnityEngine;
using System.Collections;

public class CLevelToolManager : SceneManager {

	protected override void Awake()
	{
		base.Awake ();
		gameState = GameState.LevelTool_Cam;
	}

	// Update is called once per frame
	void Update () {
		UpdateState ();
		InputKey ();
	}

	public override void DispatchInputData (InputData _inputData)
	{
		if (_inputData.selectedGameObject.CompareTag ("Grid")) 
		{
			GameObject selectedGameObject = _inputData.selectedGameObject;
		}
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.LevelTool_ChangeChecker:
			ChangeState(GameState.LevelTool_Checker);
			break;
		}

		_gameMessage.Destroy ();
	}

	protected override void UpdateState ()
	{
		Debug.Log (gameState);
	}

	protected override void ChangeState (GameState _gameState)
	{
		gameState = _gameState;
	}


	void InputKey()
	{
		GameMessage inputMessage;

		if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)1);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)2);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)3);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)4);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha5)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)5);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha6)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)6);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha7)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)7);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha8)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)8);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha9)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)9);
			Broadcast (inputMessage);
		}
		else if (Input.GetKeyDown (KeyCode.Alpha0)) 
		{
			inputMessage = GameMessage.Create(MessageName.LevelTool_ChangeChecker);
			inputMessage.Insert("CheckerNum",(int)10);
			Broadcast (inputMessage);
		}

	}
}
