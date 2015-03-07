using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public class C4_Camera : MonoBehaviour {

    public void cameraMove(InputData inputData)
    {
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
    }
}