using UnityEngine;
using System.Collections;

public class C4_Move : MonoBehaviour {

    protected float moveSpeed;
    protected Vector3 toMove;
    protected bool isCoroutine;

    [System.NonSerialized]
    public bool isMove;

    public void setMoveSpeed(float inputMoveSpeed)
    {
        moveSpeed = inputMoveSpeed;
    }

    public void setToMove(Vector3 inputToMove)
    {
        toMove = inputToMove;
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

    public void startMove(Vector3 inputToMove)
    {
        setToMove(inputToMove);
        setMoving();
    }

    protected virtual IEnumerator move() 
    {
        yield return null;
    }
}
