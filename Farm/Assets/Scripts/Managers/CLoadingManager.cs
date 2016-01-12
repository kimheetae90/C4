using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CLoadingManager : SceneManager
{
    public Image ProgressBar;
    string nextScene;
    
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        GetNextSceneData();
        ChangeState(GameState.Loading_Ready);
    }

    public override void DispatchInputData(InputData _inputData)
    {
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        _gameMessage.Destroy();
    }

    protected override void UpdateState()
    {

    }

    protected override void ChangeState(GameState _gameState)
    {
        gameState = _gameState;

        switch (gameState)
        {
            case GameState.Loading_Ready:
                StartLoadNextScene();
                break;
            case GameState.Loading_LoadMain:
                break;
            case GameState.Loading_LoadPlay:
                break;
            case GameState.Loading_LoadSelectChapter:
                break;
            case GameState.Loading_LoadSelectStage:
                break;
        }
    }




    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 다음 씬으로 넘어갈 때, 'LoadNextScene' 코루틴을 시작하는 함수
    /// </summary>
    void StartLoadNextScene()
    {
        StartCoroutine("LoadNextScene");
    }

    /// <summary>
    /// 다음 씬으로 넘어갈 때 실행되는 코루틴.
    /// 진행도를 ProgressBar로 표현함.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadNextScene()
    {
        AsyncOperation async = Application.LoadLevelAsync(nextScene);

        while (async.isDone == false)
        {
            float percent = async.progress * 100.0f;

            int percentRounded = Mathf.RoundToInt(percent);

            ProgressBar.fillAmount = (percentRounded / 80.0f);

            yield return null;
        }
    }

    /// <summary>
    /// 다음 씬이 어떤 씬인지에 대한 정보를 받아 nextScene이라는 변수에 저장하는 함수.
    /// </summary>
    void GetNextSceneData()
    {
        nextScene = (string)GameMaster.Instance.tempData.Get("next_scene");

        GameMaster.Instance.tempData.Remove("next_scene");

        if(nextScene=="Play")
        {
            //GameMaster.Instance.tempData.Insert("StageInfo", DataLoadHelper.Instance.GetStageInfo((int)GameMaster.Instance.tempData.Get("chapterNum")+1, (int)GameMaster.Instance.tempData.Get("stageNum")+1));
            GameMaster.Instance.tempData.Insert("StageInfo", DataLoadHelper.Instance.GetStageInfo(0, 0));
        }

    }
}