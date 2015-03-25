using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ObjectManager : C4_BaseObjectManager
{
    [System.NonSerialized]
    private Queue<C4_Object> removeReservedObjectQueue;

    [System.NonSerialized]
    private int currentObjectCode;

    [System.NonSerialized]
    private Queue<int> deletedObjectCode;

    [System.NonSerialized]
    private Dictionary<GameObjectType, C4_BaseObjectManager> objectManagerList;

    C4_BaseObjectManager objectManager;
    C4_Object removeReservedObject;
    public override void Awake()
    {
        base.Awake();
        removeReservedObjectQueue = new Queue<C4_Object>();
        objectManagerList = new Dictionary<GameObjectType, C4_BaseObjectManager>();
        currentObjectCode = 0;
        C4_PlayerObjectManager playerObjectManager = GameObject.Find("PlayerObjectManager").GetComponent<C4_PlayerObjectManager>();
        C4_EnemyObjectManager enemyObjectManager = GameObject.Find("EnemyObjectManager").GetComponent<C4_EnemyObjectManager>();
        objectManagerList.Add(GameObjectType.Player, playerObjectManager);
        objectManagerList.Add(GameObjectType.Enemy, enemyObjectManager);
    }

    void LateUpdate()
    {
        clearRemoveReservedObjectQueue();
    }

    void clearRemoveReservedObjectQueue()
    {
        while (removeReservedObjectQueue.Count > 0)
        {
            removeReservedObject = removeReservedObjectQueue.Dequeue();
            Destroy(removeReservedObject.gameObject);
        }
    }

    public void registerObjectToAll(ref C4_Object inputObject,GameObjectType type)
    {
        inputObject.objectID.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        inputObject.objectID.type = GameObjectType.Player;

        addObject(inputObject);
        if (objectManagerList.ContainsKey(inputObject.objectID.type))
        {
            objectManagerList.TryGetValue(inputObject.objectID.type, out objectManager);
            objectManager.addObject(inputObject);
        }
    }
    
    public void reserveRemoveObject(C4_Object _removeObject)
    {
        removeReservedObjectQueue.Enqueue(_removeObject);

        if (objectManagerList.TryGetValue(_removeObject.objectID.type, out objectManager))
        {
            objectManager.removeObject(_removeObject);
            removeObject(_removeObject);
        }
    }

    public C4_BaseObjectManager getSubObjectManager(GameObjectType type)
    {
        objectManagerList.TryGetValue(type, out objectManager);
        return objectManager;
    }
}
