using UnityEngine;
using System.Collections;

public class CPlayManager : SceneManager {

    int stageNum;
    string stageName;
    int chapterNum;
    string chapterName;
		
	int wave;
	public const int maxWave = 5;

    public float managementTime =5f;
    public float waveLimitTime = 30f;
    public float limitTime;

	protected override void Awake()
	{
		base.Awake ();
		ChangeState (GameState.Play_Init);
		Init ();
	}
	
	void Start()
	{
        //ObjectPooler.Instance.GetGameObject("Background");
	}
	
	void Update () {
		UpdateState ();
	}
	
	public override void DispatchInputData (InputData _inputData)
	{
        switch(_inputData.keyState)
        {
            case InputData.KeyState.Press:
                CameraMove(_inputData);
                break;
            case InputData.KeyState.Up:
                BroadcastClickMsg(_inputData);
                break;
        }
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.Play_OneWaveOver:
			OneWaveOver();
			break;
        case MessageName.Play_SceneChangeToHome:
            SceneChangeToHome();
            break;
        case MessageName.Play_SceneChangeToNextStage:
            SceneChangeToNextStage();
            break;
        case MessageName.Play_SceneChangeToRestart:
            //SceneChangeToRestart();
            ResetStage();
            break;
        case MessageName.Play_MissleOrderedByTool:
            Broadcast(_gameMessage);
            break;
        case MessageName.Play_StageFailed:
            ChangeState(GameState.Play_Failed);
            break;

        default :
            Broadcast(_gameMessage);
            break;
		}

		_gameMessage.Destroy ();
	}
	
	protected override void UpdateState ()
	{
        limitTime -= Time.deltaTime;

        if (gameState == GameState.Play_Start)
        {
            WaveTimeUIUpdate();
            
            if (limitTime <= 0)
            {
                WaveTimeOver();
            }
        }

        if (gameState == GameState.Play_Management) 
        {
            MaintainTimeUIUpdate();

            if (limitTime <= 0)
            {
                ManagementOver();
            }
        }

	}
	
	protected override void ChangeState (GameState _gameState)
	{
		gameState = _gameState;

		switch(gameState)
		{
		case GameState.Play_Init:
			break;

        case GameState.Play_Reset: Reset();
			break;

		case GameState.Play_Ready:	
			break;

		case GameState.Play_Start:
            limitTime = waveLimitTime;
			break;

        case GameState.Play_Management:
            limitTime = managementTime;
			break;

        case GameState.Play_Clear: StageClear();
            break;
        case GameState.Play_Failed: StageFailed();
            break;
                
		}
	}
	
	
	
	
	///////////////////////////////////////////////////////////////////////////////
	//////////////////////// 			구현               ////////////////////////
	///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 게임마스터에서 현재 stage와 chapter정보를 받아옴.
    /// 웨이브 카운터인 wave를 초기화 하고
    /// GameState를 Reset으로 바꿈.
    /// </summary>
	void Init()
	{
        //stageNum = (int)GameMaster.Instance.tempData.Get("stageNum");
        //stageName = (string)GameMaster.Instance.tempData.Get("stageName");
        //chapterNum = (int)GameMaster.Instance.tempData.Get("chapterNum");
        //chapterName = (string)GameMaster.Instance.tempData.Get("chapterName");
        GameMaster.Instance.tempData.Clear();
		wave = 0;
        ChangeState(GameState.Play_Management);
		//ChangeState (GameState.Play_Reset);
        UnPause();
	}

    /// <summary>
    /// 제한시간을 밤에 주어진 시간(waveLimitTime)으로 초기화 하고
    /// 첫번째 웨이브(시작할때)가 아니면 게임메세지 Play_MaintainOver를 broadcast한다.
    /// GameState를 Start로 바꾼다.
    /// </summary>
	void Reset()
	{
        Debug.Log(wave+1+"번째 웨이브");

        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MaintainOver);
        gameMsg.Insert("wavecount", wave+1);
        Broadcast(gameMsg);
        ChangeState (GameState.Play_Start);
	}

    
    /// <summary>
    /// wavetime(밤시간)일때 항상 불려져서 ui가 남은 시간만큼 보이게 함.
    /// Play_UIController에게 게임메세지 Play_UIWaveTimerAmount를 보냄.
    /// </summary>
    void WaveTimeUIUpdate() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_UIWaveTimerAmount);
        gameMsg.Insert("timerAmount", limitTime / waveLimitTime);
        Broadcast(gameMsg);

    }
    /// <summary>
    /// maintaintime(낮시간)일때 항상 불려져서 ui가 남은시간만큼 보이게 함.    
    /// Play_UIController에게 게임메세지 Play_UIMaintainTimerAmount를 보냄.
    /// </summary>
    void MaintainTimeUIUpdate() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_UIMaintainTimerAmount);
        gameMsg.Insert("timerAmount", limitTime / managementTime);
        Broadcast(gameMsg);

    }
    /// <summary>
    /// wavetime(밤시간)이 끝나면 불려져서 MonsterController에게 게임메세지 Play_MonsterReturn를 보내서
    /// 몬스터들이 돌아가게 만들고
    /// 한웨이브가 끝남을 알림.
    /// </summary>
    void WaveTimeOver() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterReturn);
        Broadcast(gameMsg);
        OneWaveOver();
    }

    /// <summary>
    /// 한 웨이브가 끝났을 때 호출됨.
    /// 게임상태가 Failed가 아니면
    /// 웨이브 카운터 wave를 업데이트 하고
    /// 모든 웨이브가 끝났으면 게임상태를 Clear로 바꾸고,
    /// 아직 덜 끝났으면 게임상태를 Management로 바꾼다.
    /// Play_UIController에게 게임메세지 Play_UIWaveTimerAmount를 보내 남은시간을 0으로 보이게 한다.
    /// 제한시간을 managementTime(낮시간)으로 초기화 해줌.
    /// </summary>
    void OneWaveOver()
    {
        if (gameState != GameState.Play_Failed)
        {
            wave++;
            if (wave == maxWave)
            {
                ChangeState(GameState.Play_Clear);
            }
            else
            {
                ChangeState(GameState.Play_Management);
            }

            GameMessage gameMsg = GameMessage.Create(MessageName.Play_UIWaveTimerAmount);
            gameMsg.Insert("timerAmount", 0.0f);
            Broadcast(gameMsg);
        }
    }

    /// <summary>
    /// managementTime(낮시간)이 끝나면 불려짐.
    /// Reset함수를 호출하고
    /// Play_UIController에게 게임메세지 Play_UIMaintainTimerAmount를 호출해서 maintaintimerUI를 꽉차게 그린다.(배경색으로도씀)
    /// </summary>
    void ManagementOver() {
          Reset();
          GameMessage gameMsg = GameMessage.Create(MessageName.Play_UIMaintainTimerAmount);
          gameMsg.Insert("timerAmount", 1.0f);
          Broadcast(gameMsg);
    }

    /// <summary>
    /// 메인 씬으로 이동하는 LoadMain 코루틴을 호출.
    /// </summary>
    void SceneChangeToHome() {
        StartCoroutine("LoadMain");
    }

    /// <summary>
    /// 다음 스테이지로 이동하기 전 Maintain씬으로 이동하는 코루틴 LoadMaintain을 호출
    /// 아직 다음스테이지의 정보를 넘겨주는 등의 것은 없음.
    /// </summary>
    void SceneChangeToNextStage() {
        StartCoroutine("LoadMaintain");
    }
    /// <summary>
    /// 스테이지를 다시 시작하기 위해 gamemaster에게 현재 스테이지 정보를 넘기고
    /// LoadPlay 코루틴을 호출해서 play씬을 다시 실행.
    /// </summary>
    void SceneChangeToRestart()
    {
        GameMaster.Instance.tempData.Insert("stageNum", stageNum);
        GameMaster.Instance.tempData.Insert("stageName", stageName);
        GameMaster.Instance.tempData.Insert("chapterNum", chapterNum);
        GameMaster.Instance.tempData.Insert("chapterName", chapterName);
        StartCoroutine("LoadPlay");
    }

    /// <summary>
    /// 스테이지를 클리어하면 게임메세지 Play_StageClear를 Broadcast함.
    /// </summary>
    void StageClear() {

        Debug.Log("스테이지 클리어");
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_StageClear);
        Broadcast(gameMsg);
    }

    /// <summary>
    /// 스테이지를 실패하면 게임메세지 Play_StageFailed를 Broadcast함.
    /// </summary>
    void StageFailed()
    {
        Debug.Log("스테이지 클리어 실패");
        Pause();
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_StageFailed);
        Broadcast(gameMsg);
    }

    /// <summary>
    /// 마우스 클릭을 떼었을 때 clickposition과 dragpositin의 거리차이가 일정거리(0.2) 이하일때 클릭으로 간주하고
    /// PlayerController에게 게임메세지 Play_PlayerMove를 보내 플레이어를 이동시킴.
    /// </summary>
    /// <param name="_inputData">현재의 inputData</param>
    void BroadcastClickMsg(InputData _inputData)
    {
        if(Vector3.Distance(_inputData.clickPosition,_inputData.dragPosition)<=0.2f){
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_PlayerMove);
        gameMsg.Insert("ClickPosition", _inputData.clickPosition);
        gameMsg.Insert("SelectedGameObject", _inputData.selectedGameObject);
        Broadcast(gameMsg);
        }
    }

    /// <summary>
    /// 마우스를 드래그 하면 불려져서 CameraController에게 게임메세지 Play_CameraMove를 보내서
    /// 카메라를 이동시킴.
    /// </summary>
    /// <param name="_inputData">현재의 inputData를 보냄.</param>
    void CameraMove(InputData _inputData)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_CameraMove);
        gameMsg.Insert("inputData", _inputData);
        Broadcast(gameMsg);
    }

    /// <summary>
    /// maintain(정비)씬으로 이동.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadMaintain() 
	{
        AsyncOperation async = Application.LoadLevelAsync("Maintain");
		
		while (!async.isDone) 
		{
			yield return null;
		}
	}
    /// <summary>
    /// main씬으로 이동.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadMain()
    {
        AsyncOperation async = Application.LoadLevelAsync("Main");

        while (!async.isDone)
        {
            yield return null;
        }
    }
    /// <summary>
    /// 현재 씬인 play씬을 다시 실행.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadPlay()
    {
        AsyncOperation async = Application.LoadLevelAsync("Play");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    void Pause() {

        Time.timeScale = 0;
    }
    void UnPause() {
        Time.timeScale = 1;
    }


    void ResetStage() {

        wave = 0;
        UnPause();
        ChangeState(GameState.Play_Management);
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_StageRestart);
        Broadcast(gameMsg);
    }
}
