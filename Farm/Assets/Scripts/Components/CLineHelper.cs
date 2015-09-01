﻿using UnityEngine;
using System.Collections;

public class CLineHelper : MonoBehaviour {

    int lineNum;
    GameObject tmpLineMesh;

    /// <summary>
    /// 파라미터로 넘겨준 게임 오브젝트의 line Number를 'lineNum' 변수에 저장 시키는 함수.
    /// </summary>
    /// <param name="gameObject"></param>
    void FindLineOfGameObject(GameObject gameObject)
    {
        float xPos = gameObject.transform.position.x;
        if (xPos < -18.5f)
        {
            lineNum = 0;
            return;
        }
           
        float yPos = gameObject.transform.position.y;

        if (1.5f <= yPos && yPos < 4.5f)
        {
            lineNum = 1;
        }
        else if (-2.1f <= yPos && yPos < 1.5f)
        {
            lineNum = 2;
        }
        else if (-5.5f <= yPos && yPos < -2.1f)
        {
            lineNum = 3;
        }
        else if (-9.0f <= yPos && yPos < -5.5f)
        {
            lineNum = 4;
        }
    }

    /// <summary>
    /// 게임 오브젝트의 위치를 line Number에 해당하는
    /// LineMesh의 Position으로 이동시켜주는 함수
    /// </summary>
    /// <param name="_gameObject"></param>
    public void OrderingYPos(GameObject gameObject)
    {
        FindLineOfGameObject(gameObject);

        if (lineNum == 0)
            return;

        tmpLineMesh = GameObject.Find("LineMesh" + lineNum);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, tmpLineMesh.transform.position.y, 0);
    }
}
