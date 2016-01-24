using UnityEngine;
using System.Collections;

public class CMouse_Boss : CMonster
{

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }
}
