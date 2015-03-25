using UnityEngine;
using System.Collections;

public class C4_DistanceCheck : MonoBehaviour
{

    // Use this for initialization
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;
    [System.NonSerialized]
    public C4_Move move;
    [System.NonSerialized]
    public float distance;
    [System.NonSerialized]
    public int range;

    Vector3 firstpos;
    [System.NonSerialized]
    public bool isOver;
    [System.NonSerialized]
    public int gage;

    // Use this for initialization
    void Start()
    {
        isOver = false;
        boatFeature = transform.GetComponent<C4_BoatFeature>();
    }

    public void DistCheck()  // 진입하는 함수(Boat에서 호출)
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
                isMove = false;
                StopCoroutine(distanceCheck());
            }

        }
        if (isOver)
        {
            boatFeature.gageDown(boatFeature.needGageStackToMove);
            isOver = false;
        }

        if (isMove)
        {
            StartCoroutine(distanceCheck());
        }
        else
        {
            StopCoroutine(distanceCheck());
        }

    }
}
