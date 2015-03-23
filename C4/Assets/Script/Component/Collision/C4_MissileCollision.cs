using UnityEngine;
using System.Collections;

public class C4_MissileCollision : MonoBehaviour {

    C4_Object collisionObject;
    C4_MissileMove missileMove;

    void OnTriggerEnter(Collider other)
    {
        collisionObject = other.GetComponent<C4_Object>();
        missileMove = GetComponent<C4_MissileMove>();
        switch (collisionObject.objectAttr.type)
        {
            case GameObjectType.Player:
            case GameObjectType.Enemy:
                missileMove.isMove = false;
                break;
        }
    }

}
