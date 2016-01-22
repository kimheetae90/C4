using UnityEngine;
using System.Collections;

public class CPlayer : BaseObject
{
    public int hp;
    public float moveSpeed;
    public GameObject player;
    public float m_moveSpeed;
    bool isMoveable;
    int m_hp;

    public bool isAlive;
    public bool canHold;
    public bool readyToBomb;

    public Material material;
    public Texture[] texture = new Texture [3];

    CPlayerAnimation playerAnimation;
    CMove moveScript;

    void Awake()
    {
        playerAnimation = GetComponent<CPlayerAnimation>();
        moveScript = GetComponent<CMove>();
    }

    void Start() {
        Reset();
    }

    void Update() {
        UpdateState();
    }

    protected override void UpdateState()
    {
        if (isAlive&&moveScript.isMove==false) {

            if (canHold)
            {
                ChangeState(ObjectState.Play_Player_Ready);
            }
            else
            {
                ChangeState(ObjectState.Play_Player_Idle_With_Tool);
            }
        }
    
    }

    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;
        switch (objectState)
        {
            case ObjectState.Play_Player_Reset:
                break;
            case ObjectState.Play_Player_Pause:
                PlayerStop();
                break;
            case ObjectState.Play_Player_Ready:
                PlayerReady();
                break;
            case ObjectState.Play_Player_Move:
                PlayerMove();
                break;
            case ObjectState.Play_Player_Idle_With_Tool:
                PlayerIdleWithTool();
                break;
            case ObjectState.Play_Player_Move_Front_Wiht_Tool:
                PlayerMoveFrontWithTool();
                break;
            case ObjectState.Play_Player_Move_Back_With_Tool:
                PlayerMoveBackWithTool();
                break;
            case ObjectState.Play_Player_Die:
                PlayerDie();
                break;
        }
    }

    /// <summary>
    /// 플레이어의 상태가 Ready로 바뀌면 불러지는 함수.
    /// </summary>
    void PlayerReady() {
        playerAnimation.Reset();
        playerAnimation.Idle();
    }
    /// <summary>
    /// 플레이어의 상태가 Move로 바뀌면 불러지는 함수.
    /// </summary>
    void PlayerMove() {
        playerAnimation.Reset();
        playerAnimation.Move();
    }

    /// <summary>
    /// 플레어의 상태가 IdleWithTool로 바뀌면 불러지는 함수.
    /// </summary>
    void PlayerIdleWithTool() {
        playerAnimation.Reset();
        playerAnimation.IdleWithTool();
    }

    /// <summary>
    /// 플레어의 상태가 MoveFrontWithTool로 바뀌면 불러지는 함수.
    /// </summary>
    void PlayerMoveFrontWithTool() {
        playerAnimation.Reset();
        playerAnimation.MoveFrontWithTool();
    }

    /// <summary>
    /// 플레이어의 상태가 MoveBackWithTool로 바뀌면 불러지는 함수.
    /// </summary>
    void PlayerMoveBackWithTool() {
        playerAnimation.Reset();
        playerAnimation.MoveBackWithTool();
    }

    /// <summary>
    /// 플레이어가 피격당했을 때 불러지는 함수.
    /// </summary>
    void Ouch() {

        switch (objectState) { 
        
            case ObjectState.Play_Player_Ready:
                playerAnimation.IdleOuch();
                break;
            case ObjectState.Play_Player_Move:
                playerAnimation.MoveOuch();
                break;
            case ObjectState.Play_Player_Idle_With_Tool:
                playerAnimation.IdleWithToolOuch();
                break;
            case ObjectState.Play_Player_Move_Front_Wiht_Tool:
                playerAnimation.MoveFrontWithToolOuch();
                break;
            case ObjectState.Play_Player_Move_Back_With_Tool:
                playerAnimation.MoveBackWithToolOuch();
                break;
        }
    
    }

    /// <summary>
    /// 플레이어의 상태가 die가 되면 불러지는 함수.
    /// </summary>
    void PlayerDie() {
        playerAnimation.Reset();
        playerAnimation.Die();
    }

    /// <summary>
    /// 외부에서 사용해서 플레이어의 상태를 idle로 바꿔주는 함수
    /// </summary>
    public void ChangeStateToIdle() {
        ChangeState(ObjectState.Play_Player_Ready);
    }

    /// <summary>
    /// 외부에서 사용해서 플레이어의 상태를 Move로 바꿔주는 함수.
    /// </summary>
    public void ChangeStateToMove() {
        ChangeState(ObjectState.Play_Player_Move);
    }

    /// <summary>
    /// 외부에서 사용해서 플레이어의 상태를 IdleWIthTool로 바꿔주는 함수.
    /// </summary>
    public void ChangeStateToIdleWithTool() {
        ChangeState(ObjectState.Play_Player_Idle_With_Tool);
    }

    /// <summary>
    /// 외부에서 사용해서 플레이어의 상태를 MoveFrontWithTool로 바꿔주는 함수.
    /// </summary>
    public void ChangeStateToMoveFrontWithTool() {
        ChangeState(ObjectState.Play_Player_Move_Front_Wiht_Tool);
    }
    /// <summary>
    /// 외부에서 사용해서 플레이어의 상태를 MoveBackWithTool로 바꿔주는 함수.
    /// </summary>
    public void ChangeStateToMoveBackWithTool() {
        ChangeState(ObjectState.Play_Player_Move_Back_With_Tool);
    }

    /// <summary>
    /// 파라미터로 받은 데미지 값만큼 플레이어의 hp를 감소시켜주는 함수.
    /// hp가 0 이하가 되면 플레이어의 상태를 Play_Player_Die로 변경한다.
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        m_hp -= damage;
        Ouch();
        ChangeTexture();
        if (m_hp <= 0)
        {
            isAlive = false;
            ChangeState(ObjectState.Play_Player_Die);
        }
    }
    /// <summary>
    /// 남은 체력 비례 텍스쳐 변경.
    /// </summary>
    void ChangeTexture()
    {
        if ((float)m_hp / hp <= 0.3f && material.mainTexture != texture[2])
        {
            material.mainTexture = texture[2];
        }
        else if ((float)m_hp / hp <= 0.6f && material.mainTexture == texture[0])
        {

            material.mainTexture = texture[1];
        }
    }
    /// <summary>
    /// 변수들을 초기화.
    /// </summary>
    public void Reset()
    {
        material.mainTexture = texture[0];
        isAlive = true;
        m_hp = hp;
        m_moveSpeed = moveSpeed;
        canHold = true;
        readyToBomb = false;
        ChangeState(ObjectState.Play_Player_Ready);
    }

    /// <summary>
    /// 플레이어가 툴을 잡았을 때 실행시켜주는 함수.
    /// </summary>
    /// <param name="tool"></param>
    public void HoldTool(GameObject tool)
    {
        if (canHold)
        {
            canHold = false;
            ChangeState(ObjectState.Play_Player_Idle_With_Tool);
            transform.localScale = new Vector3(1, 1, 1);

            moveScript.SetMoveSpeed(m_moveSpeed*((float)tool.GetComponent<CTool>().weight/100));
        }
    }

    /// <summary>
    /// 플레이어가 툴을 놓았을 때 실행시켜주는 함수.
    /// </summary>
    /// <param name="tool"></param>
    public void PutDownTool(GameObject tool)
    {
        if (canHold == false) {
            ChangeState(ObjectState.Play_Player_Ready);
            canHold = true;

            m_moveSpeed = moveSpeed;
            moveScript.SetMoveSpeed(m_moveSpeed);

        }
    }
    /// <summary>
    /// 외부에서 사용되어 palyer의 state를 pause로 바꿈.
    /// </summary>
    public void ReadyToPause() {
        ChangeState(ObjectState.Play_Player_Pause);
    }
    /// <summary>
    /// player의 move를 stop시킴.
    /// </summary>
    public void PlayerStop() {
        GetComponent<CMove>().StopMoveToTarget();
    }
    /// <summary>
    /// player의 state를 반환.
    /// </summary>
    /// <returns></returns>
    public ObjectState GetPlayerState()
    {
        return objectState;
    }

}

