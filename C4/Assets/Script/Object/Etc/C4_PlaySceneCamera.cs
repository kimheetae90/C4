using UnityEngine;
using System.Collections;

public class C4_PlaySceneCamera : C4_Camera
{
    protected override void moveToSomeObject(InputData inputData)
    {
        toMove = inputData.clickPosition;
        moveSpeed = Vector3.Distance(toMove, transform.position);
        StartCoroutine("moveToSomeObjectCoroutine");
    }

    protected override IEnumerator moveToSomeObjectCoroutine()
    {
        yield return null;

        double distance = Vector3.Distance(toMove, transform.position);

        if (distance > moveSpeed * 0.2f)
        {
            transform.Translate((toMove - transform.position).normalized * (float)moveSpeed * 10f * Time.deltaTime);
            StartCoroutine("moveToSomeObjectCoroutine");
        }
        else if (distance > moveSpeed * 0.02f)
        {
            transform.Translate((toMove - transform.position).normalized * (float)moveSpeed * Time.deltaTime);
            StartCoroutine("moveToSomeObjectCoroutine");
        }
        else
        {
            StopCoroutine("moveToSomeObjectCoroutine");
        }
    }
}
