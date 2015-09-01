using UnityEngine;
using System.Collections;

public struct InputData
{
	public enum KeyState
	{
		Up,
		Press,
		Down,
		Sleep,
		MultiTap,
	}
	
	
	//좌표 정보
	public Vector3 clickPosition;
	public Vector3 dragPosition;
	
	//클릭한 대상에 대한 정보
	public GameObject selectedGameObject;
	
	//클릭,드래그 상태
	public KeyState keyState;
	public KeyState preKeyState;
	
	public float preMultiTapDistance;
	public float multiTapDistance;
	
	//화면 좌표 정보
	public Vector2 clickDevicePosition;
	public Vector2 dragDevicePosition;

	public void clear()
	{
		clickPosition = Vector3.zero;
		dragPosition = Vector3.zero;
		clickDevicePosition = Vector2.zero;
		dragDevicePosition = Vector2.zero;
		selectedGameObject = null;
		keyState = KeyState.Sleep;
		preKeyState = KeyState.Sleep;
	}
}

