using UnityEngine;
using System.Collections;

public class CDog_Hell : CMonster{

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }

    protected override void MonsterHitted()
    {
        base.MonsterHitted();
        //분신 생성
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_ShadowCalled);
        gameMsg.Insert("position", transform.position);
        SendGameMessage(gameMsg);
    }
}
