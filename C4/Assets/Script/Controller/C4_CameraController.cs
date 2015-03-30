﻿using UnityEngine;
using System.Collections;

public class C4_CameraController : C4_Controller 
{
	C4_Camera camObject;

	enum eCameraControllerActionState
	{
		None,
		Move,
		ZoomIn,
		ZoomOut,
	}

	public override void Start()
	{
		base.Start();

		camObject = Camera.main.transform.root.GetComponent<C4_Camera>();
	}
	
	override public void dispatchData(InputData inputData)
	{
		if (camObject == null)
			return;

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
			camObject.cameraMove(inputData);
			break;
		case eCameraControllerActionState.ZoomIn:
			break;
		case eCameraControllerActionState.ZoomOut:
			break;
		}
	}
}