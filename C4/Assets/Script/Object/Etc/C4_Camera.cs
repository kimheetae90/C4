using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public class C4_Camera : C4_Object, C4_IControllerListener
{

    protected override void Start()
    {
        base.Start();
        C4_Object obj = this;
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref obj, GameObjectType.Camera, GameObjectInputType.Invalid);
    }

    public void cameraMove(InputData inputData)
    {
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
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
        }
    }
}