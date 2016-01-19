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

    GameObject mainQuadUI;
    GameObject selectChapterQuadUI;
    GameObject selectStageQuadUI;

    GameObject storageUI;
    GameObject developmentCenterUI;
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
                InitButtons();
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
        storageUI.SetActive(false);
        developmentCenterUI.SetActive(false);
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    void ShowMainUI()
    {
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
        storageUI.SetActive(false);
        developmentCenterUI.SetActive(true);
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    public void StartStorage()
    {
        storageUI.SetActive(true);
        developmentCenterUI.SetActive(false);
        mainQuadUI.SetActive(false);
        selectChapterQuadUI.SetActive(false);
        selectStageQuadUI.SetActive(false);
    }

    public void StartMain()
    {
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
        InputTempDataAboutNextScene("Play");
        LoadLoadingScene();
    }

    //protected void InputTempDataAboutNextScene(string _scene_name, List<string> skillList)
    //{
    //    GameMaster.Instance.tempData.Insert("next_scene", _scene_name);
    //    for (int i = 0; i < skillList.Count; i++)
    //    {
    //        GameMaster.Instance.tempData.Insert("skill_" + i, skillList[i]);
    //    }
    //}

    private void InitButtons()
    {
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
        Button upgradeButton = GameObject.Find("Button_Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(delegate { UpgrageTool(curStorageToolID); });
        CreateToolButtons(storageToolCount);

    }

    void CreateDevToolButtons()
    {
        for (int i = 0; i < devToolInfoList.Count; i++)
        {
            GameObject toolPanel = MonoBehaviour.Instantiate(devToolPrefab) as GameObject;
            int xPos = 60 * i;
            toolPanel.name = "Panel_Tool_" + devToolInfoList[i].id;
            toolPanel.transform.SetParent(devToolListBox.transform);
            toolPanel.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 10, 0);

            var texts = toolPanel.GetComponentsInChildren<Text>();
            foreach (var tInfoText in texts)
            {
                if (tInfoText.gameObject.name == "Text_ToolInfo")
                {
                    int tID = devToolInfoList[i].id;

                    tInfoText.text = tID.ToString() + "\n\n";

                    tInfoText.text += "HP : " + DataLoadHelper.Instance.GetToolInfo(tID).hp.ToString() + "\n";
                    tInfoText.text += "Power : " + DataLoadHelper.Instance.GetToolInfo(tID).power.ToString() + "\n";
                    tInfoText.text += "AS : " + DataLoadHelper.Instance.GetToolInfo(tID).attackSpeed.ToString() + "\n";
                    tInfoText.text += "Price : " + DataLoadHelper.Instance.GetToolInfo(tID).price.ToString() + "\n";

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
        
        storageToolIDList.Add(id);
        
        GameObject buttonObject = MonoBehaviour.Instantiate(storageButtonPrefab) as GameObject;
        Button button = buttonObject.GetComponent<Button>();

        int xPos = 30 + (70 * storageToolCount);
        buttonObject.name = "Button_Tool_" + DataLoadHelper.Instance.GetToolInfo(storageToolIDList[storageToolCount]).id.ToString(); // name을 변경
        buttonObject.GetComponentInChildren<Text>().text = DataLoadHelper.Instance.GetToolInfo(storageToolIDList[storageToolCount]).id.ToString();
        buttonObject.transform.SetParent(storageToolListBox.transform);
        buttonObject.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 0, 0);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { ShowToolInfo(button); });

        buttons.Add(storageToolCount, button);
    }

    private void UpgrageTool(int curToolID)
    {
        Debug.Log(curToolID);
        int instance = GameMaster.Instance.myTool.GetInstanceByToolID(curToolID);
        GameMaster.Instance.myTool.LevelUp(instance);
    }

    Dictionary<int, Button> buttons = new Dictionary<int, Button>();

    void CreateToolButtons(int toolCount)
    {
        if(toolCount < 1) return;

        for (int i = 0; i < toolCount; i++)
        {
            GameObject buttonObject = MonoBehaviour.Instantiate(storageButtonPrefab) as GameObject;
            Button button = buttonObject.GetComponent<Button>();

            int xPos = 30 + (70 * i);
            buttonObject.name = "Button_Tool_" + DataLoadHelper.Instance.GetToolInfo(storageToolIDList[i]).id.ToString(); // name을 변경
            buttonObject.transform.SetParent(storageToolListBox.transform);
            buttonObject.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 0, 0);
            buttonObject.GetComponentInChildren<Text>().text = DataLoadHelper.Instance.GetToolInfo(storageToolIDList[i]).id.ToString();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate { ShowToolInfo(button); });

            buttons.Add(i, button);
        }

        Button b;
        buttons.TryGetValue(0, out b);
        ShowToolInfo(b);
    }

    void ShowToolInfo(Button button)
    {
        foreach(var b in buttons)
        {
            if(b.Value == button)
            {
                b.Value.interactable =false;
            }
            else
            {
                b.Value.interactable = true;
            }
        }

        string[] idString = button.name.Split('_');
        int id = int.Parse(idString[2]);
        // TODO : 현재 버튼 이름으로 id값 파싱하는 중. button 이름이 바뀌거나 하면 이 부분 수정해주어야함.

        curStorageToolID = id;

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