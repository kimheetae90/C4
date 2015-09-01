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
    int m_hp;

    public bool isAlive;

    List<BaseObject> listLatestFindObjects;
    GameObject player;
    CMove move;
    CLineHelper lineHelper;

    void Awake()
    {
        isAlive = true;
        listLatestFindObjects = new List<BaseObject>();
		objectState = ObjectState.Play_Tool_CanHeld;
        m_hp = hp;
        move = GetComponent<CMove>();
        lineHelper = GetComponent<CLineHelper>();
    }

    void Update()
    {
        UpdateState();
    }

    protected override void UpdateState()
    {
        switch (objectState)
        {
            case ObjectState.Play_Tool_CanHeld:
            case ObjectState.Play_Tool_CanAttack:
                CheckCanAttack();
                break;
		    case ObjectState.Play_Tool_CanNotHeld:
                transform.position = player.transform.position + new Vector3(1.0f, 0, 0);
                break;
            default:
                break;
        }
    }
    protected override void ChangeState(ObjectState _objectState)
    {
        if (objectState == _objectState)
            return;

        objectState = _objectState;

        switch (_objectState)
        {
		    case ObjectState.Play_Tool_CanAttack:
                StartAttack();
                break;
		    case ObjectState.Play_Tool_CanNotHeld:
		    case ObjectState.Play_Tool_UnAvailable:
                StopAttack();        
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 공격할 수 있는 상황일 때, 2초마다 공격을 명령하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        while (CheckCanAttack())
	    {
	    	GameMessage gameMsg = GameMessage.Create(MessageName.Play_ToolAttackMonster);
            gameMsg.Insert("tool_id", id);
            gameMsg.Insert("tool_position", transform.position);
            SendGameMessage(gameMsg);

		    yield return new WaitForSeconds(2);
	    }
    }

    /// <summary>
    /// 공격 가능하면 Attack 코루틴을 start하는 함수.
    /// </summary>
    void StartAttack()
    {
        if (CheckCanAttack())
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
			ChangeState(ObjectState.Play_Tool_CanAttack);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 반경이 radius인 Sphere속에 파라미터로 넘겨준
    /// tag를 가진 게임오브젝트가 있는지에 대한 정보를 bool값으로 리턴하는 함수.
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
        hp -= damage;
        if (hp <= 0)
        {
            isAlive = false;
			ChangeState(ObjectState.Play_Tool_UnAvailable);
        }
    }

    /// <summary>
    /// 툴이 플레이어에게 잡힐 때 호출하는 함수
    /// </summary>
    /// <param name="_player"></param>
    public void HoldByPlayer(GameObject _player)
    {
        switch(objectState)
        {
		case ObjectState.Play_Tool_CanAttack:
		case ObjectState.Play_Tool_CanHeld:
                player = _player;
			ChangeState(ObjectState.Play_Tool_CanNotHeld);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 툴이 플레이어에 의해 놓여질 때 호출되는 함수.
    /// </summary>
    /// <param name="_player"></param>
    public void PutDownByPlayer(GameObject _player)
    {
		if(objectState == ObjectState.Play_Tool_CanNotHeld)
        {
            player = null;
            lineHelper.OrderingYPos(gameObject);
			ChangeState(ObjectState.Play_Tool_CanHeld);
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
}
