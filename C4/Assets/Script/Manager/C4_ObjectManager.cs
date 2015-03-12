using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ObjectManager : MonoBehaviour, C4_IntInitInstance {
    
    private static C4_ObjectManager _instance;
    public static C4_ObjectManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_ObjectManager)) as C4_ObjectManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_ObjectManager";
                    _instance = container.AddComponent(typeof(C4_ObjectManager)) as C4_ObjectManager;
                }
            }

            return _instance;
        }
    }

    public void initInstance()
    {
        if (!_instance)
        {
            _instance = GameObject.FindObjectOfType(typeof(C4_ObjectManager)) as C4_ObjectManager;
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "C4_ObjectManager";
                _instance = container.AddComponent(typeof(C4_ObjectManager)) as C4_ObjectManager;
            }
        }
    }

    public Queue<C4_Object> removeReservedObjectList;
    public Dictionary<ObjectID, C4_Object> objectList;
    
    public int currentObjectCode;
    Queue<int> deletedObjectCode;
    
    void Start()
    {
        currentObjectCode = 0;
    }
}
