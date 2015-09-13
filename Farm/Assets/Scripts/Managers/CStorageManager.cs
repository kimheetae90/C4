using UnityEngine;
using System.Collections;

public class CStorageManager : SceneManager
{
    public GameObject buttonPrefab;
    GameObject toolListBox;
    int toolCount;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        toolListBox = GameObject.Find("Tools");
        //toolCount = GetToolCount();
        toolCount = 10; // 임의로 10 넣어둠. 나중에 위에 있는 함수 사용해서 받아오면 됨.
        RectTransform toolListRect = toolListBox.GetComponent<RectTransform>();
        toolListRect.sizeDelta = new Vector2(70 * toolCount , 0);
        CreateToolButtons(10);
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

    int GetToolsCount()
    {
        if (GameMaster.Instance.tempData.Get("ToolCount") == null) return 0;

        return (int)GameMaster.Instance.tempData.Get("ToolCount");
    }

    void CreateToolButtons(int toolCount)
    {
        for(int i = 0; i<toolCount; i++)
        {
            GameObject button = MonoBehaviour.Instantiate(buttonPrefab) as GameObject;
            int xPos = 30 + (70 * i);
            button.name = "Button_Tool_" + i; // name을 변경
            button.transform.SetParent(toolListBox.transform);
            button.GetComponent<RectTransform>().localPosition = new Vector3(xPos,0,0);
        }
    }
}
