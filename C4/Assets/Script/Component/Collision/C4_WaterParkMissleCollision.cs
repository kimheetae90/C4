using UnityEngine;
using System.Collections;

public class C4_WaterParkMissleCollision : C4_MissileCollision
{

    public bool isfirst = true;

    void Start()
    {
        power = GetComponentInParent<C4_MissileFeature>().power;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            return;
        }
        C4_MissileFeature missleFeature = GetComponentInParent<C4_MissileFeature>();
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        switch (collisionObject.objectAttr.type)
        {
            case GameObjectType.Ground:
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                    C4_MissleColliderCollision collider = transform.GetComponentInParent<C4_MissileFeature>().transform.GetChild(1).transform.GetComponent<C4_MissleColliderCollision>();
                    if (collider != null&&isfirst)
                    {
                        collider.GetComponent<C4_MissleColliderCollision>().checkpoint(missleFeature.transform.position);
                        collider.transform.localScale = new Vector3(missleFeature.misslerange, missleFeature.misslerange, missleFeature.misslerange);
                        collider.gameObject.SetActive(true);
                        isfirst = false;
                    }
                break;
        }
    }
}
