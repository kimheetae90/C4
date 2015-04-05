using UnityEngine;
using System.Collections;

/// <summary>
///  움직이는 기능의 스크립트
///  move : 움직이는 코루틴, 목표지점에 다다를때까지 계속 움직이며 목표지점에 다다르면 코루틴이 종료된다.
///  움직임의 상태는 isMove로 체크한다.
/// </summary>

public class C4_ParabolicMove2 : C4_Move
{

    private Vector3 firstPos;

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

    void moveToTarget()
    {
        if (isfirst)
        {
            isfirst = false;
            firstPos = transform.position;

        }

        //Debug.Log(count);

        //Debug.Log("handle x = "+handle.x+",y = "+handle.y+",z = "+handle.z);
        point1 = CalculateParabolicPoint(firstPos + (toMove - firstPos) * count, firstPos, toMove);
        transform.position = point1;

        transform.GetChild(0).rotation = Quaternion.LookRotation((point1 - point0).normalized);




        Debug.DrawLine(point0, point1, Color.red, 1000);

        point0 = point1;

        StartCoroutine("move");
    }

    public void stopMoveToTarget()
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

            count += 1 / Vector3.Distance(toMove, firstPos);
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


    public Vector3 CalculateParabolicPoint(Vector3 t, Vector3 first, Vector3 last)
    {
        //x2/a2＋y2/b2=2z


        Vector3 p;

        float x = t.x;
        float z = t.z;

        float y = -0.02f * ((x - first.x) * (x - last.x) + (z - first.x) * (z - last.z));

        p = new Vector3(x, y, z);

        return p;
    }


}