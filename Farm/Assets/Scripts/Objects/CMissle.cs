using UnityEngine;
using System.Collections;

public class CMissle : BaseObject
{
    public int power;
    public CTool tool;
    public CMonster monster;
    public float attackRange;
    public float startPosX;
    
    

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
            case ObjectState.Play_Missle_Reset:
                break;
            case ObjectState.Play_Missle_Pause:
                MisslePause();
                break;
		    case ObjectState.Play_Missle_Ready:
                GetComponent<CMove>().isMove = false;
                break;
		    case ObjectState.Play_Missle_Move:
                break;
        }
    }

    protected override void UpdateState()
    {
        if (tool != null)
        {
            if (transform.position.x > startPosX + attackRange)
            {
                ChangeState(ObjectState.Play_Missle_Ready);
                GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleDisappear);
                gameMsg.Insert("missle_id", id);
                gameMsg.Insert("tool_id", tool.id);
                SendGameMessage(gameMsg);
            }
        }
        else if (monster != null) {
            if (transform.position.x < startPosX - attackRange)
            {
                ChangeState(ObjectState.Play_Missle_Ready);
                GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleDisappear);
                gameMsg.Insert("missle_id", id);
                gameMsg.Insert("monster_id", monster.id);
                SendGameMessage(gameMsg);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (tool != null)
        {
            if (other.CompareTag("Play_Monster") && other.GetComponent<CMonster>().isAlive)
            {
                AttackMonster(other.GetComponent<CMonster>());
            }
        }
        if (monster != null) {
            if (other.CompareTag("Play_Player") && other.GetComponent<CPlayer>().isAlive)
            {
                Debug.Log("missleattackplayer");
                AttackPlayer();
            }
            else if (other.CompareTag("Play_Tool") && other.GetComponent<CTool>().isAlive)
            {
                Debug.Log("missleattacktool");
                AttackTool(other.GetComponent<CTool>());
            }
            else if (other.CompareTag("Play_Fence"))
            {
                AttackFence(other.GetComponent<CFence>());
            }
        
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
    /// 몬스터가 발사한 미사일이 플레이어에게 닿으면 호출되어 플레이어를 공격.
    /// </summary>
    void AttackPlayer() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleAttackPlayer);
        gameMsg.Insert("missle_power", power);
        gameMsg.Insert("missle_id", id);
        gameMsg.Insert("monster_id", monster.id);
        SendGameMessage(gameMsg);
    }
    /// <summary>
    /// 몬스터가 발사한 미사일이 툴에게 닿으면 호출되어 툴을 공격
    /// </summary>
    /// <param name="_tool">미사일을 맞은 툴</param>
    void AttackTool(CTool _tool)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleAttackTool);
        gameMsg.Insert("tool_id", _tool.GetComponent<CTool>().id);
        gameMsg.Insert("missle_power", power);
        gameMsg.Insert("missle_id", id);
        gameMsg.Insert("monster_id", monster.id);
        SendGameMessage(gameMsg);
    }
    /// <summary>
    /// 몬스터가 발사한 미사일이 펜스에게 닿으면 호출되어 펜스를 공격
    /// </summary>
    /// <param name="_fence">미사일을 맞은 펜스.</param>
    void AttackFence(CFence _fence)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MissleAttackFence);
        gameMsg.Insert("fence_id", _fence.GetComponent<CFence>().id);
        gameMsg.Insert("missle_power", power);
        gameMsg.Insert("missle_id", id);
        gameMsg.Insert("monster_id", monster.id);
        SendGameMessage(gameMsg);
    }

    void MisslePause() {
        GetComponent<CMove>().StopMoveToTarget();
    }

    /// <summary>
    /// 미사일의 상태를 move로 바꿔줌.
    /// </summary>
    public void ReadyToMove()
    {
		ChangeState(ObjectState.Play_Missle_Move);
    }
    /// <summary>
    /// 미사일의 상태를 Pause로 바꿈. 외부에서 사용.
    /// </summary>
    public void ReadyToPause()
    {
        ChangeState(ObjectState.Play_Missle_Pause);
    }

    /// <summary>
    /// _tool을 미사일의 주인으로 함.
    /// </summary>
    /// <param name="_tool">미사일의 주인인 tool</param>
    public void SetOwner(BaseObject _owner)
    {
        if (_owner.GetComponent<CMonster>() != null) {
            monster = _owner.GetComponent<CMonster>();
            attackRange = monster.attackRange;
        }
        else if (_owner.GetComponent<CTool>() != null)
        {
            tool = _owner.GetComponent<CTool>();
            attackRange = tool.attackRange;
        }
    }
    

}
