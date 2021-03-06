﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CToolController : Controller
{

    public List<GameObject> toolList;

    public List<int> toolID;

    public List<Transform> startPos;

    //public List<ToolName> toolName;

    void Awake()
    {
        toolID.Add(0);
        toolID.Add(1);
        toolID.Add(2);
        Init();
    }

    protected override void Start()
    {
        
        base.Start();
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_PlayersObjectDamagedByMonster:
                ToolAttackedByEnemy((int)_gameMessage.Get("object_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_ToolAttackMonster:
                ToolAttackEnemy((int)_gameMessage.Get("tool_id"), (Vector3)_gameMessage.Get("tool_position"));
                break;
            case MessageName.Play_StageFailed:
                //ToolPause();
                break;
            case MessageName.Play_StageRestart:
                ResetStage();
                break;
            case MessageName.Play_MonsterDebuffToolsAttackSpeed:
                ToolsAttackSpeedDebuffed((int)_gameMessage.Get("object_id"));
                break;

        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    GameObject FindToolOfID(int _id)
    {
        return toolList.Find(tool => tool.GetComponent<CTool>().id == _id) as GameObject;
    }

    void Init()
    {
        toolList = new List<GameObject>();

        for (int i = 0; i < toolID.Count; i++)
        {
            //toolList.Add(ObjectPooler.Instance.GetGameObject("Play_Tool_PitchingMachine"));
            //toolList.Add(ObjectPooler.Instance.GetGameObject("Play_Tool_Drum"));
            ToolName toolNam = (ToolName)toolID[i];
            toolList.Add(ObjectPooler.Instance.GetGameObject(toolNam.ToString()));
            toolList[i].GetComponent<CTool>().SetController(this);
            toolList[i].GetComponent<CTool>().currentTileNum = i * 10 + 1;
            
            toolList[i].transform.position = startPos[i].position;
        }

    }

    /// <summary>
    /// 툴이 몬스터를 공격할 때 호출하는 함수.
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_tool_position"></param>
    void ToolAttackEnemy(int _id, Vector3 _tool_position)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleOrderedByTool);
        gameMsg.Insert("tool_id", _id);
        gameMsg.Insert("tool_position", _tool_position);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 툴이 몬스터에 의해 공격당했을 때, 몬스터의 데미지만큼 hp를 감소시키는 함수
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_monster_power"></param>
    void ToolAttackedByEnemy(int _id, int _monster_power)
    {
        if (FindToolOfID(_id)!= null)
        {
            FindToolOfID(_id).GetComponent<CTool>().Damaged(_monster_power);
        }
    }

    void ToolsAttackSpeedDebuffed(int _id) {
        if (FindToolOfID(_id) != null)
        {
            FindToolOfID(_id).GetComponent<CTool>().AttackSpeedDebuff();
        }
    }

    /// <summary>
    /// 모든 툴을 일시정지 시킴.
    /// </summary>
    void ToolPause() {
        for (int i = 0; i < toolList.Count; i++) {
            toolList[i].GetComponent<CTool>().ChangeStateToPause();
        }
    }
    /// <summary>
    /// 게임을 다시시작했을때 불러지는 함수.
    /// </summary>
    void ResetStage() {
        for (int i = 0; i < toolList.Count; i++)
        {
            toolList[i].transform.position = startPos[i].position;
            toolList[i].GetComponent<CTool>().Reset();
        }
    }
}
