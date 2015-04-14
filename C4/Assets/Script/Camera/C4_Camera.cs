using UnityEngine;
using System.Collections;

/// <summary>
///  카메라
/// </summary>
public abstract class C4_Camera : MonoBehaviour, C4_IControllerListener
{
    protected float moveSpeed;
    protected Vector3 toMove;

    protected virtual void Awake()
    {
		moveSpeed = 0;
    }

    void cameraMove(InputData inputData)
    {
        StopCoroutine("moveToSomeObjectCoroutine");
        transform.Translate(inputData.clickPosition - inputData.dragPosition);
    }
    
	protected virtual void zooming(InputData data)
	{
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
		case "Zooming":
				{
					InputData data = (InputData)p[0];
					zooming (data);
				}
				break;
        }
    }
}