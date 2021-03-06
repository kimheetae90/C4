﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CMonsterController : Controller
{
    public List<GameObject> monsterList;
    public List<MonsterName> monsterName;
    public int oneWavePerMonster = 12;//한 웨이브당 생성되는 몬스터 마리 수.
    int oneWavePerMonsterCount;//한 웨이브에 죽은 몬스터 마리 수.

    int stageMode = 0;

    public Transform startPos;

    public List<Transform> startPosition;

    public float regenTime;

    public int currentIter;

    float time;
    int waveCount;

    List<StageInfo> stageInfo;

    public List<GameObject> dog_shadowList;
    public int activatedShadow;
    public int maxShadow;

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
            case MessageName.Play_MonsterAttackFarm:
                MonsterAttackFarm((int)_gameMessage.Get("id"));
                break;
            case MessageName.Play_MonsterAttackPlayersObject:
                MonsterAttackPlayersObject((int)_gameMessage.Get("object_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_MonsterShotMissle:
                MonsterShotMissle((int)_gameMessage.Get("monster_id"), (Vector3)_gameMessage.Get("monster_position"));
                break;
            case MessageName.Play_MonsterDamaged:
                MonsterDamagedByMissle((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("power"));
                break;
            case MessageName.Play_MonsterTrapped:
                MonsterTrapped((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("power"), (float)_gameMessage.Get("stunTime"));
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
                Reset((int)_gameMessage.Get("wavecount"));
                break;
            case MessageName.Play_MonsterReturn:
                MonsterReturn();
                break;
            case MessageName.Play_MonsterBlinded:
                MonsterBlind();
                break;
            case MessageName.Play_MonsterBlindOver:
                MonsterBlindOver();
                break;
            case MessageName.Play_StageFailed:
                //MonsterPause();
                break;
            case MessageName.Play_StageRestart:
                ResetStage();
                break;

            case MessageName.Play_ShadowCalled:
                CallShadow((Vector3)_gameMessage.Get("position"));
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
        time = 0;
        stageInfo = ((List<StageInfo>)GameMaster.Instance.tempData.Get("StageInfo")).ToList<StageInfo>();

        for (int i = stageInfo.Count-1; i >= 0; i--) {
            StageInfo info = stageInfo[i];
            if (info.wave == 0) {
                stageInfo.Remove(info);
            }
        }
        
        stageInfo.Sort((info1, info2) => info1.time.CompareTo(info2.time));

        //stageInfo.Sort((info1, info2) => info1.wave.CompareTo(info2.wave));
         
        
         
        monsterList = new List<GameObject>();
        foreach (StageInfo node in stageInfo) {
            
            //monsterList.Add(ObjectPooler.Instance.GetGameObject(node.id.ToString()));
            if (node.wave != 0)
            {
                GameObject monsteri = ObjectPooler.Instance.GetGameObject(((MonsterName)node.id).ToString());

                monsteri.GetComponent<CMonster>().SetController(this);

                monsteri.SetActive(false);
                monsteri.transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
                MonsterInfo mInfo = DataLoadHelper.Instance.GetMonsterInfo(node.id);
                monsteri.GetComponent<CMonster>().MonsterSetting(mInfo.hp, mInfo.power, mInfo.cooldownTime, mInfo.attackSpeed, mInfo.range, mInfo.moveSpeed, mInfo.skillID);

                monsterList.Add(monsteri);
                if (node.id == 22310 && maxShadow == 0)
                {
                    dog_shadowList = new List<GameObject>();
                    activatedShadow = 0;
                    maxShadow = 10;
                    for (int i = 0; i < maxShadow; i++)
                    {
                        GameObject shadow = ObjectPooler.Instance.GetGameObject("Play_Dog_Shadow");
                        shadow.GetComponent<CMonster>().SetController(this);
                        shadow.transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
                        MonsterInfo Info = DataLoadHelper.Instance.GetMonsterInfo(22311);
                        shadow.GetComponent<CMonster>().MonsterSetting(Info.hp, Info.power, Info.cooldownTime, Info.attackSpeed, Info.range, Info.moveSpeed, Info.skillID);
                        shadow.SetActive(false);
                        dog_shadowList.Add(shadow);

                    }
                }
            }
       }
        //oneWavePerMonster = monsterName.Count;

        
        
        //////////////////////////////////////////////////////////
        /*
        for (int i = 0; i < oneWavePerMonster; i++)
        {
            monsterList.Add(ObjectPooler.Instance.GetGameObject(monsterName[i].ToString()));
            CMonster monsteri = monsterList[i].GetComponent<CMonster>();
            monsteri.SetController(this);
            monsterList[i].SetActive(false);
            monsterList[i].transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
        }
        
        oneWavePerMonsterCount = 0;
        currentIter = 0;
        */
    }
    /// <summary>
    /// monsterList에서 Iterator로 사용되는 currentIter와, 
    /// 한 웨이브마다 몇마리의 몬스터가 죽었는지 체크하는 oneWavePerMonsterCount를 0으로 초기화/
    /// 그리고 GenMonster 코루틴을 호출.
    /// </summary>
    void Reset(int wave)
    {
        waveCount = wave;
        int monnum = 0;
        foreach (StageInfo node in stageInfo)
        {
            if (node.wave == waveCount)
            {
                monnum++;
            }
        }

        currentIter = 0;
        oneWavePerMonsterCount = 0;
        oneWavePerMonster = monnum;
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
            //yield return new WaitForSeconds(regenTime);
            yield return null;
            time += Time.deltaTime;
            /*
            int rand = Random.Range(0,4);

            switch (rand) {

                case 0: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 1;
                    break;
                case 1: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 2;
                    break;
                case 2: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 3;
                    break;
                case 3: monsterList[currentIter].GetComponent<CMonster>().lineNumber = 4;
                    break;
            }

            int _lineNum = monsterList[currentIter].GetComponent<CMonster>().lineNumber-1;
            Vector3 targetPosition = new Vector3(startPosition[_lineNum].position.x-100, startPosition[_lineNum].position.y, startPosition[_lineNum].position.z);
            monsterList[currentIter].SetActive(true);
            monsterList[currentIter].transform.position = startPosition[_lineNum].position;
            monsterList[currentIter].GetComponent<CMonster>().ChangeStateToReset();
            monsterList[currentIter].GetComponent<CMove>().SetTargetPos(targetPosition);
            monsterList[currentIter].GetComponent<CMonster>().ChangeStateToMove();
            currentIter++;

            if (currentIter >= oneWavePerMonster)
            {
                break;
            }

             */ 
            /////////////////////////////////////////////////////////////////
            
            if (stageInfo[currentIter].time <= time)
            {
                if (stageInfo[currentIter].wave == waveCount)
                {
                    monsterList[currentIter].GetComponent<CMonster>().lineNumber = stageInfo[currentIter].line;
                    int _lineNum = monsterList[currentIter].GetComponent<CMonster>().lineNumber-1;
                    Vector3 targetPos = new Vector3(startPosition[_lineNum].position.x - 100, startPosition[_lineNum].position.y, startPosition[_lineNum].position.z);
                    monsterList[currentIter].SetActive(true);
                    monsterList[currentIter].transform.position = startPosition[_lineNum].position;
                    monsterList[currentIter].GetComponent<CMonster>().ChangeStateToReset();
                    monsterList[currentIter].GetComponent<CMove>().SetTargetPos(targetPos);
                    monsterList[currentIter].GetComponent<CMonster>().ChangeStateToMove();
                }
                currentIter++;
            }

            if (currentIter >= stageInfo.Count) {
                time = 0;
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
        
        if (monsterList.Find(enemy => enemy.GetComponent<CMonster>().id == _id) as GameObject == null)
        {
            return dog_shadowList.Find(enemy => enemy.GetComponent<CMonster>().id == _id) as GameObject;
        }
        else
        {
            return monsterList.Find(enemy => enemy.GetComponent<CMonster>().id == _id) as GameObject;
        }
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
    /// Play_MonsterAttackPlayersObject 메세지를 받았을때 호출. 몬스터가 플레이어 진영의 오브젝트를 공격함.
    /// PlayManager를 통해 PlayerController에게 게임메세지 Play_PlayersObjectDamagedByMonster를 보내서 
    /// object에게 _monster_power만큼의 데미지를 입힘.
    /// </summary>
    /// <param name="_object_id">맞은 object</param>
    /// <param name="_monster_power">때린 몬스터의 power</param>
    void MonsterAttackPlayersObject(int _object_id, int _monster_power)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_PlayersObjectDamagedByMonster);
        gameMsg.Insert("object_id", _object_id);
        gameMsg.Insert("monster_power", _monster_power);
        SendGameMessage(gameMsg);
    }
    /*
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
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_FenceDamagedByMonster);
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
    */
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
                    monsterList[i].GetComponent<CMonster>().ChangeStateToMove();
                    monsterList[i].GetComponent<CMonster>().touchedFenceID = 0;
                }
        }
    
    }
    /// <summary>
    /// 몬스터가 미사일을 발사해서 공격함.
    /// </summary>
    /// <param name="_id">미사일을 발사한 몬스터의 id</param>
    /// <param name="_monsterPos">발사한지점. shotPos</param>
    void MonsterShotMissle(int _id, Vector3 _monsterPos) {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleOrderedByMonster);
        gameMsg.Insert("monster_id", _id);
        gameMsg.Insert("monster_position", _monsterPos);
        SendGameMessage(gameMsg);
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

    void MonsterTrapped(int _id, int _power, float _stunTime) {
        FindMonsterOfID(_id).GetComponent<CMonster>().Trapped(_power,_stunTime);
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
        if (FindMonsterOfID(_id).GetComponent<CDog_Shadow>() == null)
        {
            oneWavePerMonsterCount++;
        }
        else {
            activatedShadow--;
        }
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
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i].GetComponent<CMonster>().isAlive)
            {
            int _lineNum = monsterList[i].GetComponent<CMonster>().lineNumber-1;
            Vector3 returnPosition = new Vector3(startPosition[_lineNum].position.x+10, startPosition[_lineNum].position.y, startPosition[_lineNum].position.z);
            //monsterList[i].GetComponent<Collider>().enabled = false;
            monsterList[i].GetComponent<CMove>().SetTargetPos(returnPosition);
            
            monsterList[i].GetComponent<CMonster>().ChangeStateToReturn();
            }
            
        }
    }
    /// <summary>
    /// 스킬 플래시가 사용되면 살아잇는 몬스터들을 전부 블라인드 상태로 만듬.
    /// </summary>
    void MonsterBlind() {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i].GetComponent<CMonster>().isAlive)
            {
                monsterList[i].GetComponent<CMonster>().ChangeStateToBlind();
            }

        }
    }
    /// <summary>
    /// 플래시 지속시간이 끝나면 살아잇는 몬스터들을 움직이게 함.
    /// </summary>
    void MonsterBlindOver() {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i].GetComponent<CMonster>().isAlive)
            {
                monsterList[i].GetComponent<CMonster>().ChangeStateToMove();
            }

        }
    }

    void MonsterPause() {
        StopCoroutine("GenMonster");
        for (int i = 0; i < oneWavePerMonster; i++)
        {
            if (monsterList[i].activeInHierarchy)
            {
                monsterList[i].GetComponent<CMonster>().ChangeStateToPause();
            }

        }
    }

    void CallShadow(Vector3 pos) {
        if (activatedShadow < maxShadow)
        {
            foreach (GameObject shadow in dog_shadowList)
            {
                if (shadow.activeInHierarchy == false)
                {
                    shadow.transform.position = pos;

                    Vector3 targetPos = new Vector3(pos.x - 100, pos.y, pos.z);
                    shadow.SetActive(true);
                    shadow.GetComponent<CMonster>().ChangeStateToReset();
                    shadow.GetComponent<CMove>().SetTargetPos(targetPos);
                    shadow.GetComponent<CMonster>().ChangeStateToMove();

                    shadow.GetComponent<Collider>().enabled = true;
                    activatedShadow++;
                    break;
                }
            }
        }
            /*
        else
        {
             
            GameObject shadow = ObjectPooler.Instance.GetGameObject("Play_Dog_Shadow");
            shadow.GetComponent<CMonster>().SetController(this);
            shadow.transform.position = pos;

            Vector3 targetPos = new Vector3(pos.x - 100, pos.y, pos.z);
            shadow.SetActive(true);
            shadow.GetComponent<CMonster>().ChangeStateToReset();
            shadow.GetComponent<CMove>().SetTargetPos(targetPos);
            shadow.GetComponent<CMonster>().ChangeStateToMove();

            shadow.GetComponent<Collider>().enabled = true;
            activatedShadow++;
            maxShadow++;

        }*/
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

    /// <summary>
    /// 게임을 다시시작하면 불러지는 함수.
    /// </summary>
    void ResetStage() {
        StopCoroutine("GenMonster");
        oneWavePerMonsterCount = 0;
        currentIter = 0;
        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].GetComponent<CMonster>().MonsterMoveStop();
            monsterList[i].GetComponent<CMonster>().StopAttack();
            monsterList[i].transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
            monsterList[i].GetComponent<CMove>().SetTargetPos(startPos.position);

            //monsterList[i].GetComponent<CMonster>().Reset();
        }
        //Reset();
        
    }
}