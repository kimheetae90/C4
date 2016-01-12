using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {

	//////////////////////////////////////////////////////////////////////////////
	private static InputHelper _instance;
	public static InputHelper Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(InputHelper)) as InputHelper;
				if (!_instance)
				{
					GameObject container = new GameObject();
					container.name = "InputHelper";
					_instance = container.AddComponent(typeof(InputHelper)) as InputHelper;
				}
			}
			
			return _instance;
		}
	}   
	//////////////////////////////////////////////////////////////////////////////

	
	InputData inputData;
	RaycastHit hit;
	[System.NonSerialized]
	public SceneManager sceneManager;
	public Camera cam;
	
	void Start()
	{
		DontDestroyOnLoad (transform.gameObject);
		inputData = new InputData();
		sceneManager = GameMaster.Instance.GetSceneManager ();
		if (cam == null) 
		{
			cam = Camera.main;
		}
	}
	
	void Update()
	{
		updateKeyState();
		procKeyState();
	}

    public void SetSceneManager(SceneManager _sceneManager)
    {
        sceneManager = _sceneManager;
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
				inputData.keyState = InputData.KeyState.MultiTap;
			}
			else if(Input.GetTouch(1).phase == TouchPhase.Ended)
			{
				inputData.keyState = InputData.KeyState.Up;
			}
		} 
		else 
		{
			if (Input.GetMouseButtonDown(0)) {
				inputData.keyState = InputData.KeyState.Down;
			}
			if (Input.GetMouseButtonUp(0))
			{
				inputData.keyState = InputData.KeyState.Up;
				inputData.clickButton = InputData.ClickButton.Left;
			}
			if (Input.GetMouseButtonDown(1)) {
				inputData.keyState = InputData.KeyState.Down;
			}
			if (Input.GetMouseButtonUp(1))
			{
				inputData.keyState = InputData.KeyState.Up;
				inputData.clickButton = InputData.ClickButton.Right;
			}
		}
		
		
		if (inputData.keyState == inputData.preKeyState && inputData.keyState == InputData.KeyState.Down) 
		{
			inputData.keyState = InputData.KeyState.Press;
		} 
		else if (inputData.keyState == inputData.preKeyState && inputData.keyState == InputData.KeyState.Up) 
		{
			inputData.keyState = InputData.KeyState.Sleep;
		}
	}
	
	private void procKeyState()
	{
		//Action
		if (inputData.keyState != inputData.preKeyState)
		{
			switch (inputData.keyState)
			{
			case InputData.KeyState.Up:
				if(UnityEngine.EventSystems.EventSystem.current != null)
				{
					if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
					{
						setupClickUp();
						sceneManager.DispatchInputData(inputData);
					}
				}
				else
				{
					LogManager.log ("Error : EventSystem이 없음");
				}
				break;
			case InputData.KeyState.Down:
				setupClickDown();
				sceneManager.DispatchInputData(inputData);
				break;
			}
		}
		//Continuos
		else if (inputData.keyState == inputData.preKeyState)
		{
			switch (inputData.keyState)
			{
			case InputData.KeyState.Press:
				setupPress();
				sceneManager.DispatchInputData(inputData);
				break;
			case InputData.KeyState.MultiTap:
				setupMultiTap();
				sceneManager.DispatchInputData(inputData);
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
		if (cam == null) 
		{
			cam = Camera.main;
		}
		Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            Transform ct = hit.collider.transform;
            if (ct != null)
            {
                inputData.downCorrectGameObject = ct.gameObject;
                inputData.downRootGameObject = ct.root.gameObject;
                inputData.clickPosition = hit.point;
            }
        }
		else 
		{
			LogManager.log ("Error : Ray가 Collider와 만나지 않음");
		}
		
		inputData.clickDevicePosition = Input.mousePosition;
	}
	
	void setupPress()
	{
		if (cam == null) 
		{
			cam = Camera.main;
		}
		Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
		if (hit.collider != null) {
			
			inputData.dragPosition = hit.point;
			Transform ct = hit.collider.transform;
			if (ct != null)
			{
				inputData.dragCorrectGameObject = ct.gameObject;
				inputData.dragRootGameObject = ct.root.gameObject;
			}

		} else {
			LogManager.log ("Error : Ray가 Collider와 만나지 않음");
		}
		inputData.dragDevicePosition = Input.mousePosition;
	}
	
	void setupClickUp()
	{
		Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
		if (hit.collider != null) {

			Transform ct = hit.collider.transform;
			if (ct != null)
			{
				inputData.upCorrectGameObject = ct.gameObject;
				inputData.upRootGameObject = ct.root.gameObject;
			}
			
		} else {
			LogManager.log ("Error : Ray가 Collider와 만나지 않음");
		}

		inputData.multiTapDistance = 0;
		inputData.preMultiTapDistance = 0;
	}
}
