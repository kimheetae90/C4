using UnityEngine;
using System.Collections;

public class C4_Water : C4_Object {
    
    void Start()
    {
        objectAttr.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        objectAttr.type = GameObjectType.Ground;
        C4_ManagerMaster.Instance.objectManager.addObjectToAll(waterObject);
    }

}
