using UnityEngine;
using System.Collections;

/// <summary>
///  Input에 대한 것을 처리하는 Manager
///  - click Down, cliking, click Up의 경우로 나누어 처리한다
///  - input에 대한 Data를 수집하여 Camera와 Play Manager에게 전송한다
/// </summary>

public class C4_InputManager : MonoBehaviour {

    InputData inputData;
    public C4_Camera camObject;

    bool isClick;
    RaycastHit hit;
    Object clickObject_temp;
    C4_Object clickObject;
	void Start () {
        isClick = false;
	}
	
	void Update () {

        if (isClick)
        {
            onClick();
            if (inputData.clickObjectID.type == ObjectID.Type.Water)
            {
                camObject.cameraMove(inputData);
            }
            else
            {
                C4_Playmanager.Instance.dispatchData(inputData);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            onClickDown();
        }

        if (isClick&&Input.GetMouseButtonUp(0))
        {
            onClickUp();
            C4_Playmanager.Instance.dispatchData(inputData);
        }

    
    }


    /* 버튼을 눌렀을 때의 Data 처리 */
    void onClickDown()
    {
        isClick = true;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        clickObject_temp = hit.collider.transform.root.gameObject;
        clickObject = clickObject_temp as C4_Object;
        inputData.clickObjectID = clickObject.getObjectID();
        inputData.dragObjectID = clickObject.getObjectID();
        inputData.clickPosition = hit.point;
        inputData.dragPosition = hit.point;
        inputData.clickPosition.y = 0;
        inputData.dragPosition.y = 0;
        inputData.keyState = InputData.KeyState.Down;

        if (inputData.clickObjectID.type == ObjectID.Type.Player)
        {
            C4_Playmanager.Instance.setBoatScript(hit.collider.transform.root.gameObject);
        }
    }



    /* 계속 클릭했을 때(드래그)의 Data 처리 */
    void onClick()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity);
        inputData.dragPosition = hit.point;
        inputData.dragPosition.y = 0;
        clickObject_temp = hit.collider.transform.root.gameObject;
        clickObject = clickObject_temp as C4_Object;
        inputData.dragObjectID = clickObject.getObjectID();
    }



    /* 버튼을 올렸을 때의 Data 처리 */
    void onClickUp()
    {
        inputData.keyState = InputData.KeyState.Up;
        isClick = false;
    }
}
