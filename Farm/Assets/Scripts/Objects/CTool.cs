using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CTool : BaseObject
{
    public int damage;
    public float attackRange;
    public int hp;
    public float weight;
    public float attackReadySpeed;
    public float attackSpeed;
    public int troughPower;//관통력
    int m_hp;

    public bool isAlive;
    public bool canHeld;
    public bool shotable;
    public Transform shotPosition;
    public MissleName missleName;

    public List<Renderer> renderer;
    public Texture[] texture = new Texture[3];

    GameObject player;
    CLineHelper lineHelper;
    CToolAnimation toolAnimation;
    CAttackRange attackRangeScript;

    void Awake()
    {
        lineHelper = GetComponent<CLineHelper>();
        toolAnimation = GetComponent<CToolAnimation>();
        if(GetComponentInChildren<CAttackRange>()!=null){
            attackRangeScript = GetComponentInChildren<CAttackRange>();
        }
        
    }
    void Start() {
        Reset();
    }

    void Update()
    {
        UpdateState();
    }

    protected override void UpdateState()
    {
        if (canHeld == false)
        {
            transform.position = player.transform.position + new Vector3(2.0f, 0, 0);
        }

    }
    protected override void ChangeState(ObjectState _objectState)
    {
        if (objectState == _objectState)
            return;

        objectState = _objectState;

        switch (_objectState)
        {
            case ObjectState.Play_Tool_Reset:
                break;
            case ObjectState.Play_Tool_Pause:
                break;
            case ObjectState.Play_Tool_Ready:
                ToolReady();
                break;
            case ObjectState.Play_Tool_Move:
                ToolMove();
                break;
            case ObjectState.Play_Tool_ReadyToShot:
                ToolReadyToShot();
                break;
            case ObjectState.Play_Tool_Shot:
                ToolShot();
                break;
            case ObjectState.Play_Tool_UnAvailable:
                ToolDie();
                break;

            default:
                break;
        }
    }


    /// <summary>
    /// 변수들을 초기화 하는 함수.
    /// </summary>
    public void Reset()
    {
        foreach (Renderer rend in renderer) {
            rend.material.mainTexture = texture[0];
        }

        isAlive = true;
        canHeld = true;
        shotable = true;
        m_hp = hp;
        ChangeState(ObjectState.Play_Tool_Ready);

        if (attackRangeScript != null) {
            attackRangeScript.StopCoroutine("ToolAttack");
        }
        
    }

    void ToolReady() {
        toolAnimation.Reset();
        toolAnimation.Idle();
    }

    void ToolMove() {
        toolAnimation.Reset();
        toolAnimation.Move();

    }

    void ToolReadyToShot() {
        toolAnimation.Reset();
        toolAnimation.Ready();
    
    }

    void ToolShot() {
        toolAnimation.Reset();
        toolAnimation.Shot();
        Shoot();
    }

    void ToolDie() {
        toolAnimation.Reset();
        toolAnimation.Die();
        if (attackRangeScript != null)
        {
            attackRangeScript.StopCoroutine("ToolAttack");
        }
    }

    /// <summary>
    /// 공격 가능한 상태(shootable)이면 미사일을 발사하는 함수.
    /// </summary>
    void Shoot() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolAttackMonster);
        gameMsg.Insert("tool_id", id);
        gameMsg.Insert("tool_position", shotPosition.position);
        SendGameMessage(gameMsg);
    }
    /// <summary>
    /// 공격할 수 있는 상황인지 판단하여 bool값을 리턴하는 함수.
    /// </summary>
    /// <returns></returns>
    public bool CheckCanAttack()
    {
        if (transform.position.x > -18.5f &&shotable && canHeld && isAlive)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 파라미터로 넘겨준 데미지 값 만큼 hp를 깎는 함수. 0 이하가 되면 툴을 사용불가능한 상태로 만듦.
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        m_hp -= damage;
        ChangeTexture();
        if (m_hp <= 0)
        {
            isAlive = false;
			ChangeState(ObjectState.Play_Tool_UnAvailable);
            if (canHeld == false) {
                canHeld = true;
                GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolDiedWhileHelded);
                gameMsg.Insert("tool", this.gameObject);
                SendGameMessageToSceneManage(gameMsg);
            }
        }
    }

    /// <summary>
    /// 남은 체력 비례 텍스쳐 변경.
    /// </summary>
    void ChangeTexture()
    {
        if ((float)m_hp / hp <= 0.3) 
        {
            if (renderer.Count > 0 && renderer[0].material.mainTexture != texture[2])
            {
                foreach (Renderer rend in renderer)
                {
                    rend.material.mainTexture = texture[2];
                }
            }
        }
        else if ((float)m_hp / hp <= 0.6f)
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
    /// 툴이 플레이어에게 잡힐 때 호출하는 함수
    /// </summary>
    /// <param name="_player"></param>
    public void HoldByPlayer(GameObject _player)
    {
        if (canHeld)
        {
            player = _player;
            canHeld = false;
            shotable = false;

            if (attackRangeScript != null)
            {
                attackRangeScript.StopCoroutine("ToolAttack");
            }

            if (isAlive)
            {
                ChangeState(ObjectState.Play_Tool_Ready);
            }
        }
    }

    /// <summary>
    /// 툴이 플레이어에 의해 놓여질 때 호출되는 함수.
    /// </summary>
    /// <param name="_player"></param>
    public void PutDownByPlayer(GameObject _player)
    {
		if(canHeld==false)
        {
            player = null;
            lineHelper.OrderingYPos(gameObject);
            canHeld = true;
            shotable = true;
            if (isAlive)
            {
                ChangeState(ObjectState.Play_Tool_Ready);
            }
            //StartAttack();
        }
    }

    /// <summary>
    /// 툴의 objectState를 리턴값으로 얻기 위한 함수.
    /// </summary>
    /// <returns></returns>
	public ObjectState GetToolState()
    {
        return objectState;
    }
    /// <summary>
    /// 툴의 State를 Pause로 바꿈. 외부함수에서 사용.
    /// </summary>
    public void ChangeStateToPause() {
        ChangeState(ObjectState.Play_Tool_Pause);
    }
    public void ChangeStateToReady() {
        ChangeState(ObjectState.Play_Tool_Ready);
    }
    public void ChangeStateToMove() {
        ChangeState(ObjectState.Play_Tool_Move);
    }
    public void ChangeStateToReadyToShot() {
        ChangeState(ObjectState.Play_Tool_ReadyToShot);
    }
    public void ChangeStateToShot(){
        ChangeState(ObjectState.Play_Tool_Shot);
    }
}
