using UnityEngine;
using System.Collections;

public class C4_Water : C4_Object {

    void Start()
    {
        objectID.id = C4_ObjectManager.Instance.currentObjectCode++;
        objectID.type = ObjectID.Type.Water;
    }

}
