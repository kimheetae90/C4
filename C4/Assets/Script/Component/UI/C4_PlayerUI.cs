using UnityEngine;
using System.Collections;

public class C4_PlayerUI : MonoBehaviour, C4_IControllerListener
{
	C4_AimUI aimUI;
	C4_MoveUI moveUI;
	C4_SelectUI selectUI;
	C4_TargetSpotUI targetspotUI;
	C4_TargetBarUI targetbarUI;
    C4_AimLimitUI aimlimitUI;

	void Start()
	{
		aimUI = GetComponent<C4_AimUI>();
		moveUI = GetComponent<C4_MoveUI>();
		selectUI = GetComponent<C4_SelectUI>();
		targetspotUI = GetComponent<C4_TargetSpotUI>();
		targetbarUI = GetComponent<C4_TargetBarUI>();
        aimlimitUI = GetComponent<C4_AimLimitUI>();
	}
	
	public void aiming(Vector3 clickPosition, C4_Ally allyUnit, Vector3 targetPos)
	{
		
		float maxAttackRange = allyUnit.GetComponent<C4_UnitFeature>().attackRange;

		if (C4_GameManager.Instance.sceneMode.GetComponent<C4_PlayMode> ().allyController.selectedAllyUnit != null) 
		{
			if(allyUnit.canActive)
			{
				aimUI.showUI (clickPosition, maxAttackRange);
			}
			else
			{
				aimUI.showCannotActiveUI (clickPosition, maxAttackRange);
			}
		}
		else
		{
			activeDone();
		}
		showTargetUI (targetPos);
        aimlimitUI.showUI(maxAttackRange);
	}

	public void showTargetUI(Vector3 targetPos)
	{
		
		switch (C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit.GetComponent<C4_UnitFeature>().missile.GetComponent<C4_MissileFeature>().type)
		{
		case 1: 
			targetbarUI.showUI(targetPos);
			break;
		case 2: 
			targetspotUI.showUI(targetPos);
			break;
        case 3:
            targetbarUI.showUI(targetPos);
            break;
		}
	}
	
	public void startAim()
	{
		moveUI.hideUI();
	}
	
	public void select()
	{
		if (C4_GameManager.Instance.sceneMode.GetComponent<C4_PlayMode> ().allyController.selectedAllyUnit != null) 
		{
			moveUI.selectBoat ();
			selectUI.showUI ();
			aimUI.hideUI ();
			targetbarUI.hideUI();
			targetspotUI.hideUI();
            aimlimitUI.hideUI();
		} 
		else 
		{
			activeDone();
		}
	}

	public void aimCancle()
	{
		moveUI.showUI ();
		aimUI.hideUI ();
		targetbarUI.hideUI();
		targetspotUI.hideUI();
        aimlimitUI.hideUI();
	}

	public void activeDone()
	{
		aimUI.hideUI();
		moveUI.hideUI();
		selectUI.hideUI();
		targetspotUI.hideUI();
		targetbarUI.hideUI();
        aimlimitUI.hideUI();
		transform.SetParent(null);
	}
	
	public void onEvent(string message, params object[] p)
	{
		switch (message)
		{
		case "ActiveDone":
		{
			activeDone();
		}
			break;
		case "AimStart":
		{
			startAim();
		}
			break;
		case "Aim":
		{
			Vector3 pos = (Vector3)p[0];
			C4_Ally allyUnit = (C4_Ally)p[1];
			Vector3 targetPos = (Vector3)p[2];
			aiming(pos , allyUnit, targetPos);
		}
			break;
		case "Select":
		{
			transform.SetParent((Transform)p[0],false);
			transform.localPosition = new Vector3(0, 0, 0);
			select();
		}
			break;
		case "AimCancle":
		{
			aimCancle();
		}
			break;
		}
	}
}