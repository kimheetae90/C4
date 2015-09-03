using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CMonsterController : Controller
{
    List<GameObject> monsterList;

    public int oneWavePerMonster = 12;//한 웨이브당 생성되는 몬스터 마리 수.
    int oneWavePerMonsterCount;

    public Transform startPos;

    public List<Transform> startPosition;

    public float regenTime;

    public int currentIter;

    void Awake()
    {
        Init();
    }

    protected override void Start()
    {
        base.Start();
        Reset();
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_MonsterAttackFarm:
                MonsterAttackFarm((int)_gameMessage.Get("id"));
                break;
            case MessageName.Play_MonsterAttackPlayer:
                MonsterAttackPlayer((int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_MonsterAttackFence:
                MonsterAttackFence((int)_gameMessage.Get("fence_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_MonsterAttackTool:
                MonsterAttackTool((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_MonsterDamagedByMissle:
                MonsterDamagedByMissle((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_power"));
                break;
            case MessageName.Play_FenceDisappear_MonsterMove:
                if (_gameMessage.Get("fence_id") != null)
                {
                    FenceDisappear_MonsterMove((int)_gameMessage.Get("fence_id"));
                }
                break;
            case MessageName.Play_MonsterDied:
                MonsterDied((int)_gameMessage.Get("id"));
                break;
            case MessageName.Play_MaintainOver:
                Reset();
                break;
            case MessageName.Play_MonsterReturn:
                MonsterReturn();
                break;
            case MessageName.Play_StageFailed:
                MonsterPause();
                break;

        }
    }




    ///////////////////////////////////////////////////////////////////////////////
    ////////////////////////          구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// monsterList 를 초기화 시키고
    /// ObjectPooler에서 몬스터의 프리팹을 불러와서 gameobject를 생성해서 startposition에 위치시키고 list에 추가함. 
    /// </summary>
    void Init()
    {
        monsterList = new List<GameObject>();

        for (int i = 0; i < oneWavePerMonster; i++)
        {
            monsterList.Add(ObjectPooler.Instance.GetGameObject("Mouse"));
            monsterList[i].GetComponent<CMonster>().SetController(this);
            monsterList[i].SetActive(false);
            monsterList[i].transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
        }

        oneWavePerMonsterCount = 0;
        currentIter = 0;
    }
    /// <summary>
    /// monsterList에서 Iterator로 사용되는 currentIter와, 
    /// 한 웨이브마다 몇마리의 몬스터가 죽었는지 체크하는 oneWavePerMonsterCount를 0으로 초기화
    /// 그리고 GenMonster 코루틴을 호출.
    /// </summary>
    void Reset()
    {
        currentIter = 0;
        oneWavePerMonsterCount = 0;
        StartCoroutine("GenMonster");
    }
    /// <summary>
    /// regenTime에 맞춰 한마리씩 몬스터를 4개의 라인중 하나의 StartPostion에 위치시키고 Farm을 향해서 움직이게 만드는 코루틴.
    /// 정해진 갯수만큼(oneWavePerMonster) 활성화 시키면 코루틴을 멈춘다.
    /// </summary>
    /// <returns></returns>
    IEnumerator GenMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenTime);

            int rand = Random.Range(0,4);

            switch (rand) {

                case 0: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 0;
                    break;
                case 1: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 1;
                    break;
                case 2: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 2;
                    break;
                case 3: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 3;
                    break;
            }

            int _lineNum = monsterList[currentIter].GetComponent<CMonster>().lineNumber;
            Vector3 targetPosition = new Vector3(startPosition[_lineNum].position.x-100, startPosition[_lineNum].position.y, startPosition[_lineNum].position.z);
            monsterList[currentIter].SetActive(true);
            monsterList[currentIter].GetComponent<CMonster>().Reset();
            monsterList[currentIter].transform.position = startPosition[_lineNum].position;
            monsterList[currentIter].GetComponent<CMove>().SetTargetPos(targetPosition);
            monsterList[currentIter].GetComponent<CMonster>().ReadyToMove();
            currentIter++;
            if (currentIter >= oneWavePerMonster)
            {
                break;
            }
        }
    }
    /// <summary>
    /// 몬스터의 id로 monsterList안에있는 해당 몬스터의 gameobject를 찾는 함수.
    /// </summary>
    /// <param name="_id">찾을 몬스터의 id</param>
    /// <returns></returns>
    GameObject FindMonsterOfID(int _id)
    {
        return monsterList.Find(enemy => enemy.GetComponent<CMonster>().id == _id) as GameObject;
    }

    /// <summary>
    /// Play_MonsterAttackFarm 메세지를 받았을때 호출되는 함수. 몬스터가 Farm을 공격함.
    /// PlayManager에게 게임메세지 Play_StageFailed를 보낸다.
    /// 그리고 MonsterDied를 호출해 해당 몬스터를 죽인다.
    /// </summary>
    /// <param name="_id"></param>
    void MonsterAttackFarm(int _id)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_StageFailed);
        SendGameMessage(gameMsg);
        MonsterDied(_id);
    }

    /// <summary>
    /// Play_MonsterAttackPlayer 메세지를 받았을 때 호출되는 함수. 몬스터가 플레이어를 공격함.
    /// PlayManager를 통해 PlayerController에게 게임메세지 Play_PlayerDamagedByMonster를 보내서 
    /// Player에게 _monster_power만큼의 데미지를 입힘.
    /// </summary>
    /// <param name="_monster_power">플레이어를 공격한 몬스터의 power</param>
    void MonsterAttackPlayer(int _monster_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_PlayerDamagedByMonster);
        gameMsg.Insert("monster_power", _monster_power);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// Play_MonsterAttackFence 메세지를 받았을 때 호출되는 함수. 몬스터가 울타리를 공격함.
    /// PlayManager를 통해 FenceController에게 게임메세지 Play_FenceDamagedByFence를 보내서 
    /// Fence에게 _monster_power만큼의 데미지를 입힘.
    /// </summary>
    /// <param name="_fence_id">공격당한 Fence의 id</param>
    /// <param name="_monster_power">공격한 Monster의 Power</param>
    void MonsterAttackFence(int _fence_id, int _monster_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_FenceDamagedByFence);
        gameMsg.Insert("fence_id", _fence_id);
        gameMsg.Insert("monster_power", _monster_power);
        SendGameMessage(gameMsg);
    }
    /// <summary>
    /// Play_MonsterAttackTool 메세지를 받았을 때 호출되는 함수. 몬스터가 Tool을 공격함.
    /// PlayManager를 통해 ToolController에게 게임메세지 Play_ToolDamagedByMonster를 보내서 
    /// Tool에게 _monster_power만큼의 데미지를 입힘.
    /// </summary>
    /// <param name="_tool_id">공격당한 Tool의 id</param>
    /// <param name="_monster_power">공격한 Monster의 Power</param>
    void MonsterAttackTool(int _tool_id, int _monster_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolDamagedByMonster);
        gameMsg.Insert("tool_id", _tool_id);
        gameMsg.Insert("monster_power", _monster_power);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// Fence가 사라져서 해당 Fence를 공격하고 있던 Monster의 상태를 Attack에서 Move로 바꿔줘서 움직이게 하는 함수.
    /// monterList의 모든 monster들 중에서, 현재 살아있고, 닿아있는 울타리의 아이디가 _fenceid인 monster의 상태를 Move로 바꿈.
    /// </summary>
    /// <param name="_fenceid">HP가 0이되서 사라진 Fence의 id</param>
    void FenceDisappear_MonsterMove(int _fenceid) {
        for (int i = 0; i < oneWavePerMonster; i++)
        {
            if (monsterList[i].activeInHierarchy && monsterList[i].GetComponent<CMonster>().touchedFenceID == _fenceid)
                {
                    monsterList[i].GetComponent<CMonster>().ReadyToMove();
                    monsterList[i].GetComponent<CMonster>().touchedFenceID = 0;
                }
        }
    
    }

    /// <summary>
    /// 몬스터가 Missle에 의해 데미지를 입었을때 호출되는 함수.
    /// _id로 맞은 몬스터를 찾아서 _missle_power만큼의 데미지를 입힌다.
    /// </summary>
    /// <param name="_id">미사일에 맞은 monster의 id</param>
    /// <param name="_missle_power">monster를 맞춘 미사일의 power</param>
    void MonsterDamagedByMissle(int _id, int _missle_power)
    {
        FindMonsterOfID(_id).GetComponent<CMonster>().Damaged(_missle_power);
    }

    /// <summary>
    /// 몬스터가 죽었을 때 호출되는 함수.
    /// 해당 몬스터의 Collider를 없애고 (몬스터가 죽고나서 시체가 사라지기 전까지 미사일이 투과해야함)
    /// 이번 웨이브에 죽은 몬스터의 숫자(oneWavePerMonsterCount)를 카운트 한다.
    /// MonsterDie코루틴을 실행시킴. (몬스터가 죽고나서 일정시간 후에 시체가 사라짐.)
    /// 만약 이번 웨이브에서 모든 몬스터가 죽으면 SendOverMessage 코루틴을 실행.
    /// </summary>
    /// <param name="_id">죽은 monster의 id</param>
    void MonsterDied(int _id)
    {
        FindMonsterOfID(_id).GetComponent<Collider>().enabled = false;
        oneWavePerMonsterCount++;
        StartCoroutine(MonsterDie(_id));
        if (oneWavePerMonsterCount >= oneWavePerMonster)
        {
            StartCoroutine("SendOverMessage");
        }
    }

    /// <summary>
    /// 한 웨이브의 제한시간(밤시간)이 다 지나고나면, GenMonster 코루틴을 멈춰서 몬스터의 등장을 멈추고,
    /// 모든 몬스터 중 아직 살아있는 몬스터들을 다시 복귀시킨다.
    /// Collider를 꺼줘서 복귀시키는 도중 아무것과도 부딛히지 않게 한다.
    /// </summary>
    void MonsterReturn() {
        StopCoroutine("GenMonster");
        for (int i = 0; i < oneWavePerMonster; i++)
        {
            int _lineNum = monsterList[i].GetComponent<CMonster>().lineNumber;
            Vector3 returnPosition = new Vector3(startPosition[_lineNum].position.x+10, startPosition[_lineNum].position.y, startPosition[_lineNum].position.z);
            monsterList[i].GetComponent<Collider>().enabled = false;
            monsterList[i].GetComponent<CMove>().SetTargetPos(returnPosition);
            if (monsterList[i].activeInHierarchy)
            {
                monsterList[i].GetComponent<CMonster>().ReadyToReturn();
            }
            
        }
    }

    void MonsterPause() {
        StopCoroutine("GenMonster");
        for (int i = 0; i < oneWavePerMonster; i++)
        {
            if (monsterList[i].activeInHierarchy)
            {
                monsterList[i].GetComponent<CMonster>().ReadyToPause();
            }

        }
    }
    /// <summary>
    /// 몬스터가 죽으면 실행시켜서 해당 몬스터의 시체를 일정시간 후에 사라지게 함.
    /// </summary>
    /// <param name="_id">죽은 monster의 id</param>
    /// <returns></returns>
    IEnumerator MonsterDie(int _id) {
        yield return new WaitForSeconds(3f);
        FindMonsterOfID(_id).SetActive(false);
    }

    /// <summary>
    /// 모든 몬스터가 죽으면 몬스터들의 위치를 startPosition으로 옮기고
    /// PlayManager에게 게임메세지 Play_OneWaveOver 를 보내서 한 웨이브의 끝남을 알린다.
    /// </summary>
    /// <returns></returns>
    IEnumerator SendOverMessage()
    {
        
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < oneWavePerMonster; i++)
        {
            monsterList[i].transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);

        }
        
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_OneWaveOver);
        SendGameMessage(gameMsg);
    }
}