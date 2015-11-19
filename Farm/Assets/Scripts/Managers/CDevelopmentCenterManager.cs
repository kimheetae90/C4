using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class CDevelopmentCenterManager : SceneManager
{
    public GameObject toolPrefab;
    GameObject toolListBox;
    int toolCount;
    List<int> toolIDList;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        toolListBox = GameObject.Find("Tools");
        toolIDList = GameMaster.Instance.bluePrint.GetToolIDList();
        toolCount = toolIDList.Count;
        RectTransform toolListRect = toolListBox.GetComponent<RectTransform>();
        toolListRect.sizeDelta = new Vector2(75 * toolCount, 0);
        CreateToolButtons();
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
        switch (_gameMessage.messageName)
        {
            case MessageName.DevelopmentCenter_ActivateToolButton:
//                OneWaveOver();
                break;

            default:
                Broadcast(_gameMessage);
                break;
        }
        _gameMessage.Destroy();
    }

    protected override void UpdateState()
    { }

    protected override void ChangeState(GameState _gameState)
    {
        gameState = _gameState;

        switch (gameState)
        {
            case GameState.DevelopmentCenter_Ready:
                break;

            case GameState.DevelopmentCenter_ActivateButton:
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    /// 

    //    upgradeButton.onClick.AddListener(delegate { UpgrageTool(curToolID); });
    
    void CreateToolButtons()
    {
        for (int i = 0; i < toolIDList.Count; i++)
        {
            GameObject toolPanel = MonoBehaviour.Instantiate(toolPrefab) as GameObject;
            int xPos = 35 + (75 * i);
            toolPanel.name = "Panel_Tool_" + toolIDList[i];
            toolPanel.transform.SetParent(toolListBox.transform);
            toolPanel.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 10, 0);

            var texts = toolPanel.GetComponentsInChildren<Text>();
            foreach(var tInfoText in texts)
            {
                if(tInfoText.gameObject.name == "Text_ToolInfo")
                {
                    tInfoText.text = toolIDList[i].ToString();
                }
            }

            Button buyButton = toolPanel.GetComponentInChildren<Button>();
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(delegate { BuyTool(toolPanel); });
        }
    }

    void BuyTool(GameObject tool)
    {
        int id = int.Parse(tool.name.Split('_')[2]);
        GameMaster.Instance.myTool.BuyNewTool(id);
    }
}