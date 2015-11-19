using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class CSelectStageManager : SceneManager
{
    public GameObject stageButton;
    public GameObject skillButton;
    int stageCount = 12;
    CStage stage;
    GameObject selectSkillPanel;
    List<GameObject> skillButtons = new List<GameObject>();
    List<string> curSkillList;
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
    {
       if(curSkillList.Count>0)
        Debug.Log(curSkillList.Count);
    }

    protected override void ChangeState(GameState _gameState)
    {
        gameState = _gameState;

        switch (gameState)
        {
            case GameState.SelectStage_Ready:
                InitPopup();
                InitSkillButton();
                CreateStageButtons(stageCount);
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
        if (_selectedGameObject == null) return;
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
        Debug.Log(curSkillList.Count);
        InputTempDataAboutNextScene("Play", curSkillList);
        LoadLoadingScene();
    }
    protected void InputTempDataAboutNextScene(string _scene_name, List<string> skillList)
    {
        GameMaster.Instance.tempData.Insert("next_scene", _scene_name);
        for (int i = 0; i < skillList.Count; i++)
        {
            GameMaster.Instance.tempData.Insert("skill_" + i, skillList[i]);
            Debug.Log(GameMaster.Instance.tempData.Get("skill_" + i).ToString());
        }
    }

    void CreateStageButtons(int StageCount)
    {
        GameObject stageObject = new GameObject("Stage");
        stageObject.tag = "SelectStage_Stage";

        for (int i = 0; i < StageCount; i++)
        {
            GameObject button = MonoBehaviour.Instantiate(stageButton) as GameObject;
            stage = button.GetComponent<CStage>();
            stage.stageName = "Level" + "i";
            stage.stageNum = i;
            button.transform.SetParent(stageObject.transform);
            float xPos = -8 + 1.5f * i;
            button.name = "Stage_" + i; // name을 변경
            button.transform.position = new Vector3(xPos, 0, 0);
        }
    }

    private void InitSkillButton()
    {
        // 아마 무조건 스킬 버튼이 고정일 것 같아서 (현재 기획서상 3개) 이렇게 코드작성하였음.
        // 나중에 유저가 아직 스킬을 3개 이상 가지고 있지 않을때는 xml이 없어서 예외처리 안했음.
        int skillCount = 3;

        for (int i = 0; i < skillCount; i++)
        {
            GameObject skillButton = GameObject.Find("Button_SelectedSkill_" + i);
            skillButton.GetComponent<Button>().onClick.RemoveAllListeners();
            skillButton.GetComponent<Button>().onClick.AddListener(OpenPopup);
        }
    }

    private void ChangeSkill()
    {
        
    }
    private void InitPopup()
    {
        selectSkillPanel = GameObject.Find("Panel_SelectSkill");
        GameObject.Find("Button_Close").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button_Close").GetComponent<Button>().onClick.AddListener(ClosePopup);
        InitPopupSkillList();
        selectSkillPanel.SetActive(false);
    }

    private void InitPopupSkillList()
    {
        GameObject stageObject = new GameObject("Stage");
        stageObject.tag = "SelectStage_Stage";

        int skillCount = 10;

        int row = 0;
        int column = 0;
        int columnCount = 7;
        for (int i = 0; i < skillCount; i++)
        {
            if (i != 0 && i % columnCount == 0)
            {
                column = 0;
                row++;
            }

            GameObject buttonObj = MonoBehaviour.Instantiate(skillButton) as GameObject;

            // TODO : 나중에 이런 버튼 생성류 함수 따로 유틸함수로 빼야될듯.
            buttonObj.transform.SetParent(selectSkillPanel.transform);
            buttonObj.GetComponentInChildren<Text>().text = "skill_" + i;
            Vector3 pos = new Vector3(-208 + (69 * column), 55 - (70 * row));
            buttonObj.GetComponent<RectTransform>().localPosition = pos;

            buttonObj.GetComponent<Button>().onClick.AddListener(delegate { AddSkill(buttonObj.GetComponent<Button>()); });
            skillButtons.Add(buttonObj);
            column++;
        }
    }

    List<string> tmpSkillList;

    private void OpenPopup()
    {
        selectSkillPanel.SetActive(true);
        tmpSkillList = new List<string>();
    }

    private void ClosePopup()
    {
        selectSkillPanel.SetActive(false);
        foreach (var sb in skillButtons)
        {
            sb.GetComponent<Button>().interactable = true;
        }
    }

    private void AddSkill(Button sButton)
    {
        sButton.interactable = false;
        tmpSkillList.Add(sButton.GetComponentInChildren<Text>().text);
        if(tmpSkillList.Count >= 3)
        {
            ClosePopup();
            ChangeCurSkills(tmpSkillList);
            curSkillList = tmpSkillList;
        }
    }

    private void ChangeCurSkills(List<string> curSkill)
    {
        int skillCount = 3;

        for (int i = 0; i < skillCount; i++)
        {
            GameObject skillButton = GameObject.Find("Button_SelectedSkill_" + i);
            skillButton.GetComponentInChildren<Text>().text = curSkill[i];
        }
    }

    //GameMaster.Instance.userInfo.setUserSkill();

}