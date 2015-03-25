using UnityEngine;
using System.Collections;

public class C4_DistanceCheck : MonoBehaviour
{

    // Use this for initialization
    C4_BoatFeature boatFeature;
    C4_StraightMove move;
    float distance;
    int range;

    Vector3 firstpos;
    bool isOver;

    // Use this for initialization
    void Start()
    {
        isOver = false;
        boatFeature = transform.GetComponent<C4_BoatFeature>();
        move = GetComponent<C4_StraightMove>();
    }

    public void DistCheck()
    {
        range = boatFeature.moveRange;
        firstpos = transform.position;
        StartCoroutine(distanceCheck());

    }



    IEnumerator distanceCheck()
    {
        yield return null;
        distance = Vector3.Distance(firstpos, transform.position);
        if (distance >= range)
        {
            if (boatFeature.stackCount > 0)
            {
                isOver = true;
                range += boatFeature.moveRange;
            }
            else
            {
                move.isMove = false;
                StopCoroutine(distanceCheck());
            }

        }
        if (isOver)
        {
            boatFeature.gageDown(boatFeature.needGageStackToMove);
            isOver = false;
        }

        if (move.isMove)
        {
            StartCoroutine(distanceCheck());
        }
        else
        {
            StopCoroutine(distanceCheck());
        }

    }
}
