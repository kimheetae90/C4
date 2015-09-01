using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour {
	
	// How to Use
	// 1. 컨트롤러 만들고 상속시켜라
	// 2. 게임오브젝트에 붙여라
	// 3. 스타트에 base 호출
	// 4. 추상함수 구현

	SceneManager sceneManager;
	
	protected virtual void Start () {
		GameMaster.Instance.GetSceneManager ().AddController (this, this.GetType().ToString());
	}

	public abstract void DispatchGameMessage (GameMessage _gameMessage);
	public void SendGameMessage(GameMessage _gameMessage)
	{
		if (_gameMessage == null){
			LogManager.log ("Error : _gameMessage가 생성되지 않음(Null Pointer)");
			return ;
		}

		GameMaster.Instance.GetSceneManager ().DispatchGameMessage (_gameMessage);
	}
}
