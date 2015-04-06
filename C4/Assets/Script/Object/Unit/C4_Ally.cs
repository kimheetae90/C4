using UnityEngine;
using System.Collections;

public class C4_Ally : C4_Unit , C4_IControllerListener {

    C4_ControllUnitMove moveControllScript;

    protected override void Start()
    {
        base.Start();
        moveControllScript = GetComponent<C4_ControllUnitMove>();
        C4_Object ally = GetComponent<C4_Object>();
        C4_GameManager.Instance.objectManager.registerObjectToAll(ref ally, GameObjectType.Ally, GameObjectInputType.SelectAbleObject | GameObjectInputType.ClickAbleObject);
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
                    moveControllScript.startCheckGageAndControlMove();
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
