using UnityEngine;
using System.Collections;

public class CFence : BaseObject {

    public int hp;
    int m_hp;
    
    void Awake()
    {
        ChangeState(ObjectState.Play_Fence_Alive);
        m_hp = hp;
    }

    protected override void UpdateState()
    {}

    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;

        switch (_objectState)
        {
		case ObjectState.Play_Fence_Alive:
                break;
		case ObjectState.Play_Fence_Died:
                
                break;
        }
    }

    /// <summary>
    /// 파라미터로 받은 데미지 값만큼 울타리의 hp를 감소시켜주는 함수.
    /// hp가 0 이하가 되면 울타리가 파괴됨.
    /// </summary>
    /// <param name="damage"></param>
    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    /// <summary>
    /// 울타리를 파괴하는 메시지를 FenceController로 전달하는 함수.
    /// </summary>
    public void Die(){
    	ChangeState(ObjectState.Play_Fence_Died);
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_FenceDie);
        gameMsg.Insert("fence_id", id);
        SendGameMessage(gameMsg);
    }

    /// <summary>
    /// 리턴값으로 울타리의 상태를 리턴하는 함수
    /// </summary>
    /// <returns></returns>
	public ObjectState GetFenceState()
    {
        return objectState;
    }
}