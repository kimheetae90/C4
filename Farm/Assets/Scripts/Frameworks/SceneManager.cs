using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SceneManager : MonoBehaviour {

	// How to Use
	// 1. 씬매니저 만들고 상속
	// 2. 게임오브젝트에 붙여라
	// 3. 어웨이크에 base 호출
	// 4. 추상함수 구현
	// 5. 'UpdateState'를 Update에 넣어라
	// 6. '_gameMessage.Destroy ();' 를 DispatchGameMessage 맨 뒤에 넣어라

	List<Controller> controllerList;
	Dictionary<string, Controller> controllerDic;
	protected GameState gameState;

	protected virtual void Awake()
	{
		GameMaster.Instance.SetSceneManager (this.GetComponent<SceneManager>());
        InputHelper.Instance.SetSceneManager(this.GetComponent<SceneManager>());
		controllerList = new List<Controller> ();
		controllerDic = new Dictionary<string, Controller> ();
	}

	public void AddController(Controller _controller, string _name)
	{
		controllerList.Add (_controller);
		controllerDic.Add (_name, _controller);
	}

	public abstract void DispatchInputData (InputData _inputData);
	public abstract void DispatchGameMessage (GameMessage _gameMessage);
	protected abstract void UpdateState ();
	protected abstract void ChangeState (GameState _gameState);

	protected Controller SendTo(string _name, GameMessage _gameMessage)
	{
		if (controllerDic.ContainsKey (_name)) 
		{
			return controllerDic[_name];
		}
		else 
		{
			return	null;
		}
	}

	protected void Broadcast(GameMessage _gameMessage)
	{
		for (int i=0; i<controllerList.Count; i++) 
		{
			controllerList[i].DispatchGameMessage(_gameMessage);
		}

		_gameMessage.Destroy ();
	}

    protected void InputTempDataAboutNextScene(string _scene_name)
    {
        GameMaster.Instance.tempData.Insert("next_scene", _scene_name);
    }

    protected void LoadLoadingScene()
    {
        Application.LoadLevel("Loading");
    }
}
