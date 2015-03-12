using UnityEngine;
using System.Collections;

public class C4_Enemy : C4_Character {

    void Start()
    {
        objectID.id = C4_ObjectManager.Instance.currentObjectCode++;
        objectID.type = ObjectID.Type.Enemy;
    }

    protected override bool checkHP()
    {
        if (boatFeature.hp < 0)
        {
            Destroy(this);
            return true;
        }
        else return false;
    }
}
