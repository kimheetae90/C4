﻿using UnityEngine;
using System.Collections;

/// <summary>
///  움직이는 기능의 스크립트
///  setToMove : 움직일 지점을 선택하고 코루틴을 시작한다.
///  move : 움직이는 코루틴, 목표지점에 다다를때까지 계속 움직이며 목표지점에 다다르면 코루틴이 종료된다.
///  움직임의 상태는 isMove로 체크한다.
/// </summary>

public class Move : MonoBehaviour {

    public float moveSpeed;
    Vector3 toMove;

    [System.NonSerialized]
    public bool isMove;
    bool isCoroutine;

	// Use this for initialization
    void Start()
    {
        toMove = transform.position;
        isMove = false;
        isCoroutine = false;
	}

    public void setToMove(Vector3 click)
    {
        toMove = click;
        isMove = true;
        if (!isCoroutine)
        {
            StartCoroutine(move());
            isCoroutine = true;
        }
    }
    
    IEnumerator move()
    {
        yield return null;
        if (isMove)
        {
            float distance = Vector3.Distance(toMove, transform.position);
            if (distance > 0.5f)
            {
                transform.Translate((toMove - transform.position).normalized * moveSpeed * Time.deltaTime);
                StartCoroutine("move");
            }
            else
            {
                isMove = false;
                isCoroutine = false;
                StopCoroutine("move");
            }
        }
        else
        {
            isCoroutine = false;
            StopCoroutine("move");
        }
    }
}