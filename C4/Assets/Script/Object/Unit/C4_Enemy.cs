using UnityEngine;
using System.Collections;

public class C4_Enemy : C4_Unit {

    bool sendGageFullMessageToController;

    protected override void Start()
    {
        base.Start();
        sendGageFullMessageToController = false;
        C4_Object enemy = GetComponent<C4_Object>();
        C4_GameManager.Instance.objectManager.registerObjectToAll(ref enemy, GameObjectType.Enemy, GameObjectInputType.Invalid);
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
