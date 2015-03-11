using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ObjectManager : MonoBehaviour {
    
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

    public List<C4_Object> objectList;
    public Dictionary<ObjectID, C4_Object> objectID2Object;

    void initStartObject()
    {
        
    }
}
