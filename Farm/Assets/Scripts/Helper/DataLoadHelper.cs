using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLoadHelper : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    private static DataLoadHelper _instance;
    public static DataLoadHelper Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(DataLoadHelper)) as DataLoadHelper;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "DataLoadHelper";
                    _instance = container.AddComponent(typeof(DataLoadHelper)) as DataLoadHelper;
                }
            }

            return _instance;
        }
    }
    //////////////////////////////////////////////////////////////////////////////

    List<ToolInfo> toolInfoList;
    List<MonsterInfo> monsterInfoList;

    void Awake()
    {
        DontDestroyOnLoad(this);
        toolInfoList = new List<ToolInfo>();
        monsterInfoList = new List<MonsterInfo>();
        stageInfoList = new List<StageInfo>();
        ToolInfoLoad();
        MonsterInfoLoad();
    }

    void Start()
    {
        Debug.Log(GetToolInfo(13002).hp);
        Debug.Log(GetMonsterInfo(21101).attackSpeed);
        StageStart(1,1);
        foreach(StageInfo node in stageInfoList)
        {
            Debug.Log(node.wave + ", " + node.line + ", " + node.time + ", " + node.id);
        }
        StageClear();
        if (stageInfoList.Count == 0) Debug.Log("Clear");
    }

    void ToolInfoLoad()
    {
        ToolDataLoadHelper toolLoader = new ToolDataLoadHelper();
        toolInfoList = toolLoader.GetToolInfoList();
    }

    void MonsterInfoLoad()
    {
        MonsterDataLoadHelper monsterLoader = new MonsterDataLoadHelper();
        monsterInfoList = monsterLoader.GetMonsterInfoList();
    }

    public ToolInfo GetToolInfo(int _id)
    { 
        return toolInfoList.Find(x => x.id == _id);
    }

    public MonsterInfo GetMonsterInfo(int _id)
    {
        return monsterInfoList.Find(x => x.id == _id);
    }


    public List<StageInfo> GetStageInfo(int _chapter, int _stage)
    {
        List<StageInfo> stageInfoList = new List<StageInfo>();

        StageDataLoadHelper stageLoader = new StageDataLoadHelper();
        stageInfoList = stageLoader.GetStageInfo(_chapter, _stage);

        return stageInfoList;
    }
}
