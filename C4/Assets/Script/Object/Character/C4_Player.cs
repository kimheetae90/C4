using UnityEngine;
using System.Collections;

public class C4_Player : C4_Character {

    void Start()
    {
        objectID.id = C4_ObjectManager.Instance.currentObjectCode++;
        objectID.type = ObjectID.Type.Player;
    }

    protected override bool checkHP()
    {
        if (boatFeature.hp < 0)
        {
            return true;
        }
        else return false;
    }
}
