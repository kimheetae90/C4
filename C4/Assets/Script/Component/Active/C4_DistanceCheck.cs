using UnityEngine;
using System.Collections;

public class C4_DistanceCheck : MonoBehaviour
{

    // Use this for initialization
    C4_UnitFeature unitFeature;
    C4_StraightMove move;
    float distance;
    int range;

    Vector3 firstpos;
    bool isOver;

    // Use this for initialization
    void Start()
    {
        isOver = false;
        unitFeature = transform.GetComponent<C4_UnitFeature>();
        move = GetComponent<C4_StraightMove>();
    }

    public void distCheck()
    {
        unitFeature.gageDown(unitFeature.needGageStackToMove);
        range = unitFeature.moveRange;
        firstpos = transform.position;
        StartCoroutine("distanceCheck");
    }

    IEnumerator distanceCheck()
    {
        yield return null;
        distance = Vector3.Distance(firstpos, transform.position);
        if (distance >= range)
        {
            if (unitFeature.stackCount > 0)
            {
                isOver = true;
                range += unitFeature.moveRange;
            }
            else
            {
                move.stopMoveToTarget();
                StopCoroutine("distanceCheck");
            }

        }
        if (isOver)
        {
            unitFeature.gageDown(unitFeature.needGageStackToMove);
            isOver = false;
        }

        if (move.isMove)
        {
            StartCoroutine("distanceCheck");
        }
        else
        {
            StopCoroutine("distanceCheck");
        }

    }
}
