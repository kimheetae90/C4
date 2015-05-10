using UnityEngine;
using System.Collections;

public class C4_Enemy : C4_Unit {

    bool sendGageFullMessageToController;

    public bool SendGageFullMessageToController
    {
        get { return sendGageFullMessageToController; }
        set { sendGageFullMessageToController = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        sendGageFullMessageToController = false;
        C4_Object enemy = GetComponent<C4_Object>();
        C4_GameManager.Instance.objectManager.registerObjectToAll(ref enemy, GameObjectType.Enemy, GameObjectInputType.Invalid);
    }

	protected override void checkHP()
	{
		if (unitFeature.hp <= 0)
		{
			C4_GameManager.Instance.objectManager.reserveRemoveObject(GetComponent<C4_Object>());
		}
	}

    protected override void checkActive()
    {
        base.checkActive();
        if (sendGageFullMessageToController)
        {
            if (!canActive)
            {
                sendGageFullMessageToController = false;
            }
        }
        else if (canActive)
        {
            sendGageFullMessageToController = true;
            C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().addFullGageEnemy(this);
        }
    }
}
