using UnityEngine;
using System.Collections;

public class C4_Missile : C4_Object {

    [System.NonSerialized]
    public C4_MissileMove moveScript;

    Vector3 toMove;

    void Start()
    {
        gameObject.SetActive(false);
        moveScript = GetComponent<C4_MissileMove>();

        C4_Object obj = this;
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref obj, GameObjectType.Missile);
    }

    public void startMove(Vector3 click)
    {
        toMove = click;
        Invoke("startMoveScript", 0.04f);
    }

    void startMoveScript()
    {
        moveScript.toMove = toMove;
        moveScript.startMove();
    }
}
