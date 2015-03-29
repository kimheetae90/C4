using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ObjectManager : C4_BaseObjectManager
{
    Queue<C4_Object> removeReservedObjectQueue;
    int currentObjectCode;
    Queue<int> deletedObjectCode;
    Dictionary<GameObjectType, C4_BaseObjectManager> objectManagerDictionary;

    public override void Awake()
    {
        base.Awake();
        removeReservedObjectQueue = new Queue<C4_Object>();
        deletedObjectCode = new Queue<int>();
        objectManagerDictionary = new Dictionary<GameObjectType, C4_BaseObjectManager>();
        currentObjectCode = 0;
    }

    void LateUpdate()
    {
        clearRemoveReservedObjectQueue();
    }

    public void addSubObjectManager(GameObjectType _type, C4_BaseObjectManager _subObjectManager)
    {
        objectManagerDictionary.Add(_type, _subObjectManager);
    }

    void clearRemoveReservedObjectQueue()
    {
        while (removeReservedObjectQueue.Count > 0)
        {
            C4_Object removeReservedObject = removeReservedObjectQueue.Dequeue();
            Destroy(removeReservedObject.gameObject);
        }
    }

    public void resetAllObjectData()
    {
        clearListAndDictionary();
        currentObjectCode = 0;
        deletedObjectCode.Clear();
        objectManagerDictionary.Clear();
    }

    public void registerObjectToAll(ref C4_Object inputObject,GameObjectType type, GameObjectInputType inputType)
    {
        inputObject.objectAttr.id = currentObjectCode++;
        inputObject.objectAttr.type = type;
        inputObject.objectAttr.setBits(inputType);

        addObject(inputObject);
        if (objectManagerDictionary.ContainsKey(inputObject.objectAttr.type))
        {
            C4_BaseObjectManager objectManager;
            objectManagerDictionary.TryGetValue(inputObject.objectAttr.type, out objectManager);
            objectManager.addObject(inputObject);
        }
    }
    
    public void reserveRemoveObject(C4_Object _removeObject)
    {
        removeReservedObjectQueue.Enqueue(_removeObject);

        C4_BaseObjectManager objectManager;
        if (objectManagerDictionary.TryGetValue(_removeObject.objectAttr.type, out objectManager))
        {
            objectManager.removeObject(_removeObject);
            removeObject(_removeObject);
        }
    }

    public C4_BaseObjectManager getSubObjectManager(GameObjectType type)
    {
        C4_BaseObjectManager objectManager;
        objectManagerDictionary.TryGetValue(type, out objectManager);
        return objectManager;
    }
}
