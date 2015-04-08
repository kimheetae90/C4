using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public class C4_Camera : C4_Object, C4_IControllerListener
{
    double moveSpeed;
    Vector3 toMove;

    protected override void Start()
    {
        base.Start();
        C4_Object obj = this;
        C4_GameManager.Instance.objectManager.registerObjectToAll(ref obj, GameObjectType.Camera, GameObjectInputType.Invalid);
    }

    void cameraMove(InputData inputData)
    {
        StopCoroutine("moveToSomeObjectCoroutine");
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
    }

    void moveToSomeObject(InputData inputData)
    {
        toMove = inputData.clickPosition;
        moveSpeed = Vector3.Distance(toMove, transform.position);
        StartCoroutine("moveToSomeObjectCoroutine");
    }

    IEnumerator moveToSomeObjectCoroutine()
    {
        yield return null;

        double distance = Vector3.Distance(toMove,transform.position);

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

    public void onEvent(string message, params object[] p)
    {
        switch (message)
        {
            case "Move":
                {
                    InputData data = (InputData)p[0];
                    cameraMove(data);
                }
                break;
            case "Focus":
                {
                    InputData data = (InputData)p[0];
                    moveToSomeObject(data);
                }
                break;
        }
    }
}