using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CMonster : BaseObject
{
    public int power;
    public int hp;
    int _hp;
    public int lineNumber;

    public bool isAlive;

    public float attackReadyTime;
    public float attackTime;
    public float stunTime;
    public bool attackable;

    public int touchedFenceID;

    public bool touchedWithPlayer;
    public bool touchedWithTool;

    CMonsterAnimation monsterAnimation;


    void Awake()
    {
        monsterAnimation = GetComponent<CMonsterAnimation>();
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
            case ObjectState.Play_Monster_Reset:
                MonsterReset();
                break;
            case ObjectState.Play_Monster_Pause:
                MonsterPause();
                break;
		    case ObjectState.Play_Monster_Ready:
                MonsterReady();
                break;
		    case ObjectState.Play_Monster_Move:
                MonsterMove();
                break;
            case ObjectState.Play_Monster_ReadyForAttack:
                MonsterReadyForAttack();
                break;
		    case ObjectState.Play_Monster_Attack:
                MonsterAttack();
                break;
		    case ObjectState.Play_Monster_Hitted:
                MonsterHitted();
                break;
		    case ObjectState.Play_Monster_Return:
                MonsterReturn();
                break;
		    case ObjectState.Play_Monster_Die:
                MonsterDie();
                break;
        }
    }

    protected override void UpdateState()
    {
    }

    protected abstract void OnTriggerStay(Collider other);

    protected abstract void OnTriggerExit(Collider other);
    

   /// <summary>
   /// Monster의 상태가 Reset상태가 되면 불러져서 변수들과 Collider를 초기화 시켜줌.
   /// </summary>
    void MonsterReset() {
        isAlive = true;
        _hp = hp;
        attackable = true;
        GetComponent<Collider>().enabled = true;
        touchedWithPlayer = false;
        touchedWithTool = false;
        touchedFenceID = 0;
    }
    /// <summary>
    /// Monster의 상태가 Pause가 되면 불려짐.
    /// </summary>
    void MonsterPause() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Idle();
    }
    /// <summary>
    /// Monster의 상태가 Ready가 되면 불려짐.
    /// </summary>
    void MonsterReady() {
        monsterAnimation.Reset();
        monsterAnimation.Idle();
    }


    /// <summary>
    /// Monster의 상태가 Move상태가 되면 불러져서 목표지점까지 움직이는것을 실행하고 애니메이션을 Walk로 바꿈.
    /// </summary>
    void MonsterMove() {
        transform.GetComponent<CMove>().StartMove();
        monsterAnimation.Reset();
        monsterAnimation.Walk();
    }

    /// <summary>
    /// Monster가 공격을 하기 전 준비동작을 하는 상태가 되면 불러지는 함수.
    /// </summary>
    void MonsterReadyForAttack() {
        monsterAnimation.Reset();
        monsterAnimation.Ready();
    }

    /// <summary>
    /// Monster의 상태가 Attack이 되면 불러져서 몬스터의 움직임을 멈추고 애니메이션을 Attack으로 바꿈
    /// </summary>
    void MonsterAttack() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }

    /// <summary>
    /// Monster가 맞아서 상태가 Hitted가 되면 불러져서 몬스터의 움직임을 멈추고 애니메이션 Stun을 실행하고
    /// 코루틴 Monster_Stun을 실행함(일정시간(StunTime)이 지난 후 다시 움직이게 함.)
    /// </summary>
    void MonsterHitted() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Stun();
        StartCoroutine("Monster_Stun");
    }
    /// <summary>
    /// 밤시간이 지나서 Monster가 원래 자리로 Return하는 함수.
    /// </summary>
    void MonsterReturn() {
        touchedFenceID=0;
        touchedWithPlayer = false;
        touchedWithTool = false;

        StopCoroutine("Attack_Player");
        StopCoroutine("Attack_Tool");
        StopCoroutine("Attack_Fence");
        transform.GetComponent<CMove>().StartMove();
        monsterAnimation.Reset();
        monsterAnimation.Return();

    }

    /// <summary>
    /// Monster의 Hp가 0이되어 상태가 Die가 되면 불러짐. 몬스터의 움직임을 멈추고 애니메이션 Death를 실행함.
    /// 그리고 MonsterController에게 메세지를 보냄.
    /// </summary>
    void MonsterDie() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Death();
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterDied);
        gameMsg.Insert("id", id);
        SendGameMessage(gameMsg);
    }
   

    /// <summary>
    /// 다른 함수에서 몬스터의 상태를 Reset로 바꾸고 싶을때 사용되는 함수.
    /// </summary>
    public void Reset() {
		ChangeState(ObjectState.Play_Monster_Reset);
    }

    /// <summary>
    /// 다른 함수에서 몬스터의 상태를 Move로 바꾸고 싶을때 사용되는 함수.
    /// </summary>
    public void ReadyToMove()
    {
        ChangeState(ObjectState.Play_Monster_Move);
    }

    /// <summary>
    /// 몬스터의 움직임을 멈출 때 사용되는 함수.
    /// </summary>
    public void MonsterMoveStop() {
        transform.GetComponent<CMove>().StopMoveToTarget();
    }
    /// <summary>
    /// 다른 함수에서 몬스터의 상태를 Return으로 바꾸고 싶을때 사용되는 함수.
    /// </summary>
    public void ReadyToReturn() {
        ChangeState(ObjectState.Play_Monster_Return);
    }
    public void ReadyToPause() {
        ChangeState(ObjectState.Play_Monster_Pause);
    }

    /// <summary>
    /// 몬스터가 미사일에 맞아서 데미지를 입었을 때 사용되는 함수. hp를 _damage만큼 깎고 몬스터의 상태를 Hitted로 바꿈.
    /// 만약 몬스터의 hp가 0이 되면 몬스터의 상태를 Die로 바꿈.
    /// </summary>
    /// <param name="_damage">몬스터 Controller에서 받은 메세지에 첨부되어있는 미사일의 Power</param>
    public void Damaged(int _damage)
    {
        _hp -= _damage; 
        ChangeState(ObjectState.Play_Monster_Hitted);
        if (_hp <= 0&&isAlive==true)
        {
            isAlive = false; 
            ChangeState(ObjectState.Play_Monster_Die);
        }
    }

    /// <summary>
    /// 몬스터가 Player에게 닿아있 때, Player가 살아있는 상태고 몬스터가 공격 가능한 상태일때 불러지는 코루틴.
    /// AttackPlayer함수를 호출하고, attackTime(공격속도)만큼 쉰 후에 아직 몬스터의 상태가 Attack이면 반복함.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Attack_Player()
    {
        while (true)
        {
            attackable = false;
            ChangeState(ObjectState.Play_Monster_ReadyForAttack);
            yield return new WaitForSeconds(attackReadyTime);
            if (objectState == ObjectState.Play_Monster_ReadyForAttack)
            {
                ChangeState(ObjectState.Play_Monster_Attack);
            }
            if (touchedWithPlayer)
            {
                AttackPlayer();
            }
            yield return new WaitForSeconds(attackTime);
            attackable = true;
            if (touchedWithPlayer==false)
            {
                if (objectState != ObjectState.Play_Monster_Return && objectState != ObjectState.Play_Monster_Ready)
                {
                    Debug.Log("why");
                    ChangeState(ObjectState.Play_Monster_Move);
                }
                break;
            }
        }
    }

    /// <summary>
    /// 몬스터가 Tool에게 닿아있을 때, Tool이 살아있는 상태고 몬스터가 공격 가능한 상태일때 불러지는 코루틴.
    /// AttackTool함수를 호출하고, attackTime(공격속도)만큼 쉰 후에 아직 몬스터의 상태가 Attack이면 반복함.
    /// </summary>
    /// <param name="_tool">현재 몬스터가 접촉하고있는 Tool을 받음.</param>
    /// <returns></returns>
    protected IEnumerator Attack_Tool(CTool _tool)
    {
        while (true)
        {
            attackable = false;
            ChangeState(ObjectState.Play_Monster_ReadyForAttack);
            yield return new WaitForSeconds(attackReadyTime);
            if (objectState == ObjectState.Play_Monster_ReadyForAttack)
            {
                ChangeState(ObjectState.Play_Monster_Attack);
            }
            if (touchedWithTool)
            {
                AttackTool(_tool);
            }
            yield return new WaitForSeconds(attackTime);
            attackable = true;
			if (touchedWithTool==false)
            {
                if (objectState != ObjectState.Play_Monster_Return && objectState != ObjectState.Play_Monster_Ready)
                {
                    Debug.Log("why");
                    ChangeState(ObjectState.Play_Monster_Move);
                }
                break;
            }
        }
    }

    /// <summary>
    /// 몬스터가 Fecne에게 닿아있을 때, 몬스터가 공격 가능한 상태일때 불러지는 코루틴.
    /// AttackFence함수를 호출하고, attackTime(공격속도)만큼 쉰 후에 아직 몬스터의 상태가 Attack이면 반복함.
    /// </summary>
    /// <param name="_fence">현재 몬스터가 닿아 있는 Fence를 받음.</param>
    /// <returns></returns>
    protected IEnumerator Attack_Fence(CFence _fence)
    {
        while (true)
        {
            attackable = false;
            ChangeState(ObjectState.Play_Monster_ReadyForAttack);
            yield return new WaitForSeconds(attackReadyTime);
            if (objectState == ObjectState.Play_Monster_ReadyForAttack)
            {
                ChangeState(ObjectState.Play_Monster_Attack);
            }
            if (touchedFenceID!=0)
            {
                AttackFence(_fence);
            }
            yield return new WaitForSeconds(attackTime);
            attackable = true;
			if (touchedFenceID==0)
            {
                if (objectState != ObjectState.Play_Monster_Return && objectState != ObjectState.Play_Monster_Ready)
                {
                    Debug.Log(objectState);
                    Debug.Log("why");
                    ChangeState(ObjectState.Play_Monster_Move);
                }
                break;
            }
        }
    }

    /// <summary>
    /// 몬스터의 상태가 Hitted일때 호출되는 코루틴.
    /// 일정시간(stunTime)동안 몬스터를 움직이지 않게 한 후에 몬스터가 아직 살아있으면 상태를 Move로 바꿈.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Monster_Stun()
    {

        yield return new WaitForSeconds(stunTime);
        if (_hp > 0)
        {
            if(touchedWithPlayer==false&&touchedWithTool==false&&touchedFenceID==0&&objectState!=ObjectState.Play_Monster_Return)
            ChangeState(ObjectState.Play_Monster_Move);
        }
    }


    /// <summary>
    /// 몬스터가 Farm에 닿았을때 호출되는 함수.
    /// MonsterController에게 게임메세지 Play_MonsterAttackFarm을 보낸다.
    /// </summary>
    protected void AttackFarm() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterAttackFarm);
        transform.GetComponent<CMove>().StopMoveToTarget(); 
        gameMsg.Insert("id", id);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 몬스터가 Player에게 닿아있는 상태일때 코루틴에서 불려지는 함수.
    /// 몬스터가 공격 가능한 상태일때 MonsterController에게 게임메세지 Play_MonsterAttackPlayer를 보낸다.
    /// </summary>
    protected void AttackPlayer()
    {
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterAttackPlayer);
            gameMsg.Insert("monster_power", power);
            SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 몬스터가 Tool에게 닿아있는 상태일때 코루틴에서 불려지는 함수.
    /// 몬스터가 공격 가능한 상황일때 MonsterController에게 게임메세지 Play_MonsterAttackTool를 보낸다.
    /// </summary>
    /// <param name="_tool">몬스터가 현재 닿아있는 Tool을 받음.</param>
    protected void AttackTool(CTool _tool)
    {
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterAttackTool);
            gameMsg.Insert("tool_id", _tool.id);
            gameMsg.Insert("monster_power", power);
            SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 몬스터가 Fence에게 닿아있는 상태일때 코루틴에서 불려지는 함수.
    /// 몬스터가 공격 가능한 상황일때 MonsterController에게 게임메세지 Play_MonsterAttackFence를 보낸다.
    /// </summary>
    /// <param name="_fence">몬스터가 현재 닿아있는 Fence를 받음.</param>
    void AttackFence(CFence _fence)
    {
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterAttackFence);
            gameMsg.Insert("fence_id", _fence.id);
            gameMsg.Insert("monster_power", power);
            SendGameMessage(gameMsg);
    }
    /// <summary>
    /// 모든 공격을 멈춤.
    /// </summary>
    public void StopAttack() {
        touchedWithPlayer = false;
        touchedWithTool = false;
        touchedFenceID = 0;
        StopCoroutine("Attack_Player");
        StopCoroutine("Attack_Tool");
        StopCoroutine("Attack_Fence");

    }
}