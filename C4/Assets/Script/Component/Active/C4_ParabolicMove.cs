using UnityEngine;
using System.Collections;

/// <summary>
///  움직이는 기능의 스크립트
///  move : 움직이는 코루틴, 목표지점에 다다를때까지 계속 움직이며 목표지점에 다다르면 코루틴이 종료된다.
///  움직임의 상태는 isMove로 체크한다.
/// </summary>

public class C4_ParabolicMove : C4_Move
{

    private Vector3 firstPos;
    private Vector3 handle;
    private Vector3 point0;
    private Vector3 point1;

    private bool isfirst = true;
    private float count = 0.0f;
    // Use this for initialization
    void Start()
    {

        firstPos = transform.position;
        //toMove = transform.position;
        isMove = false;
        isCoroutine = false;
        point0 = firstPos;

    }

    protected override void moveToTarget()
    {
        if (isfirst)
        {
            isfirst = false;
            firstPos = transform.position;
        }

        //Debug.Log(count);
        handle.x = firstPos.x + (toMove.x - firstPos.x) / 2;
        handle.z = firstPos.z + ((toMove.z - firstPos.z) / 2);
        handle.y = Vector3.Distance(toMove, firstPos) / 2;
        //Debug.Log("handle x = "+handle.x+",y = "+handle.y+",z = "+handle.z);
        point1 = CalculateBezierPoint(count, firstPos, handle, handle, toMove);
        transform.position = point1;

        transform.GetChild(0).rotation = Quaternion.LookRotation((point1 - point0).normalized);




        Debug.DrawLine(point0, point1, Color.red, 1000);

        point0 = point1;

        StartCoroutine("move");
    }

    public override void stopMoveToTarget()
    {

        isMove = false;
        isCoroutine = false;
        StopCoroutine("move");
        count = 0.0f;
        isfirst = true;

    }

    protected override IEnumerator move()
    {
        yield return null;
        if (isMove)
        {

            count += (moveSpeed * 0.015f) / Vector3.Distance(toMove, firstPos);
            //count += 0.01f;
            //float distance = Vector3.Distance(toMove, transform.position);
            if (count < 1)
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

    public Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        /*
         [x,y,z] = (1-t)^3 p0 + 3(1-t)^2 t p1 +3(1-t) t^2 p2 + t^3 p3
 
            where:
            t represents a value which ranges from 0 (starting point of the curve) to 1 (ending point of the curve)
            p0 is the starting point of the curve
            p3 is the ending point of the curve
            p1,p2 are the handles of the bezier
            0<=t<=1
          
         */

        float u;
        float uu;
        float uuu;
        float tt;
        float ttt;
        Vector3 p;
        u = 1 - t;
        uu = u * u;
        uuu = uu * u;
        tt = t * t;
        ttt = tt * t;
        p = uuu * p0; //first term of the equation
        p += 3 * uu * t * p1; //second term of the equation
        p += 3 * u * tt * p2; //third term of the equation
        p += ttt * p3; //fourth term of the equation
        return p;
    }


}