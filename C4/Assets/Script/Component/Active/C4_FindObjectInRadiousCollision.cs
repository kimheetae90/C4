using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class C4_FindObjectInRadiousCollision : MonoBehaviour
{
    List<C4_Object> latestFindObject;

    void Start()
    {
        latestFindObject = new List<C4_Object>();
    }

    public bool FindObjectsInRadious(float radius, GameObjectType type)
    {
        latestFindObject.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < hitColliders.Length; ++i)
        {
            C4_Object obj = hitColliders[i].transform.parent.gameObject.GetComponent<C4_Object>();

            if (obj.isType(type))
            {
                latestFindObject.Add(obj);
            }
        }

        sortObj();
    
        return latestFindObject.Count > 0 ? true : false;
    }

    void sortObj()
    {
        latestFindObject.Sort(delegate(C4_Object t1, C4_Object t2)
                { 
                    return Vector3.Distance(t1.transform.position, transform.position).CompareTo(Vector3.Distance(t2.transform.position, transform.position));
                }
        );
    }

    public List<C4_Object> getLatestFindObjects()
    {
        return latestFindObject;
    }

    public C4_Object getNearestObject()
    {
        if (latestFindObject.Count == 0) return null;

        return latestFindObject[0];
    }
}

