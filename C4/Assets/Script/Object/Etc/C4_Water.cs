using UnityEngine;
using System.Collections;

public class C4_Water : C4_Ground
{

    protected override void Start()
    {
        base.Start();
        C4_Object me = this;
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref me, GameObjectType.Ground, GameObjectInputType.CameraMoveAbleObject);
    }

}
