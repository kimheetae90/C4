using UnityEngine;
using System.Collections;

public class C4_Water : C4_Object {

    C4_Object waterObject;

    void Start()
    {
        objectAttr.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        objectAttr.type = GameObjectType.Water;
        waterObject = transform.gameObject.GetComponent<C4_Water>();
        C4_ManagerMaster.Instance.objectManager.addObjectToAll(waterObject);
    }

}
