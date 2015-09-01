using UnityEngine;
using System.Collections;

public class CGameLoadingManager : SceneManager {

	protected override void Awake()
	{
		base.Awake ();
	}
	
	void Start()
	{
		ChangeState (GameState.GameLoading_Ready);
	}
	
	void Update () {
		UpdateState ();
	}
	
	public override void DispatchInputData (InputData _inputData)
	{
		if (_inputData.keyState == InputData.KeyState.Up) 
		{
			OnClickToStartLoad();
		}
	}
	
	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		_gameMessage.Destroy ();
	}
	
	protected override void UpdateState ()
	{

	}
	
	protected override void ChangeState (GameState _gameState)
	{
		gameState = _gameState;

		switch(gameState)
		{
		case GameState.GameLoading_LoadMainScene:
            LoadMain();
			break;
		}
	}
	
	///////////////////////////////////////////////////////////////////////////////
	//////////////////////// 			구현               ////////////////////////
	///////////////////////////////////////////////////////////////////////////////

	void OnClickToStartLoad()
	{
		ChangeState(GameState.GameLoading_LoadMainScene);
	}

    /// <summary>
    /// Main씬으로 넘어가는 함수.
    /// </summary>
	void LoadMain () 
	{
        InputTempDataAboutNextScene("Main");
        LoadLoadingScene();
	}
}
