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
    RaycastHit clickHit;
    RaycastHit dragHit;

	// Use this for initialization
	void Start () {
        cameraScript = c4_camera.GetComponent<C4_Camera>();
        playManagerScript = c4_playManager.GetComponent<C4_Playmanager>();
        clickFlag = false;
        isClick = false;
	}
	
	// Update is called once per frame
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
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out clickHit, Mathf.Infinity, (1 << 4));
        inputData.clickPosition = clickHit.point;
        inputData.dragPosition = clickHit.point;
        inputData.clickPosition.y = 0;
        inputData.dragPosition.y = 0;

        if (clickHit.collider.CompareTag("water"))
        {
            inputData.clickObjectType = InputData.ObjectType.WATER;            
        }
        else if (clickHit.collider.CompareTag("boat"))
        {
            playManagerScript.selectedBoat = clickHit.collider.transform.root.gameObject;;
            inputData.clickObjectType = InputData.ObjectType.BOAT;
        }
    }

    void onClick()
    {
        clickFlag = true;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out dragHit, Mathf.Infinity, (1 << 4));
        inputData.dragPosition = dragHit.point;
        inputData.dragPosition.y = 0;
        inputData.keyState = InputData.KeyState.DRAG;

        if (dragHit.collider.CompareTag("water"))
        {
            inputData.dragObjectType = InputData.ObjectType.WATER;
        }
        else if (dragHit.collider.CompareTag("boat"))
        {
            inputData.dragObjectType = InputData.ObjectType.BOAT;
        }
    }

    void onClickUp()
    {
        isClick = false;        
        inputData.keyState = InputData.KeyState.UP;
    }

}
