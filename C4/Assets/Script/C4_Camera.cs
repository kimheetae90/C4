using UnityEngine;
using System.Collections;

public class C4_Camera : MonoBehaviour {

    public void cameraMove(InputData inputData)
    {
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
    }

}