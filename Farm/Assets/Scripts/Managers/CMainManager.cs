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

    public GameObject chapterButton;
    public GameObject stageButton;

    GameObject mainQuadUI;
    //GameObject selectChapterQuadUI;
    //GameObject selectStageQuadUI;

    GameObject storageUI;
    GameObject stageUI;
    Image storageCurToolImage;

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
            case GameState.Main_SelectChapter:
                StartSelectStage();
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////

    private void OffUIs()
    {
        storageUI.SetActive(false);
        mainQuadUI.SetActive(false);
        stageUI.SetActive(false);
    }

    void ShowMainUI()
    {
        mainQuadUI.SetActive(true);
    }

    void ShowSelectChapterUI()
    {
        mainQuadUI.SetActive(false);
    }
    void ShowSelectStageUI()
    {
        mainQuadUI.SetActive(false);
    }

    void OnClickToLoadSelectChapter(GameObject _selectedGameObject)
    {
        Debug.Log(_selectedGameObject.tag);

        switch (_selectedGameObject.tag)
        {
            case "Main_ToStorage":
                ChangeState(GameState.Main_LoadStorage);
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
                //CStage tempStage = _selectedGameObject.GetComponent<CStage>();
                //GameMaster.Instance.tempData.Insert("stageNum", tempStage.stageNum);
                //GameMaster.Instance.tempData.Insert("stageName", tempStage.stageName);
                //LoadStage();
                break;
            default:
                break;
        }
    }

    private void InitUIs()
    {
        storageUI = GameObject.Find("StorageUI");
        stageUI = GameObject.Find("SelectStageUI");
        mainQuadUI = GameObject.Find("MainQuadUI");
        //CreateChapterButtons(5);
        storageCurToolImage = GameObject.Find("Image_Tool").GetComponent<Image>();
        //CreateStageButtons(10);
    }

    public void StartStorage()
    {
        storageUI.SetActive(true);
        mainQuadUI.SetActive(false);
        stageUI.SetActive(false);
    }

    public void StartMain()
    {
        storageUI.SetActive(false);
        mainQuadUI.SetActive(true);
        stageUI.SetActive(false);
    }

    public void StartSelectStage()
    {
        storageUI.SetActive(false);
        mainQuadUI.SetActive(false);
        stageUI.SetActive(true);
    }

    //void CreateChapterButtons(int ChapterCount)
    //{
    //    for (int i = 0; i < ChapterCount; i++)
    //    {
    //        GameObject chapterObject = MonoBehaviour.Instantiate(chapterButton) as GameObject;
    //        chapterObject.AddComponent<CChapter>();
    //        chapterObject.GetComponent<CChapter>().chapterName = "Chapter" + (i + 1);
    //        chapterObject.GetComponent<CChapter>().chapterNum = (i + 1);
    //        chapterObject.transform.SetParent(selectChapterQuadUI.transform);

    //        float xPos = -4f + (2.0f) * i;
    //        chapterObject.name = "Chapter_" + (i+1); // name을 변경
    //        chapterObject.transform.position = new Vector3(xPos, 0, 0);
    //    }
    //}

    void CreateStageButtons(int StageCount)
    {
        int x = -200;
        int y = 60;

        for (int i = 0; i < StageCount; i++)
        {
            if (i != 0 && i % 5 == 0)
            {
                x = -200;
                y -= 140;
            }

            GameObject stageObject = MonoBehaviour.Instantiate(stageButton) as GameObject;

            stageObject.GetComponent<Button>().onClick.AddListener(delegate { LoadStage(stageObject); });

            CStage stageInfo = stageObject.AddComponent<CStage>();
            stageInfo.stageName = "Stage" + (i + 1);
            stageInfo.stageNum = (i + 1);

            GameObject parent = GameObject.Find("Panel_Stages");
            stageObject.transform.SetParent(parent.transform);
            stageObject.name = "Stage_" + (i + 1); // name을 변경
            stageObject.transform.position = new Vector3(466.5f + x, 242.5f + y, 0);
            x += 100;
        }
    }

    void LoadStage(GameObject stageObj)
    {
        GameMaster.Instance.tempData.Insert("chapterNum", 1);
        GameMaster.Instance.tempData.Insert("stageNum", stageObj.GetComponent<CStage>().stageNum);

        InputTempDataAboutNextScene("Play");
        LoadLoadingScene();
    }

    private void InitButtons()
    {
        // storage init toolButton
        storageToolListBox = GameObject.Find("StorageTools");
        storageToolIDList = GameMaster.Instance.myTool.GetToolIDList();
        storageToolCount = storageToolIDList.Count;
        RectTransform storageToolListRect = storageToolListBox.GetComponent<RectTransform>();
        storageToolListRect.sizeDelta = new Vector2(80 * storageToolCount, 0);
        Button upgradeButton = GameObject.Find("Button_Upgrade").GetComponent<Button>();
        upgradeButton.onClick.AddListener(delegate { UpgrageTool(curStorageToolID); });
        CreateToolButtons(storageToolCount);

        CreateStageButtons(10);
    }

    private void UpgrageTool(int curToolID)
    {
        int instance = GameMaster.Instance.myTool.GetInstanceByToolID(curToolID);
        GameMaster.Instance.myTool.LevelUp(instance);
    }

    Dictionary<int, Button> buttons = new Dictionary<int, Button>();
    Dictionary<int, Sprite> ToolSprites = new Dictionary<int, Sprite>();

    void CreateToolButtons(int toolCount)
    {
        if(toolCount < 1) return;

        for (int i = 0; i < toolCount; i++)
        {
            GameObject buttonObject = MonoBehaviour.Instantiate(storageButtonPrefab) as GameObject;
            Button button = buttonObject.GetComponent<Button>();

            int xPos = 50 + (105 * i);
            int toolId = DataLoadHelper.Instance.GetToolInfo(storageToolIDList[i]).id;
            buttonObject.name = "Button_Tool_" + toolId.ToString(); // name을 변경
            buttonObject.transform.SetParent(storageToolListBox.transform);
            buttonObject.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 0, 0);
            //Tool_11103_Icon
            string path = "UIs/Main/UI_Storage/Tool_" + toolId + "_Icon";

            Texture2D tmpTexture = Resources.Load(path) as Texture2D;
            Rect rect = new Rect(0, 0, tmpTexture.width, tmpTexture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(tmpTexture, rect, pivot);

            if (sprite == null) Debug.Log("이미지 못찾음");
            button.image.overrideSprite = sprite;

            ToolSprites.Add(toolId, sprite);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate { ShowToolInfo(button); });

            buttons.Add(toolId, button);
        }

        int count = 0;

        foreach(var b in buttons)
        {
            if(count == 0) ShowToolInfo(b.Value);
            count++;
        }
    }

    Dictionary<int, GameObject> selectedUIs = new Dictionary<int, GameObject>();

    void ShowToolInfo(Button button)
    {
        string[] idString = button.name.Split('_');
        int id = int.Parse(idString[2]);
        // TODO : 현재 버튼 이름으로 id값 파싱하는 중. button 이름이 바뀌거나 하면 이 부분 수정해주어야함.

        foreach (var b in buttons)
        {
            int buttonId = b.Key;

            foreach (var i in b.Value.gameObject.GetComponentsInChildren<Image>())
            {
                if (selectedUIs.ContainsKey(buttonId) == false)
                {
                    if (i.gameObject.name == "SelectedUI")
                    {
                        selectedUIs.Add(buttonId, i.gameObject);
                    }
                }
            }
        }

        foreach (var b in buttons)
        {
            if(b.Value == button)
            {
                b.Value.interactable = false;
            }
            else
            {
                b.Value.interactable = true;
            }
        }

        foreach(var ui in selectedUIs)
        {
            if (ui.Key == id)
            {
                ui.Value.SetActive(true);
            }
            else
            {
                ui.Value.SetActive(false);
            }
        }

        curStorageToolID = id;
        Sprite curSprite;
        ToolSprites.TryGetValue(id, out curSprite);
        storageCurToolImage.overrideSprite = curSprite;

        GameObject hp = GameObject.Find("Stat_HP");
        GameObject damage = GameObject.Find("Stat_AD");
        GameObject attackRate = GameObject.Find("Stat_AS");
        GameObject moveRate = GameObject.Find("Stat_MoveRate");

        float hpPer = DataLoadHelper.Instance.GetToolInfo(id).hp / 300.0f;
        float damagePer = DataLoadHelper.Instance.GetToolInfo(id).power / 50.0f; ;
        float attackRatePer = DataLoadHelper.Instance.GetToolInfo(id).attackSpeed / 10.0f;
        float moveRatePer = DataLoadHelper.Instance.GetToolInfo(id).moveSpeed / 100.0f;

        hp.GetComponentInChildren<Image>().fillAmount = hpPer;
        damage.GetComponentInChildren<Image>().fillAmount = damagePer;
        attackRate.GetComponentInChildren<Image>().fillAmount = attackRatePer;
        moveRate.GetComponentInChildren<Image>().fillAmount = moveRatePer;


        Text ToolNameText = GameObject.Find("Text_Tool_Name").GetComponent<Text>();
        ToolNameText.text = DataLoadHelper.Instance.GetToolInfo(id).name.ToString();

        Text Text_Tool_Price = GameObject.Find("Text_Tool_Price").GetComponent<Text>();
        Text_Tool_Price.text = DataLoadHelper.Instance.GetToolInfo(id).upgradePrice.ToString();
    }

}