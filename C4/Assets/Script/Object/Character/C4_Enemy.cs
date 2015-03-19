using UnityEngine;
using System.Collections;

public class C4_Enemy : C4_Character {

    protected override bool checkHP()
    {
        if (boatFeature.hp < 0)
        {
            C4_ObjectManager.Instance.reserveRemoveObject(GetComponent<C4_Object>());
            return true;
        }
        else return false;
    }
}
