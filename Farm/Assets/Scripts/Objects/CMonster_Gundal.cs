using UnityEngine;
using System.Collections;

public class CMonster_Gundal : CMonster {

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }
}
