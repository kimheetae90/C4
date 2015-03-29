using UnityEngine;
using System.Collections;

public class C4_BoatCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        C4_StraightMove moveScript = GetComponentInParent<C4_StraightMove>();
        C4_Character character = GetComponentInParent<C4_Character>();
        Debug.Log(collisionObject.objectAttr.type);
        switch(collisionObject.objectAttr.type)
        {
            case GameObjectType.Player:
            case GameObjectType.Enemy:
                moveScript.stopMoveToTarget();
                break;
            case GameObjectType.Missile:
                C4_MissileFeature missileFeature = collisionObject.GetComponent<C4_MissileFeature>();
                character.damaged(missileFeature.power);
                break;
        }
    }

}
