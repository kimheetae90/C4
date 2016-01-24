using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CMonster : BaseObject
{
    public int hp;
    int _hp;
    public int power;
    public float attackReadyTime;
    public float attackTime;
    public float attackRange;
    public float moveSpeed;
    float m_moveSpeed;
    public int SkillID;//일단 1이면 흑사쥐의 공격속도감소 디버프.

    public ParticleSystem particle;
    
    public int lineNumber;

    
    public float stunTime;
    public float m_stunTime;
    

    

    public MissleName missleName;
    public Transform shotPos;

    public bool isAlive;
    public bool attackable;

    public int touchedFenceID;

    public bool touchedWithPlayer;
    public bool touchedWithTool;
    public bool touchedWithWood;

    public List<Renderer> renderer;
    public Texture[] texture = new Texture[3];

    protected CMonsterAnimation monsterAnimation;
    CMonsterAttackRange monsterAttackRange;
    CMove moveScript;

    void Awake()
    {
        moveScript = GetComponent<CMove>();
        monsterAnimation = GetComponent<CMonsterAnimation>();
        monsterAttackRange = GetComponentInChildren<CMonsterAttackRange>();
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
            case ObjectState.Play_Monster_Blind:
                MonsterBlind();
                break;
            case ObjectState.Play_Monster_Traped:
                MonsterTrapped();
                break;
		    case ObjectState.Play_Monster_Die:
                MonsterDie();
                break;
        }
    }

    protected override void UpdateState()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Play_Farm"))
        {
            AttackFarm();
        }
    }


   public void MonsterSetting(int m_hp, int _power, float _cooldownTime, float _attackSpeed, float _range, float _moveSpeed, int _skillID){

       hp = m_hp;
       _hp = m_hp;
       power = _power;
       attackReadyTime = _cooldownTime;
       attackTime = _attackSpeed;
       attackRange = _range;
       moveSpeed = _moveSpeed;
       m_moveSpeed = _moveSpeed;
       m_stunTime = stunTime;
       SkillID = _skillID;

       moveScript.SetMoveSpeed(_moveSpeed);
       //monsterAttackRange.Setting();
   }
   /// <summary>
   /// Monster의 상태가 Reset상태가 되면 불러져서 변수들과 Collider를 초기화 시켜줌.
   /// </summary>
    void MonsterReset() {

        foreach (Renderer rend in renderer)
        {
            rend.material.mainTexture = texture[0];
        }
        m_moveSpeed = moveSpeed;
        isAlive = true;
        _hp = hp;
        attackable = true;
        GetComponent<Collider>().enabled = true;
        moveScript.SetMoveSpeed(m_moveSpeed);

        monsterAttackRange.GetComponent<Collider>().enabled = true;
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
        moveScript.StartMove();
        monsterAttackRange.GetComponent<Collider>().enabled = true;
        monsterAnimation.Reset();
        monsterAnimation.Walk();
    }

    /// <summary>
    /// Monster가 공격을 하기 전 준비동작을 하는 상태가 되면 불러지는 함수.
    /// </summary>
    void MonsterReadyForAttack() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Ready();
    }

    /// <summary>
    /// Monster의 상태가 Attack이 되면 불러져서 몬스터의 움직임을 멈추고 애니메이션을 Attack으로 바꿈
    /// </summary>
    protected abstract void MonsterAttack();



    /// <summary>
    /// Monster가 맞아서 상태가 Hitted가 되면 불러져서 몬스터의 움직임을 멈추고 애니메이션 Stun을 실행하고
    /// 코루틴 Monster_Stun을 실행함(일정시간(StunTime)이 지난 후 다시 움직이게 함.)
    /// </summary>
    protected virtual void MonsterHitted()
    {
        MonsterMoveStop();
        //monsterAnimation.Reset();
        monsterAnimation.Stun();
        particle.Play();
        StartCoroutine("Monster_Stun");
    }
    /// <summary>
    /// 밤시간이 지나서 Monster가 원래 자리로 Return하는 함수.
    /// </summary>
    void MonsterReturn() {
        touchedFenceID=0;
        touchedWithPlayer = false;
        touchedWithTool = false;

        monsterAttackRange.StopCoroutine("MonsterAttack");
        moveScript.SetMoveSpeed(m_moveSpeed * 3);
        moveScript.StartMove();
        GetComponent<Collider>().enabled = false;
        monsterAttackRange.GetComponent<Collider>().enabled = false;
        monsterAnimation.Reset();
        monsterAnimation.Return();

    }

    void MonsterBlind() {
        monsterAttackRange.StopCoroutine("MonsterAttack");
        attackable = true;
        monsterAttackRange.GetComponent<Collider>().enabled = false;
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Idle();
    }

    void MonsterTrapped() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Idle();
        StartCoroutine("Monster_Trapped");
    }

    /// <summary>
    /// Monster의 Hp가 0이되어 상태가 Die가 되면 불러짐. 몬스터의 움직임을 멈추고 애니메이션 Death를 실행함.
    /// 그리고 MonsterController에게 메세지를 보냄.
    /// </summary>
    void MonsterDie() {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Death();
        GetComponent<Collider>().enabled = false;
        monsterAttackRange.StopCoroutine("MonsterAttack");
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterDied);
        gameMsg.Insert("id", id);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 원거리공격을 하는 몬스터인 경우, 공격 가능한 상태(shootable)이면 미사일을 발사하는 함수.
    /// </summary>
    public void Shoot()
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterShotMissle);
        gameMsg.Insert("monster_id", id);
        gameMsg.Insert("monster_position", shotPos.position);
        SendGameMessage(gameMsg);
    }
   

    /// <summary>
    /// 다른 함수에서 몬스터의 상태를 Reset로 바꾸고 싶을때 사용되는 함수.
    /// </summary>
    public void ChangeStateToReset()
    {
		ChangeState(ObjectState.Play_Monster_Reset);
    }
    public void ChangeStateToReady()
    {
        ChangeState(ObjectState.Play_Monster_Ready);
    }
    public void ChangeStateToMove()
    {
        ChangeState(ObjectState.Play_Monster_Move);
    }
    public void ChangeStateToReadyForAttack()
    {
        ChangeState(ObjectState.Play_Monster_ReadyForAttack);
    }
    public void ChangeStateToAttack()
    {
        ChangeState(ObjectState.Play_Monster_Attack);
    }
    /// <summary>
    /// 다른 함수에서 몬스터의 상태를 Return으로 바꾸고 싶을때 사용되는 함수.
    /// </summary>
    public void ChangeStateToReturn()
    {
        ChangeState(ObjectState.Play_Monster_Return);
    }
    public void ChangeStateToBlind() {
        ChangeState(ObjectState.Play_Monster_Blind);
    }
    public void ChangeStateToPause()
    {
        ChangeState(ObjectState.Play_Monster_Pause);
    }

    /// <summary>
    /// 몬스터의의 objectState를 리턴값으로 얻기 위한 함수.
    /// </summary>
    /// <returns></returns>
    public ObjectState GetMonsterState()
    {
        return objectState;
    }
    

    /// <summary>
    /// 몬스터의 움직임을 멈출 때 사용되는 함수.
    /// </summary>
    public void MonsterMoveStop() {
        moveScript.StopMoveToTarget();
    }
   

    /// <summary>
    /// 몬스터가 미사일에 맞아서 데미지를 입었을 때 사용되는 함수. hp를 _damage만큼 깎고 몬스터의 상태를 Hitted로 바꿈.
    /// 만약 몬스터의 hp가 0이 되면 몬스터의 상태를 Die로 바꿈.
    /// </summary>
    /// <param name="_damage">몬스터 Controller에서 받은 메세지에 첨부되어있는 미사일의 Power</param>
    public void Damaged(int _damage)
    {
        _hp -= _damage;
        ChangeTexture();

        if (_hp <= 0 && isAlive == true)
        {
            isAlive = false;
            ChangeState(ObjectState.Play_Monster_Die);
        }

        if (objectState != ObjectState.Play_Monster_Blind && objectState != ObjectState.Play_Monster_Traped)
        {

            ChangeState(ObjectState.Play_Monster_Hitted);
        }
        else {

            monsterAnimation.Stun();
        }
        
    }

    public void Trapped(int _damage, float _stuntime) {

        m_stunTime = _stuntime;
        _hp -= _damage;
        ChangeTexture();
        ChangeState(ObjectState.Play_Monster_Traped);
        if (_hp <= 0 && isAlive == true)
        {
            isAlive = false;
            ChangeState(ObjectState.Play_Monster_Die);
        }
        m_stunTime = stunTime;
    }



    /// <summary>
    /// 남은 체력 비례 텍스쳐 변경.
    /// </summary>
    void ChangeTexture()
    {
        if ((float)_hp / hp <= 0.3f)
        {
            if (renderer.Count > 0 && renderer[0].material.mainTexture != texture[2])
            {
                foreach (Renderer rend in renderer)
                {
                    rend.material.mainTexture = texture[2];
                }
            }
        }
        else if ((float)_hp / hp <= 0.6f)
        {
            if (renderer.Count > 0 && renderer[0].material.mainTexture == texture[0])
            {
                foreach (Renderer rend in renderer)
                {
                    rend.material.mainTexture = texture[1];
                }
            }
        }

    }

    /// <summary>
    /// 공격할 수 있는 상황인지 판단하여 bool값을 리턴하는 함수.
    /// </summary>
    /// <returns></returns>
    public bool CheckCanAttack()
    {
        if (attackable && isAlive)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 플레이어나 툴이나 펜스에 닿아있는 상태인지 체크하는 함수.
    /// </summary>
    /// <returns></returns>
    public bool CheckTouched()
    {
        if (touchedWithPlayer || touchedWithTool || touchedFenceID !=0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 몬스터의 상태가 Hitted일때 호출되는 코루틴.
    /// 일정시간(stunTime)동안 몬스터를 움직이지 않게 한 후에 몬스터가 아직 살아있으면 상태를 Move로 바꿈.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Monster_Stun()
    {

        yield return new WaitForSeconds(m_stunTime);
        particle.Stop();
        if (_hp > 0)
        {
            if(touchedWithPlayer==false&&touchedWithTool==false&&touchedFenceID==0&&objectState!=ObjectState.Play_Monster_Return&&objectState!=ObjectState.Play_Monster_Blind&&objectState!=ObjectState.Play_Monster_Traped)
            ChangeState(ObjectState.Play_Monster_Move);
        }
    }

    IEnumerator Monster_Trapped() {
        yield return new WaitForSeconds(m_stunTime);
        if (_hp > 0)
        {
            if (touchedWithPlayer == false && touchedWithTool == false && touchedFenceID == 0 && objectState != ObjectState.Play_Monster_Return && objectState != ObjectState.Play_Monster_Blind)
                ChangeState(ObjectState.Play_Monster_Move);
        }
    }


    /// <summary>
    /// 몬스터가 Farm에 닿았을때 호출되는 함수.
    /// MonsterController에게 게임메세지 Play_MonsterAttackFarm을 보낸다.
    /// </summary>
    protected void AttackFarm() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterAttackFarm);
        moveScript.StopMoveToTarget(); 
        gameMsg.Insert("id", id);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 몬스터가 플레이어진영의 오브젝트를 공격함.
    /// </summary>
    /// <param name="baseObject"></param>
    public void AttackPlayersObject(BaseObject baseObject) {
       
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterAttackPlayersObject);
        gameMsg.Insert("object_id", baseObject.id);
        gameMsg.Insert("monster_power", power);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 모든 공격을 멈춤.
    /// </summary>
    public void StopAttack() {
        touchedWithPlayer = false;
        touchedWithTool = false;
        touchedWithWood = false;
        touchedFenceID = 0;
        monsterAttackRange.StopCoroutine("MonsterAttack");

    }
}