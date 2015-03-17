using UnityEngine;
using System.Collections;

public class C4_Water : C4_Object {

    C4_Object waterObject;

    void Start()
    {
        objectID.id = C4_ObjectManager.Instance.currentObjectCode++;
        objectID.type = ObjectID.Type.Water;
        waterObject = transform.gameObject.GetComponent<C4_Water>();
        C4_ObjectManager.Instance.addObjectToAll(waterObject);
    }

}
