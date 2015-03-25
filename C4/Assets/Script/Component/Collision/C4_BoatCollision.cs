using UnityEngine;
using System.Collections;

public class C4_BoatCollision : MonoBehaviour {

    C4_Object collisionObject;
    C4_StraightMove moveScript;
    C4_Character character;

    void OnTriggerEnter(Collider other)
    {
        collisionObject = other.GetComponent<C4_Object>();
        moveScript = GetComponent<C4_StraightMove>();
        character = GetComponent<C4_Character>();

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
