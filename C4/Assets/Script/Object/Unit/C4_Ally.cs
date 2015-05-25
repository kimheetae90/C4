﻿using UnityEngine;
using System;
using System.Collections;

public class C4_Ally : C4_Unit , C4_IControllerListener {

    DateTime shotTime;

    protected override void Awake()
    {
        base.Awake();
        C4_Object ally = GetComponent<C4_Object>();
        C4_GameManager.Instance.objectManager.registerObjectToAll(ref ally, GameObjectType.Ally, GameObjectInputType.SelectAbleObject | GameObjectInputType.ClickAbleObject);
    } 	
	
	protected override void checkHP()
	{
		if (unitFeature.hp <= 0)
		{
			C4_GameManager.Instance.objectManager.reserveRemoveObject(GetComponent<C4_Object>());
			C4_GameManager.Instance.sceneMode.GetComponent<C4_PlayMode> ().playerUI.activeDone ();
			C4_GameManager.Instance.sceneMode.GetComponent<C4_PlayMode>().allyController.activeDone();
            FindObjectOfType<C4_ButtonUI>().unactive(this.gameObject);
		}
	}

    public void onEvent(string message, params object[] p)
    {
        switch(message)
        {
            case "Aim":
                {
					doAim(p);
                }
                break;
            case "Move":
                {
                    doMove(p);

                }
                break;
        }
    }

    public override void onUserEvent(AnimEventUserMsg msg) 
    {
        switch(msg.title)
        {
            case "Shot":
				doShot ();
                break;
        }
    }

    private void doAim(params object[] p)
    {
		aimPosition = (Vector3)p [2];
        C4_ControllUnitMove controllUnitMove = GetComponentInParent<C4_ControllUnitMove>();
        if (controllUnitMove != null)
        {
            controllUnitMove.stopCompletely();
        }
		turn(aimPosition);

    }

    private void doMove(params object[] p)
    {
        Vector3 pos = (Vector3)p[0];
        move(pos);
        turn(pos);

        C4_AutomoveCancleUI Automove = gameObject.GetComponentInChildren<C4_AutomoveCancleUI>();

        if (Automove != null)
        {
            Automove.startAutomoveCancleUI();
        }
    }

    private void doShot()
    {
        shot(aimPosition);
        shotTime = DateTime.Now;
    }

    public DateTime getLastShotTime()
    {
        return shotTime;
    }
}
