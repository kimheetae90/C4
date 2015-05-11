using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class C4_FindObjectInRadiousCollision : MonoBehaviour
{
    List<C4_Object> listLatestFindObjects;


    void Awake()
    {
        listLatestFindObjects = new List<C4_Object>();
    }

    public bool FindObjectsInRadious(float radius, GameObjectType type)
    {
        listLatestFindObjects.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < hitColliders.Length; ++i)
        {

            C4_Object obj = hitColliders[i].transform.gameObject.GetComponentInParent<C4_Object>();

            if (obj != null && obj.isType(type))
            {
                listLatestFindObjects.Add(obj);
            }
        }

        sortObj();
    
        return listLatestFindObjects.Count > 0 ? true : false;
    }

    void sortObj()
    {
        listLatestFindObjects.Sort(delegate(C4_Object t1, C4_Object t2)
                { 
                    return Vector3.Distance(t1.transform.position, transform.position).CompareTo(Vector3.Distance(t2.transform.position, transform.position));
                }
        );
    }

    public List<C4_Object> getLatestFindObjects()
    {
        return listLatestFindObjects;
    }

    public C4_Object getNearestObject()
    {
        if (listLatestFindObjects.Count == 0) return null;

        return listLatestFindObjects[0];
    }
}

