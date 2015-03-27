using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public class C4_Camera : C4_Object {

    public void Start()
    {
        C4_Object obj = this;
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref obj,GameObjectType.Camera);
    }

    public void cameraMove(InputData inputData)
    {
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
    }
}