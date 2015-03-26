using UnityEngine;
using System.Collections;

public class C4_Missile : C4_SubObject
{

    [System.NonSerialized]
    public C4_StraightMove moveScript;

    Vector3 toMove;

    void Start()
    {
        gameObject.SetActive(false);
        moveScript = GetComponent<C4_StraightMove>();

        C4_Object obj = this;
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref obj, GameObjectType.Missile,GameObjectInputType.Invalid);
    }

    public void startMove(Vector3 click)
    {
        toMove = click;
        Invoke("startMoveScript", 0.04f);
    }

    void startMoveScript()
    {
        moveScript.setMoving(toMove);
    }
}
