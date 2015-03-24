using UnityEngine;
using System.Collections;

public class C4_Water : C4_Object {

    C4_Object waterObject;

    void Start()
    {
        objectID.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        objectID.type = GameObjectType.Ground;
        waterObject = transform.gameObject.GetComponent<C4_Water>();
        C4_ManagerMaster.Instance.objectManager.addObjectToAll(waterObject);
    }

}
