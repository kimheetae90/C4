using UnityEngine;
using System.Collections;

public class CSelectChapterManager : SceneManager
{
    public GameObject buttonPrefab;
    int chapterCount = 5;
    CChapter chapter;
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        ChangeState(GameState.SelectChapter_Ready);
    }

    void Update()
    {
        UpdateState();
    }

    public override void DispatchInputData(InputData _inputData)
    {
        if (_inputData.keyState == InputData.KeyState.Up)
        {
            OnClickToStartSelectStage(_inputData.downRootGameObject);
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
            case GameState.SelectChapter_Ready:
                CreateChapterButtons(chapterCount);
                break;
            case GameState.SelectChapter_LoadSelectStage:
                StartSelectStage();
                break;
        }
    }




    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    /// 

    /// <summary>
    /// 클릭한 챕터의 정보를 처리하는 함수.
    /// </summary>
    /// <param name="_selectedGameObject"></param>
    void OnClickToStartSelectStage(GameObject _selectedGameObject)
    {
        switch (_selectedGameObject.tag)
        {
            case "SelectChapter_Chapter":
                CChapter tempChapter = _selectedGameObject.GetComponentInChildren<CChapter>();
                GameMaster.Instance.tempData.Insert("chapterNum", tempChapter.chapterNum);
                GameMaster.Instance.tempData.Insert("chapterName", tempChapter.chapterName);
                ChangeState(GameState.SelectChapter_LoadSelectStage);
                break;
            default:
                break;
        }
    }

    void StartSelectStage()
    {
        StartCoroutine("LoadSelectStage");
    }

    /// <summary>
    /// SelectStage씬으로 넘어가기 위한 코루틴함수.
    /// </summary>
    IEnumerator LoadSelectStage()
    {
        AsyncOperation async = Application.LoadLevelAsync("SelectStage");

        while (async.isDone == false)
            yield return null;
    }

    void CreateChapterButtons(int ChapterCount)
    {
        GameObject chapterObject = new GameObject("Chapter");
        chapterObject.tag = "SelectChapter_Chapter";
        for (int i = 0; i < ChapterCount; i++)
        {
            GameObject button = MonoBehaviour.Instantiate(buttonPrefab) as GameObject;
            chapter = button.GetComponent<CChapter>();
            chapter.chapterName = "Chapter" + i;
            chapter.chapterNum = i;
            button.transform.SetParent(chapterObject.transform);
            float xPos = -6 + 3 * i;
            button.name = "Chapter_" + i; // name을 변경
            button.transform.position = new Vector3(xPos, 0, 0);
        }
    }
}