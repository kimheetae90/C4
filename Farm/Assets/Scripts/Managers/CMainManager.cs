using UnityEngine;
using System.Collections;

public class CMainManager : SceneManager {

	protected override void Awake()
	{
		base.Awake ();
	}

	void Start()
	{
		ChangeState (GameState.Main_Ready);
	}

	void Update () {
		UpdateState ();
	}

	public override void DispatchInputData (InputData _inputData)
	{
		if (_inputData.keyState == InputData.KeyState.Up) 
		{
            OnClickToLoadSelectChapter(_inputData.selectedGameObject);			
		}
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		_gameMessage.Destroy ();
	}

	protected override void UpdateState ()
	{}

	protected override void ChangeState (GameState _gameState)
	{
		gameState = _gameState;

		switch(gameState)
		{
		case GameState.Main_Ready:
			break;

		case GameState.GameLoading_LoadMainScene:
			break;

        case GameState.Main_LoadMaintain:
            LoadMaintain();
            break;

        case GameState.Main_LoadSelectChapter:
            LoadSelectChapter();
			break;
		}
	}




	///////////////////////////////////////////////////////////////////////////////
	//////////////////////// 			구현               ////////////////////////
	/// //////////////////////////////////////////////////////////////////////////

	public void OnClickToStartMaintain()
    {
		ChangeState (GameState.Main_LoadMaintain);
	}

    void OnClickToLoadSelectChapter(GameObject _selectedGameObject)
	{
        switch(_selectedGameObject.tag)
        {
            case "Main_SelectChapter":
			    ChangeState(GameState.Main_LoadSelectChapter);
                break;
            default:
                break;
        }
	}

    /// <summary>
    /// SelectChapter씬으로 넘어가는 함수.
    /// </summary>
    void LoadSelectChapter() 
	{
        InputTempDataAboutNextScene("SelectChapter");
        LoadLoadingScene();
	}

    /// <summary>
    /// MainTain씬으로 넘어가는 함수.
    /// </summary>
	void LoadMaintain () 
	{
        InputTempDataAboutNextScene("MainTain");
        LoadLoadingScene();
	}

}

