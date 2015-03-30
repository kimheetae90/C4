﻿using UnityEngine;
using System.Collections;

public class C4_Missile : C4_SubObject
{
    public C4_StraightMove moveScript;
    Vector3 toMove;

    protected override void Start()
    {
        base.Start();
        C4_Object missile = GetComponent<C4_Object>();
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref missile, GameObjectType.Missile, GameObjectInputType.Invalid);
        gameObject.SetActive(false);
    }

    public void startMove(Vector3 click)
    {
        toMove = click;
        gameObject.SetActive(true);
        Invoke("startMoveScript", 0.04f);
    }

    void startMoveScript()
    {
        GetComponent<C4_MovingCheckAndSetActive>().startChecking();
        moveScript.setMoving(toMove);
    }
}
