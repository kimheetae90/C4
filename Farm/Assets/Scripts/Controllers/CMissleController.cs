using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CMissleController : Controller
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
            case MessageName.Play_MissleAttackMonster:
                MissleAttackMonster((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_power"));
                MissleDisappear((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("missle_id"));
                break;
            case MessageName.Play_MissleOrderedByTool:
                MissleOrderedByTool((int)_gameMessage.Get("tool_id"), (Vector3)_gameMessage.Get("tool_position"));
                break;
            case MessageName.Play_MissleDisappear:
                MissleDisappear((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("missle_id"));
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

        CToolController toolController = FindObjectOfType<CToolController>();
        for (int i = 0; i < toolController.toolList.Count; i++)
        {
            CTool _tool = toolController.toolList[i].GetComponent<CTool>();
            int _id = _tool.id;

            if (_tool.missleName != MissleName.NonMissle)
            {
                missleDic.Add(_id, new Queue<GameObject>());

                for (int j = 0; j < missleAmount; j++)
                {
                    GameObject _missle = ObjectPooler.Instance.GetGameObject(_tool.missleName.ToString());
                    CMissle _missleScript = _missle.GetComponent<CMissle>();
                    _missleScript.SetController(this);
                    _missleScript.SetOwner(_tool);
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
    void MissleOrderedByTool(int _tool_id, Vector3 _tool_position)
    {

        GameObject _missle = (GameObject)missleDic[_tool_id].Dequeue();
        CMissle _missleScript = _missle.GetComponent<CMissle>();
        _missle.SetActive(true);
        _missle.transform.position = new Vector3(_tool_position.x, _tool_position.y, _tool_position.z);
        _missleScript.startPosX = _tool_position.x;
        _missleScript.ReadyToMove();
        _missle.GetComponent<CMove>().SetTargetPos(new Vector3(transform.position.x + 100, transform.position.y, transform.position.z));
        _missle.GetComponent<CMove>().StartMove();

        firedMissleDic.Add(_missle.GetComponent<CMissle>().id, _missle);
    }

    /// <summary>
    /// 미사일이 몬스터를 맞춤.
    /// </summary>
    /// <param name="_monster_id">맞춘 몬스터의 id</param>
    /// <param name="_missle_power">미사일의 power</param>
    void MissleAttackMonster(int _monster_id, int _missle_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterDamagedByMissle);
        gameMsg.Insert("monster_id", _monster_id);
        gameMsg.Insert("missle_power", _missle_power);
        SendGameMessage(gameMsg);
    }
    /// <summary>
    /// 미사일이 사라짐. firedMissleDic에서 missleDic으로 옮김.
    /// </summary>
    /// <param name="_tool_id">미사일을 쏜 tool의 id</param>
    /// <param name="_id">사라지는 미사일의 id</param>
    void MissleDisappear(int _tool_id, int _id)
    {
            GameObject _missle = firedMissleDic[_id];
            firedMissleDic.Remove(_id);
            _missle.GetComponent<CMove>().isMove = false;
            _missle.SetActive(false);
            missleDic[_tool_id].Enqueue(_missle);
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
    /// 게임을 다시 시작한 경우 불러지는 함수. 이미 쏜 미사일들을 다시 불러들임.
    /// </summary>
    void ResetStage()
    {
        foreach (KeyValuePair<int, GameObject> missle in firedMissleDic)
        {
            int _id = missle.Value.GetComponent<CMissle>().id;
            int _tool_id = missle.Value.GetComponent<CMissle>().tool.id;
            GameObject _missle = firedMissleDic[_id];
            _missle.GetComponent<CMove>().isMove = false;
            _missle.SetActive(false);
            missleDic[_tool_id].Enqueue(_missle);
            //missleDic[missle.Value.GetComponent<CMissle>().tool.GetComponent<CTool>().id].Enqueue(missle.Value);
        }
        firedMissleDic.Clear();

    }
}
