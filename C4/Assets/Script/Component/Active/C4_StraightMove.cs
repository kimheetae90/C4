using UnityEngine;
using System.Collections;

/// <summary>
///  움직이는 기능의 스크립트
///  move : 움직이는 코루틴, 목표지점에 다다를때까지 계속 움직이며 목표지점에 다다르면 코루틴이 종료된다.
///  움직임의 상태는 isMove로 체크한다.
/// </summary>

public class C4_StraightMove : C4_Move
{
	// Use this for initialization
    void Start()
    {
        toMove = transform.position;
        isMove = false;
        isCoroutine = false;
	}

    void moveToTarget()
    {
        transform.Translate((toMove - transform.position).normalized * moveSpeed * Time.deltaTime);
        StartCoroutine("move");
    }

    public void stopMoveToTarget()
    {
        isMove = false;
        isCoroutine = false;
        StopCoroutine("move");
    }

    protected override IEnumerator move()
    {
        Debug.Log("fuck");
        yield return null;
        if (isMove)
        {
            float distance = Vector3.Distance(toMove, transform.position);
            if (distance > moveSpeed * 0.02f)
            {
                moveToTarget();
            }
            else
            {
                stopMoveToTarget();
            }
        }
        else
        {
            stopMoveToTarget();
        }
    }
}