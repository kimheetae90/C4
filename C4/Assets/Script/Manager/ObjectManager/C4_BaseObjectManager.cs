using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_BaseObjectManager : MonoBehaviour {

    [System.NonSerialized]
    private List<C4_Object> objectList;
    [System.NonSerialized]
    private Dictionary<ObjectID, C4_Object> objectDictionary;

    public virtual void Awake()
    {
        objectList = new List<C4_Object>();
        objectDictionary = new Dictionary<ObjectID, C4_Object>();
    }

    public void clearListAndDictionary()
    {
        objectList.Clear();
        objectDictionary.Clear();
    }

    public void addObject(C4_Object inputObject)
    {
        objectList.Add(inputObject);
        objectDictionary.Add(inputObject.objectAttr, inputObject);
    }

    public void removeObject(C4_Object removeObject)
    {
        objectList.Remove(removeObject);
        objectDictionary.Remove(removeObject.objectAttr);
    }

    public C4_Object getObject(ObjectID objectID)
    {
        C4_Object ret = objectDictionary[objectID];

        return ret;
    }

    public C4_Object getObjectInList(int idx)
    {
        C4_Object ret = objectList[idx];

        return ret;
    }

    public int getObjectCount()
    {
        return objectList.Count;
    }
}
