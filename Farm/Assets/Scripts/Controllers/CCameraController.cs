using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CCameraController : Controller
{
    public Camera camera;

    Vector3 clickPos;
    Vector3 dragPos;

    public int leftEndXPos;
    public int rightEndXPos;

    void Awake()
    {
    }

    protected override void Start()
    {
        base.Start();
    }
    
    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_CameraMove:
                CameraMove((InputData)_gameMessage.Get("inputData"));
                break;
        }
    }

    

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////




    /// <summary>
    /// 카메라를 움직이는 함수.
    /// _inputData에서 마우스의 ClickPosition과 DragPosition을 받아서
    /// 두 position의 거리가 일정 거리(0.5) 이상이면 현재 카메라의 위치에서 clickposition의 x좌표에서 dragposition의 x좌표를 뺀만큼 움직인다.
    /// 카메라의 이동속도는 clickposition과 dragposition의 차이의 10배이다.
    /// targetposition이 오른쪽이나 왼쪽 끝일경우 고정시킨다. (rightEndXPos,leftEndXPos)
    /// </summary>
    /// <param name="_inputData"></param>
    void CameraMove(InputData _inputData)
    {
        clickPos = new Vector3(_inputData.clickPosition.x, 0, 0);
        dragPos = new Vector3(_inputData.dragPosition.x, 0, 0);

        if (Vector3.Distance(clickPos, dragPos) > 0.5f)
        {
            Vector3 targetPos = new Vector3(camera.transform.position.x + (clickPos.x - dragPos.x), camera.transform.position.y, camera.transform.position.z);

            if (targetPos.x > rightEndXPos) {
                targetPos.x = rightEndXPos;
            }
            else if (targetPos.x < leftEndXPos) {
                targetPos.x = leftEndXPos;
            }
            camera.GetComponent<CMove>().SetTargetPos(targetPos);
            camera.GetComponent<CMove>().SetMoveSpeed(Mathf.Abs(clickPos.x - dragPos.x) * 10);
            camera.GetComponent<CMove>().StartMove();
        }
    }
    
}
