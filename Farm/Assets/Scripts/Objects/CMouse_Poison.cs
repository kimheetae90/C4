using UnityEngine;
using System.Collections;

public class CMouse_Poison : CMonster {

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        Shoot();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }
}
