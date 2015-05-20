using UnityEngine;
using System.Collections;

public class C4_MissileCollision : MonoBehaviour {
    public int power;
    public int randomdamagerange;
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            return;
        }
        C4_MissileFeature missleFeature = GetComponentInParent<C4_MissileFeature>();
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        C4_Move missileMove = GetComponentInParent<C4_Move>();
        switch (collisionObject.objectAttr.type)
        {
            case GameObjectType.Ground: 
                if (missleFeature.type == 4)
                {
                    C4_MissleColliderCollision collider = transform.GetComponentInParent<C4_MissileFeature>().transform.GetChild(1).transform.GetComponent<C4_MissleColliderCollision>();
                    if (collider != null)
                    {
                        collider.transform.localScale = new Vector3(missleFeature.misslerange, missleFeature.misslerange, missleFeature.misslerange);
                        collider.gameObject.SetActive(true);
                    }
                } 
                break;
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                if (missleFeature.type == 4)
                {
                    C4_MissleColliderCollision collider = transform.GetComponentInParent<C4_MissileFeature>().transform.GetChild(1).transform.GetComponent<C4_MissleColliderCollision>();
                    if (collider != null)
                    {
                        collider.transform.localScale = new Vector3(missleFeature.misslerange, missleFeature.misslerange, missleFeature.misslerange);
                        collider.gameObject.SetActive(true);
                    }
                }
                else
                {
                    missileMove.stopMoveToTarget();
                    //수정바람   

                    C4_UnitFeature unit = GetComponentInParent<C4_MissileFeature>().unit.GetComponent<C4_UnitFeature>();
                    if (unit != null)
                    {
                        unit.rageUp(unit.GetComponent<C4_UnitFeature>().rageGageChargeInAttack);

                    }
                }
                
               break;
        }
    }
      */

}
