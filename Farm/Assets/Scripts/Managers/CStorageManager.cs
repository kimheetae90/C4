using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CStorageManager : SceneManager
{
    public GameObject buttonPrefab;
    GameObject toolListBox;
    int toolCount;
    int curToolID;
    List<int> ToolIDList;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        toolListBox = GameObject.Find("Tools");
        ToolIDList = GameMaster.Instance.bluePrint.GetToolIDList();
        toolCount = ToolIDList.Count;
        RectTransform toolListRect = toolListBox.GetComponent<RectTransform>();
        toolListRect.sizeDelta = new Vector2(70 * toolCount , 0);
        CreateToolButtons(toolCount);
    }

    void Update()
    {
        UpdateState();
    }

    public override void DispatchInputData(InputData _inputData)
    {
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

        switch(gameState)
        {
            case GameState.Storage_Ready:
                break;
            case GameState.Storage_ActivateButton:
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    /// 

    void CreateToolButtons(int toolCount)
    {
        for(int i = 0; i<toolCount; i++)
        {
            GameObject button = MonoBehaviour.Instantiate(buttonPrefab) as GameObject;
            int xPos = 30 + (70 * i);
            button.name = "Button_Tool_" + DataLoadHelper.Instance.GetToolInfo(ToolIDList[i]).id.ToString(); // name을 변경
            button.transform.SetParent(toolListBox.transform);
            button.GetComponent<RectTransform>().localPosition = new Vector3(xPos,0,0);
            button.GetComponentInChildren<Text>().text = DataLoadHelper.Instance.GetToolInfo(ToolIDList[i]).id.ToString();
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
        curToolID = id;
    }

    void ShowToolInfo(Button button)
    {
        string[] idString = button.name.Split('_');

        int id = int.Parse(idString[2]);
        // TODO : 현재 버튼 이름으로 id값 파싱하는 중. button 이름이 바뀌거나 하면 이 부분 수정해주어야함.

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
