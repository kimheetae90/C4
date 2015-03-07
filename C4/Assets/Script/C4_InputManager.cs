using UnityEngine;
using System.Collections;

/// <summary>
///  Input에 대한 것을 처리하는 Manager
///  - click Down, cliking, click Up의 경우로 나누어 처리한다
///  - input에 대한 Data를 수집하여 Camera와 Play Manager에게 전송한다
/// </summary>

public class C4_InputManager : MonoBehaviour {

    public GameObject c4_camera;
    public GameObject c4_playManager;

    C4_Camera cameraScript;
    C4_Playmanager playManagerScript;
    InputData inputData;

    bool isClick;
    RaycastHit hit;

	void Start () {
        cameraScript = c4_camera.GetComponent<C4_Camera>();
        playManagerScript = c4_playManager.GetComponent<C4_Playmanager>();
        isClick = false;
	}
	
	void Update () {

        if (isClick)
        {
            onClick();
            if (inputData.clickObjectType == InputData.ObjectType.WATER)
            {
                cameraScript.cameraMove(inputData);
            }
            else
            {
                playManagerScript.dispatchData(inputData);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            onClickDown();
        }

        if (isClick&&Input.GetMouseButtonUp(0))
        {
            onClickUp();
            playManagerScript.dispatchData(inputData);
        }

    
    }


    /* 버튼을 눌렀을 때의 Data 처리 */
    void onClickDown()
    {
        isClick = true;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        inputData.clickPosition = hit.point;
        inputData.dragPosition = hit.point;
        inputData.clickPosition.y = 0;
        inputData.dragPosition.y = 0;
        inputData.keyState = InputData.KeyState.DRAG;

        checkObjectType(ref inputData.clickObjectType);
        if (hit.collider.CompareTag("ally"))
        {
            if (hit.collider.transform.root.gameObject.GetComponent<C4_Boat>().canMove)
            {
                playManagerScript.SendMessage("setBoatScript",hit.collider.transform.root.gameObject);
            }
        }
    }



    /* 계속 클릭했을 때(드래그)의 Data 처리 */
    void onClick()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        inputData.dragPosition = hit.point;
        inputData.dragPosition.y = 0;
        checkObjectType(ref inputData.dragObjectType);
    }



    /* 버튼을 올렸을 때의 Data 처리 */
    void onClickUp()
    {
        inputData.keyState = InputData.KeyState.UP;
        isClick = false;
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
