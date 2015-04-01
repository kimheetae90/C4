using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ObjectManager : C4_BaseObjectManager
{
    Queue<C4_Object> QueRemoveReservedObject;
    int currentObjectCode;
    Queue<int> deletedObjectCode;
    Dictionary<GameObjectType, C4_BaseObjectManager> DicObjectManager;

    public override void Awake()
    {
        base.Awake();
        QueRemoveReservedObject = new Queue<C4_Object>();
        deletedObjectCode = new Queue<int>();
        DicObjectManager = new Dictionary<GameObjectType, C4_BaseObjectManager>();
        currentObjectCode = 0;
    }

    void LateUpdate()
    {
        clearRemoveReservedObjectQueue();
    }

    public void addSubObjectManager(GameObjectType _type, C4_BaseObjectManager _subObjectManager)
    {
        DicObjectManager.Add(_type, _subObjectManager);
    }

    void clearRemoveReservedObjectQueue()
    {
        while (QueRemoveReservedObject.Count > 0)
        {
            C4_Object removeReservedObject = QueRemoveReservedObject.Dequeue();
            Destroy(removeReservedObject.gameObject);
        }
    }

    public void resetAllObjectData()
    {
        clearListAndDictionary();
        currentObjectCode = 0;
        deletedObjectCode.Clear();
        DicObjectManager.Clear();
    }

    public void registerObjectToAll(ref C4_Object inputObject,GameObjectType type, GameObjectInputType inputType)
    {
        inputObject.objectAttr.id = currentObjectCode++;
        inputObject.objectAttr.type = type;
        inputObject.objectAttr.setBits(inputType);

        addObject(inputObject);
        if (DicObjectManager.ContainsKey(inputObject.objectAttr.type))
        {
            C4_BaseObjectManager objectManager;
            DicObjectManager.TryGetValue(inputObject.objectAttr.type, out objectManager);
            objectManager.addObject(inputObject);
        }
    }
    
    public void reserveRemoveObject(C4_Object _removeObject)
    {
        QueRemoveReservedObject.Enqueue(_removeObject);

        C4_BaseObjectManager objectManager;
        if (DicObjectManager.TryGetValue(_removeObject.objectAttr.type, out objectManager))
        {
            objectManager.removeObject(_removeObject);
            removeObject(_removeObject);
        }
    }

    public C4_BaseObjectManager getSubObjectManager(GameObjectType type)
    {
        C4_BaseObjectManager objectManager;
        DicObjectManager.TryGetValue(type, out objectManager);
        return objectManager;
    }
}
