using UnityEngine;
using System.Collections;

public class CMissle : BaseObject
{
    public int power;
    public CTool tool;
    
    

    void Awake()
    {
		ChangeState(ObjectState.Play_Missle_Ready);
    }

    void Update()
    {
        UpdateState();
        
    }


    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;

        switch (objectState)
        {
		case ObjectState.Play_Missle_Ready:
                GetComponent<CMove>().isMove = false;
                break;
		case ObjectState.Play_Missle_Move:
                break;
        }
    }

    protected override void UpdateState()
    {
        if (transform.position.x > 10)
        {
			ChangeState(ObjectState.Play_Missle_Ready);
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleDisappear);
            gameMsg.Insert("missle_id", id);
            gameMsg.Insert("tool_id", tool.id);
            SendGameMessage(gameMsg);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Play_Monster"))
        {
            AttackMonster(other.GetComponent<CMonster>());
        }
    }

    /// <summary>
    /// 미사일이 몬스터에 닿으면 호출되어 몬스터를 공격하는 함수.
    /// PlayManager를 통해 MonsterController에게 게임메세지 Play_MissleAttackMonster를 보낸다.
    /// </summary>
    /// <param name="_monster">공격한 monster</param>
    void AttackMonster(CMonster _monster) {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleAttackMonster);
        gameMsg.Insert("monster_id", _monster.GetComponent<CMonster>().id);
        gameMsg.Insert("missle_power", power);
        gameMsg.Insert("missle_id", id);
        gameMsg.Insert("tool_id", tool.id);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 미사일의 상태를 move로 바꿔줌.
    /// </summary>
    public void ReadyToMove()
    {
		ChangeState(ObjectState.Play_Missle_Move);
    }

    /// <summary>
    /// _tool을 미사일의 주인으로 함.
    /// </summary>
    /// <param name="_tool">미사일의 주인인 tool</param>
    public void SetOwner(CTool _tool)
    {
        tool = _tool;
    }

}
