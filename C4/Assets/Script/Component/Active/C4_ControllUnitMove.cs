using UnityEngine;
using System.Collections;

public class C4_ControllUnitMove : MonoBehaviour {

    C4_Unit unit;
    C4_UnitFeature unitFeature;
    C4_Move moveScript;

    double moveLength;
    Vector3 startPosition;

    void Start()
    {
        unit = GetComponent<C4_Unit>();
        moveScript = GetComponent<C4_Move>();
        unitFeature = GetComponent<C4_UnitFeature>();
        moveLength = 0;
        startPosition = transform.position;
    }

    public void startCheckGageAndControlMove()
    {
        StartCoroutine("checkGageAndControllMove");
    }

    void stopBriefly()
    {
        moveScript.stopMoveToTarget();
    }

    void stopCompletely()
    {
        stopBriefly();
        moveLength = 0;
        startPosition = transform.position;
        StopCoroutine("checkGageAndControllMove");
    }

    void startMoveToNextPosition()
    {
        moveLength = 0;
        startPosition = transform.position;
        unitFeature.activeDone();
        moveScript.setMoving();
    }

    IEnumerator checkGageAndControllMove()
    {
        yield return null;

        moveLength = Vector3.Distance(transform.position, startPosition);

        if(moveScript.isMove)
        {
            if (moveLength >= unitFeature.moveRange)
            {
                stopBriefly();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, moveScript.toMove) < unitFeature.moveSpeed * 0.02f)
            {
                stopCompletely();
            }
            else if (unit.canActive)
            {
                startMoveToNextPosition();
            }
        }
        StartCoroutine("checkGageAndControllMove");
    }
}
