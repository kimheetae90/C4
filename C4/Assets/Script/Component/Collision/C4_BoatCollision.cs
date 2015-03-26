using UnityEngine;
using System.Collections;

public class C4_BoatCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        C4_Object collisionObject = other.GetComponent<C4_Object>();
        C4_StraightMove moveScript = GetComponent<C4_StraightMove>();
        C4_Character character = GetComponent<C4_Character>();

        switch(collisionObject.objectAttr.type)
        {
            case GameObjectType.Player:
            case GameObjectType.Enemy:
                moveScript.isMove = false;
                break;
            case GameObjectType.Missile:
                C4_MissileFeature missileFeature = collisionObject.GetComponent<C4_MissileFeature>();
                character.damaged(missileFeature.power);
                break;
        }
    }

}
