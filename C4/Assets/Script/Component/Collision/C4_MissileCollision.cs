using UnityEngine;
using System.Collections;

public class C4_MissileCollision : MonoBehaviour {
    
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
                GetComponentInParent<C4_MissileFeature>().unit.transform.GetChild(1).GetComponent<C4_RageUI>().rageUpAtt();;
                break;
        }
    }

}
