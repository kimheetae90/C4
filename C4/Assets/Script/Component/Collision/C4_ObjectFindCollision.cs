using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_FindObjectInRadiousCollision : MonoBehaviour
{
    public List<C4_Object> FindTargetObject(float radius, GameObjectType type)
    {
        List<C4_Object> list = new List<C4_Object>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < hitColliders.Length; ++i)
        {
            C4_Object obj = hitColliders[i].transform.parent.gameObject.GetComponent<C4_Object>();

            if(obj.isType(type))
            {
                list.Add(obj);
            }
        }

        return list;
    }
}
