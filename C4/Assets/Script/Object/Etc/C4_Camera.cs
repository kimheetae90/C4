using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public class C4_Camera : C4_Object {

    public void Start()
    {
        objectAttr.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        objectAttr.type = GameObjectType.Camera;
    }

    public void cameraMove(InputData inputData)
    {
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
    }
}