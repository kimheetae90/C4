using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_Manager : MonoBehaviour {

    [System.NonSerialized]
    public List<C4_Object> objectList;
    [System.NonSerialized]
    public Queue<C4_Object> removeReservedObjectList;
    [System.NonSerialized]
    public Dictionary<ObjectID, C4_Object> objectDictionary;

    [System.NonSerialized]
    public int currentObjectCode;
    [System.NonSerialized]
    public Queue<int> deletedObjectCode;

    void Awake()
    {
        objectList = new List<C4_Object>();
        removeReservedObjectList = new Queue<C4_Object>();
        objectDictionary = new Dictionary<ObjectID, C4_Object>();
        currentObjectCode = 0;
    }

    public void addObject(C4_Object inputObject)
    {
        objectList.Add(inputObject);
        objectDictionary.Add(inputObject.objectID, inputObject);
    }
}
