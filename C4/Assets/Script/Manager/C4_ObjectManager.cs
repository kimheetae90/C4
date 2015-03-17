using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ObjectManager : C4_BaseObjectManager, C4_IntInitInstance
{
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

    [System.NonSerialized]
    public Queue<C4_Object> removeReservedObjectQueue;

    [System.NonSerialized]
    public int currentObjectCode;

    [System.NonSerialized]
    public Queue<int> deletedObjectCode;

    [System.NonSerialized]
    public Dictionary<ObjectID.Type, C4_BaseObjectManager> objectManagerList;

    C4_BaseObjectManager objectManager;
    C4_Object removeReservedObject;
    void Awake()
    {
        base.Awake();
        removeReservedObjectQueue = new Queue<C4_Object>();
        objectManagerList = new Dictionary<ObjectID.Type, C4_BaseObjectManager>();
        currentObjectCode = 0;
        C4_PlayerObjectManager.Instance.initInstance();
        C4_EnemyObjectManager.Instance.initInstance();
        objectManagerList.Add(ObjectID.Type.Player, C4_PlayerObjectManager.Instance);
        objectManagerList.Add(ObjectID.Type.Enemy, C4_EnemyObjectManager.Instance);
    }

    void LateUpdate()
    {
        clearRemoveReservedObjectQueue();
    }

    public void addObjectToAll(C4_Object inputObject)
    {
        addObject(inputObject);
        if (objectManagerList.ContainsKey(inputObject.objectID.type))
        {
            objectManagerList.TryGetValue(inputObject.objectID.type, out objectManager);
            objectManager.addObject(inputObject);
        }
    }

    void clearRemoveReservedObjectQueue()
    {
        if (removeReservedObjectQueue.Count > 0)
        {
            removeReservedObject = removeReservedObjectQueue.Dequeue();
            objectManagerList.TryGetValue(removeReservedObject.objectID.type, out objectManager);
            objectManager.removeObject(removeReservedObject);
            removeObject(removeReservedObject);
        }
    }

    public void reserveRemoveObject(C4_Object removeObject)
    {
        removeReservedObjectQueue.Enqueue(removeObject);
    }

    public C4_BaseObjectManager getSubObjectManager(ObjectID.Type type)
    {
        objectManagerList.TryGetValue(type, out objectManager);
        return objectManager;
    }
}
