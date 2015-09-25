using UnityEngine;
using System.Collections;

public class CMouse_Boodoo : CMonster {

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        Shoot();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }
}
