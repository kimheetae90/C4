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
    int m_hp;

    public bool isAlive;
    public bool canHeld;
    public bool shotable;


    List<BaseObject> listLatestFindObjects;
    GameObject player;
    CLineHelper lineHelper;
    CToolAnimation toolAnimation;

    void Awake()
    {
        listLatestFindObjects = new List<BaseObject>();
        lineHelper = GetComponent<CLineHelper>();
        toolAnimation = GetComponent<CToolAnimation>();
        
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
                StopAttack();
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
        isAlive = true;
        canHeld = true;
        shotable = true;
        m_hp = hp;
        ChangeState(ObjectState.Play_Tool_Ready);
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
        StopAttack();
    }

    /// <summary>
    /// 공격할 수 있는 상황일 때, 2초마다 공격을 명령하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);
            if (CheckCanAttack() && canHeld)
            {
                ChangeState(ObjectState.Play_Tool_ReadyToShot);
                yield return new WaitForSeconds(attackReadySpeed);
                if (objectState == ObjectState.Play_Tool_ReadyToShot)
                {
                    ChangeState(ObjectState.Play_Tool_Shot);
                }
            }
                ChangeState(ObjectState.Play_Tool_Ready);
            
            if (isAlive==false) {
                break;
            }
            
	    }
    }
    /// <summary>
    /// 공격 가능한 상태(shootable)이면 미사일을 발사하는 함수.
    /// </summary>
    void Shoot() {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolAttackMonster);
        gameMsg.Insert("tool_id", id);
        gameMsg.Insert("tool_position", transform.position);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 공격 가능한 상태가 되면 Attack 코루틴을 start하는 함수.
    /// </summary>
    void StartAttack()
    {
        StartCoroutine("Attack");
    }

    /// <summary>
    /// Attack 코루틴을 stop하는 함수
    /// </summary>
    void StopAttack()
    {
        StopCoroutine("Attack");
    }

    /// <summary>
    /// 공격할 수 있는 상황인지 판단하여 bool값을 리턴하는 함수.
    /// </summary>
    /// <returns></returns>
    bool CheckCanAttack()
    {
        if (transform.position.x > -18.5f && FindObjectsInRadious(attackRange, "Play_Monster"))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 반경이 radius인 Sphere속에 파라미터로 넘겨준
    /// tag를 가진 게임오브젝트가 있는지에 대한 정보를 bool값으로 리턴하는 함수.
    /// 같은 라인에 있는지까지 판단함.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public bool FindObjectsInRadious(float radius, String tag)
    {
        listLatestFindObjects.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            BaseObject obj = hitColliders[i].transform.gameObject.GetComponentInParent<BaseObject>();

            if (obj != null && obj.tag.Equals(tag))
            {
                if(obj.GetComponent<CMonster>().lineNumber==lineHelper.lineNum)
                listLatestFindObjects.Add(obj);
            }
        }

        sortObj();

        return listLatestFindObjects.Count > 0 ? true : false;
    }

    /// <summary>
    /// List에 있는 오브젝트들을 가까운 거리 순으로 정렬해 주는 함수
    /// </summary>
    void sortObj()
    {
        listLatestFindObjects.Sort(delegate(BaseObject t1, BaseObject t2)
        {
            return Vector3.Distance(t1.transform.position, transform.position).CompareTo(Vector3.Distance(t2.transform.position, transform.position));
        }
        );
    }

    /// <summary>
    /// 파라미터로 넘겨준 데미지 값 만큼 hp를 깎는 함수. 0 이하가 되면 툴을 사용불가능한 상태로 만듦.
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        m_hp -= damage;
        if (m_hp <= 0)
        {
            isAlive = false;
			ChangeState(ObjectState.Play_Tool_UnAvailable);
            if (canHeld == false) {
                canHeld = true;
                GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolDiedWhileHelded);
                gameMsg.Insert("tool", this.gameObject);
                SendGameMessageToSceneManage(gameMsg);
                //player.GetComponent<CPlayer>().PutDownTool(this.gameObject);
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
            Debug.Log("held");
            player = _player;
            canHeld = false;

            ChangeState(ObjectState.Play_Tool_Ready);
            StopAttack();
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

            ChangeState(ObjectState.Play_Tool_Ready);
            StartAttack();
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
    public void ReadyToPause() {
        ChangeState(ObjectState.Play_Tool_Pause);
    }
    public void ReadyToMove() {
        ChangeState(ObjectState.Play_Tool_Move);
    }
}
