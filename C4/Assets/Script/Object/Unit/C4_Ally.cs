using UnityEngine;
using System.Collections;

public class C4_Ally : C4_Unit , C4_IControllerListener {
    
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
		}
	}

    public void onEvent(string message, params object[] p)
    {
        switch(message)
        {
            case "Aming":
                {
                    Vector3 pos = (Vector3)p[0];
                    turn(pos);
                }
                break;
            case "Move":
                {
                    gameObject.GetComponentInChildren<C4_AutomoveCancleUI>().startAutomoveCancleUI();
                    Vector3 pos = (Vector3)p[0];
                    move(pos);
                    turn(pos);
                }
                break;
            case "Shot":
                {
                    Vector3 pos = (Vector3)p[0];
                    shot(pos);
                }
                break;
        }
    }
}
