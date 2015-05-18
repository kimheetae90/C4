using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_Enemy : C4_Unit {

	bool sendGageFullMessageToController;
	Vector3 currentAimPos;


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

	public override void onUserEvent(AnimEventUserMsg msg) 
    {
        switch(msg.title)
        {
            case "Shot":
				doShot ();
                break;
        }
    }

	public void doShot()
	{
		currentAimPos = GetComponent<BehaviorComponent> ().cachedStruct.objectsInFireRange[0].transform.position;
		shot (currentAimPos);
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
