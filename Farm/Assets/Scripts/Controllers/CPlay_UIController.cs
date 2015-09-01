using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CPlay_UIController : Controller
{
    public Canvas canvas;
    public Text waveText;
    public Image waveTimer;
    public Image maintainTimer;

    public GameObject clearPopup;
    public GameObject failedPopup;
    void Awake()
    {
        ResetUI();
    }

    protected override void Start()
    {
        base.Start();
        
    }
	// Use this for initialization

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_MaintainOver: WaveTextChange((int)_gameMessage.Get("wavecount"));
                break;
            case MessageName.Play_UIWaveTimerAmount: WavetimerFill((float)_gameMessage.Get("timerAmount"));
                break;
            case MessageName.Play_UIMaintainTimerAmount: MaintainTimerFill((float)_gameMessage.Get("timerAmount"));
                break;
            case MessageName.Play_StageClear: ShowStageClearPopup();
                break;
            case MessageName.Play_StageFailed: ShowStageFailedPopup();
                break;

        }
    }
    /// <summary>
    /// popup들을 모두 감추고,
    /// WaveTimer와 MaintainTimer를 모두 채워서 그림.
    /// </summary>
    void ResetUI() {
        HidePopup();
        WavetimerFill(1.0f);
        MaintainTimerFill(1.0f);
    }
    /// <summary>
    /// 정비시간이 끝나고 다음 웨이브가 시작되면 웨이브 카운트 TEXT를 바꿔준다.
    /// </summary>
    /// <param name="waveCount">바꿔줄 현재 웨이브의 카운트. PlayManager의 wave +1이다.</param>
    void WaveTextChange(int waveCount) {
        waveText.text = "Wave " + waveCount;
    }
    /// <summary>
    /// WaveTime(밤시간)일때 남은시간을 보여주는 Wavetimer를 남은 시간만큼 채운다.
    /// </summary>
    /// <param name="fillAmount">현재 웨이브의 남은시간. (남은시간/밤에 주어진시간)</param>
    void WavetimerFill(float fillAmount) {
        waveTimer.fillAmount = fillAmount;
    }
    /// <summary>
    /// MaintainTime(낮시간)일때 남은 시간을 보여주는 Maintaintimer를 남은 시간만큼 채운다.
    /// </summary>
    /// <param name="fillAmount">현재 남은 정비시간 (남은시간/낮에 주어진시간)</param>
    void MaintainTimerFill(float fillAmount) {
        maintainTimer.fillAmount = fillAmount;
    }
    /// <summary>
    /// 스테이지 클리어 팝업을 보여줌.
    /// </summary>
    void ShowStageClearPopup() {
        clearPopup.SetActive(true);
    }
    /// <summary>
    /// 스테이지 실패 팝업을 보여줌.
    /// </summary>
    void ShowStageFailedPopup() {
        failedPopup.SetActive(true);
    }
    /// <summary>
    /// 모든 팝업을 숨김.
    /// </summary>
    void HidePopup() {
        clearPopup.SetActive(false);
        failedPopup.SetActive(false);
    }
    /// <summary>
    /// 메인으로 가기 버튼을 눌렸을때 실행.
    /// PlayManager에게 게임메세지 Play_SceneChangeToHome를 보냄.
    /// </summary>
    public void toHomeButton() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_SceneChangeToHome);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 다음 스테이지로 가기 버튼을 눌렀을때 실행.
    /// PlayManager에게 게임메세지 Play_SceneChangeToNextStage를 보냄.
    /// </summary>
    public void toNextStageButton()
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_SceneChangeToNextStage);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 다시 시작하기 버튼을 눌렀을때 실행.
    /// PlayManager에게 게임메세지Play_SceneChangeToRestart를 보냄.
    /// </summary>
    public void toRestartButton()
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_SceneChangeToRestart);
        SendGameMessage(gameMsg);
    }
}
