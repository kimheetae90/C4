using UnityEngine;
using System.Collections;

public class C4_Missile : C4_Object {

    [System.NonSerialized]
    public C4_MissileMove moveScript;

    Vector3 toMove;

    void Start()
    {
        objectAttr.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        objectAttr.type = GameObjectType.Missile;
        gameObject.SetActive(false);
        moveScript = GetComponent<C4_MissileMove>();
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
