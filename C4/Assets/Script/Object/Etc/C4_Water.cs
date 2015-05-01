using UnityEngine;
using System.Collections;

public class C4_Water : C4_Ground
{

    protected override void Awake()
    {
		base.Awake ();
		C4_Object me = GetComponent<C4_Object>();
		C4_GameManager.Instance.objectManager.registerObjectToAll(ref me, GameObjectType.Ground, GameObjectInputType.CameraMoveAbleObject | GameObjectInputType.ToMoveAbleObject);
	}
}
