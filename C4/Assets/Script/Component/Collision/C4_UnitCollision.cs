using UnityEngine;
using System.Collections;

public class C4_UnitCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        C4_StraightMove moveScript = GetComponentInParent<C4_StraightMove>();
        C4_Unit unit = GetComponentInParent<C4_Unit>();
        switch(collisionObject.objectAttr.type)
        {
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                moveScript.stopMoveToTarget();
                break;
            case GameObjectType.Missile:
                C4_MissileFeature missileFeature = collisionObject.GetComponent<C4_MissileFeature>();
                unit.damaged(missileFeature.power);
                break;
        }
    }

}
