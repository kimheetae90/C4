using UnityEngine;
using System.Collections;

public class C4_MissileCollision : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        C4_StraightMove missileMove = GetComponentInParent<C4_StraightMove>();
        switch (collisionObject.objectAttr.type)
        {
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                missileMove.stopMoveToTarget();
                break;
        }
    }

}
