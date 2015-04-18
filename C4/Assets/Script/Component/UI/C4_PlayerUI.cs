using UnityEngine;
using System.Collections;

public class C4_PlayerUI : MonoBehaviour, C4_IControllerListener
{
    C4_AimUI aimUI;
    C4_MoveUI moveUI;
    C4_SelectUI selectUI;

    void Start()
    {
        aimUI = GetComponent<C4_AimUI>();
        moveUI = GetComponent<C4_MoveUI>();
        selectUI = GetComponent<C4_SelectUI>();
    }

    public void aiming(Vector3 clickPosition)
    {
        if (C4_GameManager.Instance.sceneMode.GetComponent<C4_PlayMode> ().allyController.selectedAllyUnit != null) 
		{
			aimUI.showUI (clickPosition);
		} 
		else
		{
			activeDone();
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
                    Vector3 pos = (Vector3)p[0];
                    aiming(pos);
                }
                break;
            case "Select":
                {
                    transform.SetParent((Transform)p[0],false);
                    transform.localPosition = new Vector3(0, 0, 0);
                    select();
                }
                break;
        }
    }
}