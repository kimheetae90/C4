using UnityEngine;
using System.Collections;

public abstract class BaseObject : MonoBehaviour {
    
	protected ObjectState objectState;

	Controller controller;
	public int id;

    protected abstract void UpdateState();
    protected abstract void ChangeState(ObjectState _objectState);

	public void SetController(Controller _controller)
	{
		if (_controller == null) {
			LogManager.log ("Error : controller가 생성되지 않음(Null Pointer)");
			return ;
		}

		controller = _controller;
	}

	public void SendGameMessage(GameMessage _gameMessage)
	{
		if (_gameMessage == null){
			LogManager.log ("Error : _gameMessage가 생성되지 않음(Null Pointer)");
			return ;
		}

		controller.DispatchGameMessage (_gameMessage);
		_gameMessage.Destroy ();
	}

	public void SendGameMessageToSceneManage(GameMessage _gameMessage)
	{
		if (_gameMessage == null){
			LogManager.log ("Error : _gameMessage가 생성되지 않음(Null Pointer)");
			return ;
		}
		
		GameMaster.Instance.GetSceneManager ().DispatchGameMessage (_gameMessage);
	}
}
