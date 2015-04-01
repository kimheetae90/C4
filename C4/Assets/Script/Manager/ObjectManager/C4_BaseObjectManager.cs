using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_BaseObjectManager : MonoBehaviour {

    [System.NonSerialized]
    private List<C4_Object> ListObject;
    [System.NonSerialized]
    private Dictionary<ObjectID, C4_Object> DicObject;

    public virtual void Awake()
    {
        ListObject = new List<C4_Object>();
        DicObject = new Dictionary<ObjectID, C4_Object>();
    }

    public void clearListAndDictionary()
    {
        ListObject.Clear();
        DicObject.Clear();
    }

    public void addObject(C4_Object inputObject)
    {
        ListObject.Add(inputObject);
        DicObject.Add(inputObject.objectAttr, inputObject);
    }

    public void removeObject(C4_Object removeObject)
    {
        ListObject.Remove(removeObject);
        DicObject.Remove(removeObject.objectAttr);
    }

    public C4_Object getObject(ObjectID objectID)
    {
        C4_Object ret = DicObject[objectID];

        return ret;
    }

    public C4_Object getObjectInList(int idx)
    {
        C4_Object ret = ListObject[idx];

        return ret;
    }

    public int getObjectCount()
    {
        return ListObject.Count;
    }
}
