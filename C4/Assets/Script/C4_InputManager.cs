using UnityEngine;
using System.Collections;

public class C4_InputManager : MonoBehaviour {

    public GameObject c4_camera;
    public GameObject c4_playManager;

    C4_Camera cameraScript;
    C4_Playmanager playManagerScript;
    InputData inputData;
    bool isClick;
    bool clickFlag;
    RaycastHit hit;

	void Start () {
        cameraScript = c4_camera.GetComponent<C4_Camera>();
        playManagerScript = c4_playManager.GetComponent<C4_Playmanager>();
        clickFlag = false;
        isClick = false;
	}
	
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            onClickDown();
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            onClickUp();
        }

        else if (isClick)
        {
            onClick();
        }

        if (clickFlag)
        {
            clickFlag = false;
            if (inputData.clickObjectType == InputData.ObjectType.WATER)
            {
                cameraScript.cameraMove(inputData);
            }
            playManagerScript.dispatchData(inputData);
        }
    
    }

    void onClickDown()
    {
        clickFlag = true;
        isClick = true;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, (1 << 4));
        inputData.clickPosition = hit.point;
        inputData.dragPosition = hit.point;
        inputData.clickPosition.y = 0;
        inputData.dragPosition.y = 0;

        checkObjectType(ref inputData.clickObjectType);
        if (hit.collider.CompareTag("ally"))
        {
            if (hit.collider.transform.root.gameObject.GetComponent<C4_Boat>().isActive)
            {
                playManagerScript.selectedBoat = hit.collider.transform.root.gameObject;
            }
        }
    }

    void onClick()
    {
        clickFlag = true;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, (1 << 4));
        inputData.dragPosition = hit.point;
        inputData.dragPosition.y = 0;
        inputData.keyState = InputData.KeyState.DRAG;
        checkObjectType(ref inputData.dragObjectType);
    }

    void onClickUp()
    {
        isClick = false;        
        inputData.keyState = InputData.KeyState.UP;
    }

    void checkObjectType(ref InputData.ObjectType type)
    {
        if (hit.collider.CompareTag("water"))
        {
            type = InputData.ObjectType.WATER;
        }
        else if (hit.collider.CompareTag("ally"))
        {
            type = InputData.ObjectType.BOAT;
        }
    }

}
