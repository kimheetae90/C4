using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class CMainManager : SceneManager
{
    public GameObject storageButtonPrefab;
    GameObject storageToolListBox;
    int storageToolCount;
    int curStorageToolID;
    List<int> storageToolIDList;

    public GameObject devToolPrefab;
    GameObject devToolListBox;
    int devToolCount;
    List<ToolInfo> devToolInfoList;
    List<int> devToolIDList;

    public GameObject chapterButton;
    public GameObject stageButton;
    public GameObject skillButton;

    GameObject mainQuadUI;
    GameObject selectChapterQuadUI;
    GameObject selectStageQuadUI;

    GameObject mainUI;
    GameObject storageUI;
    GameObject developmentCenterUI;

    GameObject selectSkillPanel;
    List<GameObject> skillButtons = new List<GameObject>();
    List<string> curSkillList;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        ChangeState(GameState.Main_Ready);
    }

    void Update()
    {
        UpdateState();
    }

    public override void DispatchInputData(InputData _inputData)
    {
        if (_inputData.keyState == InputData.KeyState.Up)
        {
            OnClickToLoadSelectChapter(_inputData.downCorrectGameObject);
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
            case GameState.Main_Ready:
                InitUIs();
                InitPopup();
                InitSkillButton();
                OffUIs();
                ShowMainUI();
                break;
            case GameState.GameLoading_LoadMainScene:
                break;
            case GameState.Main_LoadStorage:
                StartStorage();
                break;
            case GameState.Main_LoadDevelopmentCenter:
                StartDevelopmentCenter();
                break;
            case GameState.Main_SelectChapter:
                ShowSelectChapterUI();
                break;
            case GameState.Main_SelectStage:
                ShowSelectStageUI();
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////

    private void OffUIs()
    {
        mainUI.SetActive(false);
        storageUI.SetActive(false);
        developmentCenterUI.SetActive(false);
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    void ShowMainUI()
    {
        mainUI.SetActive(true);
        mainQuadUI.SetActive(true);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    void ShowSelectChapterUI()
    {
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(true);
        selectStageQuadUI.SetActive(false);
    }
    void ShowSelectStageUI()
    {
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(true);
    }

    void OnClickToLoadSelectChapter(GameObject _selectedGameObject)
    {
        switch (_selectedGameObject.tag)
        {
            case "Main_ToStorage":
                ChangeState(GameState.Main_LoadStorage);
                break;
            case "Main_ToDevCenter":
                ChangeState(GameState.Main_LoadDevelopmentCenter);
                break;
            case "Main_ToSelectChapter":
                ChangeState(GameState.Main_SelectChapter);
                break;
            case "Main_ToSelectStage":
                CChapter tempChapter = _selectedGameObject.GetComponent<CChapter>();
                GameMaster.Instance.tempData.Insert("chapterNum", tempChapter.chapterNum);
                GameMaster.Instance.tempData.Insert("chapterName", tempChapter.chapterName);
                ChangeState(GameState.Main_SelectStage);
                break;
            case "Main_SelectChapter":
                ChangeState(GameState.SelectChapter_LoadSelectStage);
                break;
            case "Main_SelectStage":
                ChangeState(GameState.Loading_LoadPlay);
                break;
            case "Main_LoadStage":
                CStage tempStage = _selectedGameObject.GetComponent<CStage>();
                GameMaster.Instance.tempData.Insert("stageNum", tempStage.stageNum);
                GameMaster.Instance.tempData.Insert("stageName", tempStage.stageName);
                LoadStage();
                break;
            default:
                break;
        }
    }

    private void InitUIs()
    {
        mainUI = GameObject.Find("MainUI");
        storageUI = GameObject.Find("StorageUI");
        developmentCenterUI = GameObject.Find("DevelopCenterUI");
        mainQuadUI = GameObject.Find("MainQuadUI");
        selectChapterQuadUI = GameObject.Find("SelectChapterQuadUI");
        CreateChapterButtons(5);
        selectStageQuadUI = GameObject.Find("SelectStageQuadUI");
        CreateStageButtons(10);
    }

    public void StartDevelopmentCenter()
    {
        mainUI.SetActive(false);
        storageUI.SetActive(false);
        developmentCenterUI.SetActive(true);
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    public void StartStorage()
    {
        mainUI.SetActive(false);
        storageUI.SetActive(true);
        developmentCenterUI.SetActive(false);
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    public void StartMain()
    {
        mainUI.SetActive(true);
        storageUI.SetActive(false);
        developmentCenterUI.SetActive(false);
        mainQuadUI.SetActive(true);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    void CreateChapterButtons(int ChapterCount)
    {
        for (int i = 0; i < ChapterCount; i++)
        {
            GameObject chapterObject = MonoBehaviour.Instantiate(chapterButton) as GameObject;
            chapterObject.AddComponent<CChapter>();
            chapterObject.GetComponent<CChapter>().chapterName = "Chapter" + i;
            chapterObject.GetComponent<CChapter>().chapterNum = i;
            chapterObject.transform.SetParent(selectChapterQuadUI.transform);

            float xPos = -4f + (2.0f) * i;
            chapterObject.name = "Chapter_" + i; // name을 변경
            chapterObject.transform.position = new Vector3(xPos, 0, 0);
        }
    }

    void CreateStageButtons(int StageCount)
    {
        for (int i = 0; i < StageCount; i++)
        {
            GameObject stageObject = MonoBehaviour.Instantiate(stageButton) as GameObject;
            stageObject.AddComponent<CStage>();
            stageObject.GetComponent<CStage>().stageName = "Stage" + i;
            stageObject.GetComponent<CStage>().stageNum = i;
            stageObject.transform.SetParent(selectStageQuadUI.transform);

            float xPos = -8f + (2.0f) * i;
            stageObject.name = "Stage_" + i; // name을 변경
            stageObject.transform.position = new Vector3(xPos, 0, 0);
        }
    }

    void LoadStage()
    {
        InputTempDataAboutNextScene("Play", curSkillList);
        LoadLoadingScene();
    }

    protected void InputTempDataAboutNextScene(string _scene_name, List<string> skillList)
    {
        GameMaster.Instance.tempData.Insert("next_scene", _scene_name);
        for (int i = 0; i < skillList.Count; i++)
        {
            GameMaster.Instance.tempData.Insert("skill_" + i, skillList[i]);
        }
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
        if (tmpSkillList.Count >= 3)
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

        // dev center init toolButton
        devToolListBox = GameObject.Find("DevTools");
        devToolInfoList = DataLoadHelper.Instance.GetToolList();
        devToolIDList = GameMaster.Instance.bluePrint.GetToolIDList();
        devToolCount = devToolIDList.Count;
        RectTransform toolListRect = devToolListBox.GetComponent<RectTransform>();
        toolListRect.sizeDelta = new Vector2(75 * devToolCount, 0);
        CreateDevToolButtons();

        // storage init toolButton
        storageToolListBox = GameObject.Find("StorageTools");
        storageToolIDList = GameMaster.Instance.myTool.GetToolIDList();
        storageToolCount = storageToolIDList.Count;
        RectTransform storageToolListRect = storageToolListBox.GetComponent<RectTransform>();
        toolListRect.sizeDelta = new Vector2(70 * storageToolCount, 0);
        InitButtons();
        CreateToolButtons(storageToolCount);

    }

    void CreateDevToolButtons()
    {
        for (int i = 0; i < devToolInfoList.Count; i++)
        {
            GameObject toolPanel = MonoBehaviour.Instantiate(devToolPrefab) as GameObject;
            int xPos = 35 + (75 * i);
            toolPanel.name = "Panel_Tool_" + devToolInfoList[i].id;
            toolPanel.transform.SetParent(devToolListBox.transform);
            toolPanel.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 10, 0);

            var texts = toolPanel.GetComponentsInChildren<Text>();
            foreach (var tInfoText in texts)
            {
                if (tInfoText.gameObject.name == "Text_ToolInfo")
                {
                    tInfoText.text = devToolInfoList[i].id.ToString();
                }
            }

            Button buyButton = toolPanel.GetComponentInChildren<Button>();
            if (devToolInfoList[i].open == 0)
            {
                buyButton.interactable = false;
            }
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(delegate { BuyTool(toolPanel); });
        }
    }

    void BuyTool(GameObject tool)
    {
        int id = int.Parse(tool.name.Split('_')[2]);
        GameMaster.Instance.myTool.BuyNewTool(id);
    }

    private void InitButtons()
    {
        Button upgradeButton = GameObject.Find("Button_Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(delegate { UpgrageTool(curStorageToolID); });
    }

    private void UpgrageTool(int curToolID)
    {
        Debug.Log(curToolID);
        int instance = GameMaster.Instance.myTool.GetInstanceByToolID(curToolID);
        GameMaster.Instance.myTool.LevelUp(instance);
    }

    void CreateToolButtons(int toolCount)
    {
        for (int i = 0; i < toolCount; i++)
        {
            GameObject button = MonoBehaviour.Instantiate(storageButtonPrefab) as GameObject;
            int xPos = 30 + (70 * i);
            button.name = "Button_Tool_" + DataLoadHelper.Instance.GetToolInfo(storageToolIDList[i]).id.ToString(); // name을 변경
            button.transform.SetParent(storageToolListBox.transform);
            button.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 0, 0);
            button.GetComponentInChildren<Text>().text = DataLoadHelper.Instance.GetToolInfo(storageToolIDList[i]).id.ToString();
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(delegate { ShowToolInfo(button.GetComponent<Button>()); });
        }
    }

    public void OnPointerClick(Button button)
    {
        Debug.Log(button.name);
    }

    public void ChangeCurToolId(int id)
    {
        curStorageToolID = id;
    }

    void ShowToolInfo(Button button)
    {
        string[] idString = button.name.Split('_');
        int id = int.Parse(idString[2]);
        // TODO : 현재 버튼 이름으로 id값 파싱하는 중. button 이름이 바뀌거나 하면 이 부분 수정해주어야함.

        ChangeCurToolId(id);

        Text ToolInfoText = GameObject.Find("Text_Tools_Info").GetComponent<Text>();
        ToolInfoText.text = "HP : " + DataLoadHelper.Instance.GetToolInfo(id).hp.ToString() + "\n";
        ToolInfoText.text += "Power : " + DataLoadHelper.Instance.GetToolInfo(id).power.ToString() + "\n";
        ToolInfoText.text += "Range : " + DataLoadHelper.Instance.GetToolInfo(id).range.ToString() + "\n";
        ToolInfoText.text += "PF : " + DataLoadHelper.Instance.GetToolInfo(id).piercingForce.ToString() + "\n";
        ToolInfoText.text += "AS : " + DataLoadHelper.Instance.GetToolInfo(id).attackSpeed.ToString() + "\n";
        ToolInfoText.text += "MS : " + DataLoadHelper.Instance.GetToolInfo(id).moveSpeed.ToString() + "\n";
        ToolInfoText.text += "Price : " + DataLoadHelper.Instance.GetToolInfo(id).price.ToString() + "\n";

        Text ToolNameText = GameObject.Find("Text_Tool_Name").GetComponent<Text>();
        ToolNameText.text = "Name" + "\n" + DataLoadHelper.Instance.GetToolInfo(id).id.ToString();

        Text Text_Tool_Price = GameObject.Find("Text_Tool_Price").GetComponent<Text>();
        Text_Tool_Price.text = "Upgrade Price" + "\n" + DataLoadHelper.Instance.GetToolInfo(id).upgradePrice.ToString();
    }

}