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
        
        case GameState.Main_LoadStorage:
            StartStorage();
            break;

        case GameState.Main_LoadDevelopmentCenter:
            StartDevelopmentCenter();
            break;
		}
	}




	///////////////////////////////////////////////////////////////////////////////
	//////////////////////// 			구현               ////////////////////////
	/// //////////////////////////////////////////////////////////////////////////

	public void OnClickToStartStorage()
    {
		ChangeState (GameState.Main_LoadStorage);
	}

    public void OnClickToStartDevelopmentCenter()
    {
        ChangeState(GameState.Main_LoadDevelopmentCenter);
    }

    public void OnClickToStartSelectChapter()
    {
        ChangeState(GameState.Main_LoadSelectChapter);
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

    void StartDevelopmentCenter()
    {
        StartCoroutine("LoadDevelopmentCenter");
    }

    /// <summary>
    /// DevelopmentCenter씬으로 넘어가기 위한 코루틴함수.
    /// </summary>
    IEnumerator LoadDevelopmentCenter()
    {
        AsyncOperation async = Application.LoadLevelAsync("DevelopmentCenter");

        while (async.isDone == false)
            yield return null;
    }

    void StartStorage()
    {
        StartCoroutine("LoadStorage");
    }

    /// <summary>
    /// Storage씬으로 넘어가기 위한 코루틴함수.
    /// </summary>
    IEnumerator LoadStorage()
    {
        AsyncOperation async = Application.LoadLevelAsync("Storage");

        while (async.isDone == false)
            yield return null;
    }

}

