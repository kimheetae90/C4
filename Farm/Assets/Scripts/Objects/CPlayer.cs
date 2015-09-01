using UnityEngine;
using System.Collections;

public class CPlayer : BaseObject
{
    public int hp;
    public float moveSpeed;
    public GameObject player;
    float m_moveSpeed;
    bool isMoveable;
    int m_hp;

    public bool isAlive;
    

    void Awake()
    {
        isAlive = true;
		ChangeState(ObjectState.Play_Player_CanHold);
        m_hp = hp;
        m_moveSpeed = moveSpeed;
    }

    protected override void UpdateState()
    { }

    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;
    }
    
    /// <summary>
    /// 파라미터로 받은 데미지 값만큼 플레이어의 hp를 감소시켜주는 함수.
    /// hp가 0 이하가 되면 플레이어의 상태를 Play_Player_Die로 변경한다.
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            isAlive = false;
		ChangeState(ObjectState.Play_Player_Die);
    }

    /// <summary>
    /// 플레이어가 툴을 잡았을 때 실행시켜주는 함수.
    /// </summary>
    /// <param name="tool"></param>
    public void HoldTool(GameObject tool)
    {
        switch (objectState)
        {
		case ObjectState.Play_Tool_CanAttack:
		case ObjectState.Play_Player_CanHold:
			ChangeState(ObjectState.Play_Player_CanNotHold);
                break;
		case ObjectState.Play_Player_CanNotHold:
                break;
        }
    }

    /// <summary>
    /// 플레이어가 툴을 놓았을 때 실행시켜주는 함수.
    /// </summary>
    /// <param name="tool"></param>
    public void PutDownTool(GameObject tool)
    {
        switch (objectState)
        {
		case ObjectState.Play_Tool_CanAttack:
		case ObjectState.Play_Player_CanHold:
                break;
		case ObjectState.Play_Player_CanNotHold:
			ChangeState(ObjectState.Play_Player_CanHold);
                break;
        }
    }

}
