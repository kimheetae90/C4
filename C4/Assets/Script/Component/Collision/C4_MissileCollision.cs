using UnityEngine;
using System.Collections;

public class C4_MissileCollision : MonoBehaviour {

    C4_Object collisionObject;
    C4_StraightMove missileMove;

    void OnTriggerEnter(Collider other)
    {
        collisionObject = other.GetComponentInParent<C4_Object>();
        missileMove = GetComponentInParent<C4_StraightMove>();
        switch (collisionObject.objectAttr.type)
        {
            case GameObjectType.Player:
            case GameObjectType.Enemy:
                missileMove.stopMoveToTarget();
                break;
        }
    }

}
