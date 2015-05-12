using UnityEngine;
using System.Collections;

public abstract class C4_Move : MonoBehaviour {

    public Vector3 toMove;
    protected float moveSpeed;
    protected bool isCoroutine;
    [System.NonSerialized]
    public bool isMove;

    public void setMoveSpeed(float inputMoveSpeed)
    {
        moveSpeed = inputMoveSpeed;
    }

    public void setMoving()
    {
        isMove = true;
        if (!isCoroutine)
        {
            StartCoroutine("move");
            isCoroutine = true;
        }
    }

    protected abstract void moveToTarget();

    public abstract void stopMoveToTarget();

    public void startMove(Vector3 inputToMove)
    {
        toMove = inputToMove;
        setMoving();
    }

    protected virtual IEnumerator move() 
    {
        yield return null;
    }
}
