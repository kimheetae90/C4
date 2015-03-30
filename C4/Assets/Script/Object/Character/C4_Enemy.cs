using UnityEngine;
using System.Collections;

public class C4_Enemy : C4_Character {

    protected override void Start()
    {
        base.Start();
        C4_Object enemy = GetComponent<C4_Object>();
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref enemy, GameObjectType.Enemy, GameObjectInputType.Invalid);
    }

    protected override void checkHP()
    {
        if (boatFeature.hp <= 0)
        {
            C4_ManagerMaster.Instance.objectManager.reserveRemoveObject(GetComponent<C4_Object>());
        }
    }
}
