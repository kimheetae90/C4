using UnityEngine;
using System.Collections;

public class C4_StraightMissleCollision : C4_MissileCollision
{
    void Start()
    {
        power = GetComponentInParent<C4_MissileFeature>().power;
        randomdamagerange = GetComponentInParent<C4_MissileFeature>().randomdamagerange;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            return;
        }
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        C4_Move missileMove = GetComponentInParent<C4_Move>();
        switch (collisionObject.objectAttr.type)
        {
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                    missileMove.stopMoveToTarget();
                    //수정바람   

                    C4_UnitFeature unit = GetComponentInParent<C4_MissileFeature>().unit.GetComponent<C4_UnitFeature>();
                    if (unit != null)
                    {
                        unit.rageUp(unit.GetComponent<C4_UnitFeature>().rageGageChargeInAttack);

                    }
               break;
        }
    

    }

}
