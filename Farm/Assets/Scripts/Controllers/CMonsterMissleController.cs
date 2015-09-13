using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CMonsterMissleController : Controller
{

    Dictionary<int, Queue<GameObject>> missleDic;

    Dictionary<int, GameObject> firedMissleDic;

    public int missleAmount;

    void Awake()
    {
        Init();
    }

    protected override void Start()
    {
        base.Start();
        Generate();
    }


    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_MissleAttackPlayer:
                MissleAttackPlayer((int)_gameMessage.Get("missle_power"));
                MissleDisappear((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_id"));
                break;
            case MessageName.Play_MissleAttackTool:
                MissleAttackTool((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("missle_power"));
                MissleDisappear((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_id"));
                break;
            case MessageName.Play_MissleAttackFence:
                MissleAttackFence((int)_gameMessage.Get("fence_id"), (int)_gameMessage.Get("missle_power"));
                MissleDisappear((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_id"));
                break;
            case MessageName.Play_MissleOrderedByMonster:
                MissleOrderedByMonster((int)_gameMessage.Get("monster_id"), (Vector3)_gameMessage.Get("monster_position"));
                break;
            case MessageName.Play_MissleDisappear:
                MissleDisappear((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_id"));
                break;
            case MessageName.Play_StageFailed:
                //MissleStop();
                break;
            case MessageName.Play_StageRestart: ResetStage();
                break;

        }
    }

    /*
    GameObject FindMissleOfID(int _index,int _id)
    {
        return missleList[_index].Find(missle => missle.GetComponent<CMissle>().id == _id) as GameObject;
    }
    */

    void Init()
    {
        missleDic = new Dictionary<int, Queue<GameObject>>();
        firedMissleDic = new Dictionary<int, GameObject>();

        missleAmount = 5;
    }

    /// <summary>
    /// 미사일을 정해진 숫자만큼 미리 만들어놓는 함수.
    /// </summary>
    void Generate()
    {

        CMonsterController monsterController = FindObjectOfType<CMonsterController>();
        for (int i = 0; i < monsterController.monsterList.Count; i++)
        {
            CMonster _monster = monsterController.monsterList[i].GetComponent<CMonster>();
            int _id = _monster.id;

            if (_monster.missleName != MissleName.NonMissle)
            {
                missleDic.Add(_id, new Queue<GameObject>());

                for (int j = 0; j < missleAmount; j++)
                {
                    GameObject _missle = ObjectPooler.Instance.GetGameObject(_monster.missleName.ToString());
                    CMissle _missleScript = _missle.GetComponent<CMissle>();
                    _missleScript.SetController(this);
                    _missleScript.SetOwner(_monster);
                    _missleScript.power = _monster.power;
                    _missle.SetActive(false);
                    missleDic[_id].Enqueue(_missle);
                }
            }
        }

    }


    /// <summary>
    /// 미사일이 monster에 의해 명령 받으면 missleDic에서 빼서 firedMissleDic에 넣고 공격함(앞으로나감).
    /// </summary>
    /// <param name="_tool_id">공격 명령을 내린 tool의 id</param>
    /// <param name="_tool_position">공격명령을 내린 tool의 position</param>
    void MissleOrderedByMonster(int _monster_id, Vector3 _monster_position)
    {

        GameObject _missle = (GameObject)missleDic[_monster_id].Dequeue();
        CMissle _missleScript = _missle.GetComponent<CMissle>();
        _missle.SetActive(true);
        _missle.transform.position = new Vector3(_monster_position.x, _monster_position.y, _monster_position.z);
        _missleScript.startPosX = _monster_position.x;
        _missleScript.ReadyToMove();
        _missle.GetComponent<CMove>().SetTargetPos(new Vector3(transform.position.x - 100, transform.position.y, transform.position.z));
        _missle.GetComponent<CMove>().StartMove();

        firedMissleDic.Add(_missle.GetComponent<CMissle>().id, _missle);
    }

    /// <summary>
    /// 미사일이 몬스터를 맞춤.
    /// </summary>
    /// <param name="_missle_power">미사일의 power</param>
    void MissleAttackPlayer(int _missle_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_PlayerDamagedByMonster);
        gameMsg.Insert("monster_power", _missle_power);
        SendGameMessage(gameMsg);
    }

    void MissleAttackTool(int _tool_id,int _missle_power) 
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolDamagedByMonster);
        gameMsg.Insert("tool_id", _tool_id);
        gameMsg.Insert("monster_power", _missle_power);
        SendGameMessage(gameMsg);
    }
    void MissleAttackFence(int _fence_id, int _missle_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_FenceDamagedByMonster);
        gameMsg.Insert("fence_id", _fence_id);
        gameMsg.Insert("monster_power", _missle_power);
        SendGameMessage(gameMsg);
    }



    /// <summary>
    /// 미사일이 사라짐. firedMissleDic에서 missleDic으로 옮김.
    /// </summary>
    /// <param name="_tool_id">미사일을 쏜 monster의 id</param>
    /// <param name="_id">사라지는 미사일의 id</param>
    void MissleDisappear(int _monster_id, int _id)
    {
        GameObject _missle = firedMissleDic[_id];
        firedMissleDic.Remove(_id);
        _missle.GetComponent<CMove>().isMove = false;
        _missle.SetActive(false);
        missleDic[_monster_id].Enqueue(_missle);
    }
    /// <summary>
    /// 게임이 끝난 경우. 이미 날아간 미사일들을 멈추게 함.
    /// </summary>
    void MissleStop()
    {
        foreach (KeyValuePair<int, GameObject> missle in firedMissleDic)
        {
            missle.Value.GetComponent<CMissle>().ReadyToPause();
        }
    }
    /// <summary>
    /// 게임을 다시 시작한 경우 불러지는 함수.
    /// </summary>
    void ResetStage()
    {
        foreach (KeyValuePair<int, GameObject> missle in firedMissleDic)
        {
            missleDic[missle.Value.GetComponent<CMissle>().monster.GetComponent<CMonster>().id].Enqueue(missle.Value);
        }
        firedMissleDic.Clear();

    }
}
