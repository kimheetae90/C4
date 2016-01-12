using UnityEngine;
using System.Collections;

public class CMouse_Elite : CMonster{

    protected override void MonsterAttack()
    {
        MonsterMoveStop();
        monsterAnimation.Reset();
        monsterAnimation.Attack();
    }

    protected override void MonsterHitted()
    {
    }
}
