using UnityEngine;
using System.Collections;

public class CSelectStageManager : SceneManager
{

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        ChangeState(GameState.SelectStage_Ready);
    }

    void Update()
    {
        UpdateState();
    }

    public override void DispatchInputData(InputData _inputData)
    {
        if (_inputData.keyState == InputData.KeyState.Up)
        {
            OnClickToStartStage(_inputData.selectedGameObject);
        }
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        _gameMessage.Destroy();
    }

    protected override void UpdateState()
    { }

    protected override void ChangeState(GameState _gameState)
    {
        gameState = _gameState;

        switch (gameState)
        {
            case GameState.SelectStage_Ready:
                break;
            case GameState.SelectStage_LoadStage:
                LoadStage();
                break;
        }
    }




    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    /// 

    /// <summary>
    /// 선택한 스테이지의 정보를 처리하는 함수.
    /// </summary>
    /// <param name="_selectedGameObject"></param>
    void OnClickToStartStage(GameObject _selectedGameObject)
    {
        switch (_selectedGameObject.tag)
        {
            case "SelectStage_Stage":
                CStage tempStage = _selectedGameObject.GetComponentInChildren<CStage>();
                GameMaster.Instance.tempData.Insert("stageNum", tempStage.stageNum);
                GameMaster.Instance.tempData.Insert("stageName", tempStage.stageName);
                ChangeState(GameState.SelectStage_LoadStage);
                break;
            default:
                break;
        }
    }
    
    /// <summary>
    /// 플레이 씬으로 넘어가는 함수.
    /// </summary>
    void LoadStage()
    {
        InputTempDataAboutNextScene("Play");
        LoadLoadingScene();
    }

}

