using UnityEngine;
using System.Collections;

public class C4_PlayerUI : MonoBehaviour, C4_IControllerListener
{
    C4_AimUI aimUI;
    C4_MoveUI moveUI;
    C4_SelectUI selectUI;
    C4_TargetSpotUI targetspotUI;
    C4_TargetBarUI targetbarUI;

    void Start()
    {
        aimUI = GetComponent<C4_AimUI>();
        moveUI = GetComponent<C4_MoveUI>();
        selectUI = GetComponent<C4_SelectUI>();
        targetspotUI = GetComponent<C4_TargetSpotUI>();
        targetbarUI = GetComponent<C4_TargetBarUI>();
    }

    public void aiming(Vector3 clickPosition, C4_Ally allyUnit)
    {
        if (C4_GameManager.Instance.sceneMode.GetComponent<C4_PlayMode> ().allyController.selectedAllyUnit != null) 
		{
			if(allyUnit.canActive)
				aimUI.showUI (clickPosition);
			else
				aimUI.showCannotActiveUI (clickPosition);
		} 
		else
		{
			activeDone();
		}

        switch (C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit.GetComponent<C4_UnitFeature>().missile.GetComponent<C4_MissileFeature>().type)
        {
            case 1: targetbarUI.showUI(clickPosition);
                break;
            case 2: targetspotUI.showUI(clickPosition);
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
		} 
		else 
		{
			activeDone();
		}
    }

    public void activeDone()
	{
        aimUI.hideUI();
        moveUI.hideUI();
        selectUI.hideUI();
        targetspotUI.hideUI();
        targetbarUI.hideUI();
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
            case "StartAim":
                {
                    startAim();
                }
                break;
            case "Aming":
                {
<<<<<<< HEAD
                    Vector3 pos = (Vector3)p[0];
                    aiming(pos);
                    
                }
                break;
            case "Select":
                {
                    transform.SetParent((Transform)p[0],false);
=======
					Vector3 pos = (Vector3)p[0];
					C4_Ally allyUnit = (C4_Ally)p[1];
					aiming(pos , allyUnit);
				}
			break;
		case "Select":
		{
			transform.SetParent((Transform)p[0],false);
>>>>>>> master
                    transform.localPosition = new Vector3(0, 0, 0);
                    select();
                }
                break;
        }
    }
}