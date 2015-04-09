using UnityEngine;
using System.Collections;

public class C4_CameraController : C4_Controller 
{
	enum eCameraControllerActionState
	{
		None,
        Focus,
		Move,
		ZoomIn,
		ZoomOut,
	}

    public override void Awake()
    {
        base.Awake();
    }
	
	override public void dispatchData(InputData inputData)
	{
		eCameraControllerActionState action = eCameraControllerActionState.None;
		computeActionState(ref inputData, out action);
		ProcState(action, ref inputData);
	}

	private void computeActionState(ref InputData inputData, out eCameraControllerActionState action)
	{
		if (inputData.keyState == KeyState.Down)
		{
			computeKeyDownState(ref inputData, out action);
		}
		else if(inputData.keyState == KeyState.Drag)
		{
			computeKeyDragState(ref inputData, out action);
		}
		else
		{
			computeKeyUpState(ref inputData, out action);
		}
	}

	private void computeKeyDownState(ref InputData inputData, out eCameraControllerActionState action)
	{
		action = eCameraControllerActionState.None;
	}
	
	private void computeKeyUpState(ref InputData inputData, out eCameraControllerActionState action)
	{
		action = eCameraControllerActionState.None;

        if (inputData.clickObjectID.type == GameObjectType.Ally && inputData.dragObjectID.type == GameObjectType.Ally && inputData.clickObjectID.id == inputData.dragObjectID.id)
        {
            action = eCameraControllerActionState.Focus;
        }
	}

	private void computeKeyDragState(ref InputData inputData, out eCameraControllerActionState action)
	{
		action = eCameraControllerActionState.None;

		if(inputData.clickObjectID.isInputTypeTrue (GameObjectInputType.CameraMoveAbleObject)) 
		{
            action = eCameraControllerActionState.Move;
        }
    }

    private void ProcState(eCameraControllerActionState action, ref InputData inputData)
    {
        switch (action)
        {
            case eCameraControllerActionState.None:
                break;
            case eCameraControllerActionState.Move:
                notifyEvent("Move", inputData);
                break;
            case eCameraControllerActionState.Focus:
                notifyEvent("Focus", inputData);
                break;
            case eCameraControllerActionState.ZoomIn:
                break;
            case eCameraControllerActionState.ZoomOut:
                break;
		}
	}
}
