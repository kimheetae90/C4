using UnityEngine;
using System.Collections;

public class C4_Player : C4_Character {
    
    protected override bool checkHP()
    {
        if (boatFeature.hp < 0)
        {
            return true;
        }
        else return false;
    }
}
