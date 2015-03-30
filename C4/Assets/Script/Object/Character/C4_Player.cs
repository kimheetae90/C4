using UnityEngine;
using System.Collections;

public class C4_Player : C4_Character , C4_IControllerListener {

    protected override void Start()
    {
        base.Start();
        C4_Object player = GetComponent<C4_Object>();
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref player, GameObjectType.Player, GameObjectInputType.SelectAbleObject | GameObjectInputType.ClickAbleObject);
    }

    protected override void checkHP()
    {
        if (boatFeature.hp <= 0)
        {
            gameObject.SetActive(false);
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
