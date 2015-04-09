using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public abstract class C4_Camera : C4_Object, C4_IControllerListener
{
    protected double moveSpeed;
    protected Vector3 toMove;

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

    protected abstract void moveToSomeObject(InputData inputData);

    protected abstract IEnumerator moveToSomeObjectCoroutine();

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