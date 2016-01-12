using UnityEngine;
using System.Collections;

public class CDog_Fighter : CMonster
{

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }
}
