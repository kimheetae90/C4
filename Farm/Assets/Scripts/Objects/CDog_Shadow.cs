using UnityEngine;
using System.Collections;

public class CDog_Shadow : CMonster{

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }
}
