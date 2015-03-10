using UnityEngine;
using System.Collections;

public class C4_Enemy : C4_Character {

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
