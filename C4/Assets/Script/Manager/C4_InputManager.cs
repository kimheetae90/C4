using UnityEngine;
using System.Collections;

/// <summary>
///  Input에 대한 것을 처리하는 Manager
///  - click Down, cliking, click Up의 경우로 나누어 처리한다
///  - input에 대한 Data를 수집하여 Camera와 Play Manager에게 전송한다
/// </summary>

public class C4_InputManager : MonoBehaviour
{
    InputData inputData;
    RaycastHit hit;
    C4_Object clickObject;
	float startMultiTapDictance;

    void Start()
    {
        inputData = new InputData();
		startMultiTapDictance = 0;
    }

    void Update()
    {
        updateKeyState();
        procKeyState();
    }


    private void updateKeyState()
    {
        inputData.preKeyState = inputData.keyState;
		inputData.preMultiTapDistance = inputData.multiTapDistance;

		if (Input.touchCount >= 2) 
		{
			if(Input.GetTouch(1).phase == TouchPhase.Began)
			{
				inputData.multiTapDistance = Vector2.Distance(Input.touches[0].position,Input.touches[1].position);
				inputData.preMultiTapDistance = inputData.multiTapDistance;
				inputData.keyState = KeyState.MultiTap;
			}
		} 
		else 
		{
			if (Input.GetMouseButtonDown(0)) {
				inputData.keyState = KeyState.Down;
			}
			if (Input.GetMouseButtonUp(0)) {
				inputData.keyState = KeyState.Up;
			}
		}

     
		if (inputData.keyState == inputData.preKeyState && inputData.keyState == KeyState.Down) 
		{
			inputData.keyState = KeyState.Drag;
		} 
		else if (inputData.keyState == inputData.preKeyState && inputData.keyState == KeyState.Up) 
		{
			inputData.keyState = KeyState.Sleep;
		}
    }

    private void procKeyState()
    {
        //Action
        if (inputData.keyState != inputData.preKeyState)
        {
            switch (inputData.keyState)
            {
                case KeyState.Up:
                    setupClickUp();
                    C4_GameManager.Instance.sceneMode.sendInputDataToController(inputData);
                    break;
                case KeyState.Down:
                    setupClickDown();
                    C4_GameManager.Instance.sceneMode.sendInputDataToController(inputData);
                    break;
            }
        }
        //Continuos
        else if (inputData.keyState == inputData.preKeyState)
        {
            switch (inputData.keyState)
            {
                case KeyState.Drag:
                    setupDrag();
                    C4_GameManager.Instance.sceneMode.sendInputDataToController(inputData);
                    break;
				case KeyState.MultiTap:
					setupMultiTap();
					C4_GameManager.Instance.sceneMode.sendInputDataToController(inputData);
					break;
			}
        }
    }

	void setupMultiTap()
	{
		inputData.multiTapDistance = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
	}

    void setupClickDown()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        clickObject = hit.collider.transform.root.gameObject.GetComponent<C4_Object>();
        inputData.clickObjectID = clickObject.objectAttr;
        inputData.dragObjectID = clickObject.objectAttr;
        inputData.clickDevicePosition = Input.mousePosition;
        inputData.clickPosition = hit.point;
        inputData.dragPosition = hit.point;
        inputData.clickPosition.y = 0;
        inputData.dragPosition.y = 0;
    }

    void setupDrag()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        C4_Object dragObject = hit.collider.transform.root.gameObject.GetComponent<C4_Object>();
        inputData.dragPosition = hit.point;
        inputData.dragPosition.y = 0;
        inputData.dragDevicePosition = Input.mousePosition;
        inputData.dragObjectID = dragObject.objectAttr;
    }

    void setupClickUp()
    {
		startMultiTapDictance = 0;
		inputData.multiTapDistance = 0;
    }
}
