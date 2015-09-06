using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CToolController : Controller
{

    public List<GameObject> toolList;

    public Transform startPos;

    void Awake()
    {
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
            case MessageName.Play_ToolDamagedByMonster:
                ToolAttackedByEnemy((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_ToolAttackMonster:
                ToolAttackEnemy((int)_gameMessage.Get("tool_id"), (Vector3)_gameMessage.Get("tool_position"));
                break;
            case MessageName.Play_StageFailed:
                //ToolPause();
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

        for (int i = 0; i < 3; i++)
        {
            toolList.Add(ObjectPooler.Instance.GetGameObject("Play_Tool_PitchingMachine"));
            toolList[i].GetComponent<CTool>().SetController(this);
            toolList[i].transform.position = new Vector3(startPos.position.x + (i*0.5f), startPos.position.y, startPos.position.z);
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
        FindToolOfID(_id).GetComponent<CTool>().Damaged(_monster_power);
    }

    /// <summary>
    /// 모든 툴을 일시정지 시킴.
    /// </summary>
    void ToolPause() {
        for (int i = 0; i < toolList.Count; i++) {
            toolList[i].GetComponent<CTool>().ReadyToPause();
        }
    }
}
