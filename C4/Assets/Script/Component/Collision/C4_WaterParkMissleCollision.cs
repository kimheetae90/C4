using UnityEngine;
using System.Collections;

public class C4_WaterParkMissleCollision : MonoBehaviour {

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
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                    C4_MissleColliderCollision collider = transform.GetComponentInParent<C4_MissileFeature>().transform.GetChild(1).transform.GetComponent<C4_MissleColliderCollision>();
                    if (collider != null)
                    {
                        Debug.Log(collider);
                        collider.GetComponent<C4_MissleColliderCollision>().checkpoint(missleFeature.transform.position);
                        collider.transform.localScale = new Vector3(missleFeature.misslerange, missleFeature.misslerange, missleFeature.misslerange);
                        collider.gameObject.SetActive(true);
                    }
                break;
        }
    }
}
