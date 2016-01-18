using UnityEngine;
using System.Collections;

public class CWood : CTerrain {

    public int hp;
    public int _hp;

    protected override void Complete()
    {
        _hp = hp;
    }

    public void Damaged(int _damage)
    {
        _hp -= _damage;

        if (_hp <= 0)
        {
            ChangeState(ObjectState.Play_Terrain_Uncomplete);
        }
    }
}
