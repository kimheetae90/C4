using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  선택한 배를 플레이 하는 것을 관리하는 Manager
///  Input Manager로부터 받은 Data를 dispatchData에서 분석하여 선택된 배에게 행동을 명령한다.
///  aiming : 조준(드래그상태)
///  orderShot : 발포
///  orderMove : 이동
///  setBoatScript : 배 선택(Input Manager가 호출)
///  activeDone : 행동을 완료하여 상태를 reset
///  dispatchData : 전달받은 Data 분석
/// </summary>

public class C4_PlayerController : C4_Controller
{
    [System.NonSerialized]
    public C4_Player selectedBoat;
    [System.NonSerialized]
    bool isAiming;

    enum ePlayerControllerActionState
    {
        None,
        StartAim,
        EndAmi,
        Aming,
        Shot,
        Move,
        Select,
    }

    public override void Start()
    {
        base.Start();
    }

    override public void selectClickObject(GameObject clickGameObject)
    {
        selectedBoat = clickGameObject.GetComponent<C4_Player>();
        addListener(selectedBoat);
    }

    void activeDone()
    {
        isAiming = false;
        notifyEvent("ActiveDone");
        removeListener(selectedBoat);
        selectedBoat = null;
    }

    override public void dispatchData(InputData inputData)
    {
        ePlayerControllerActionState action = ePlayerControllerActionState.None;
        computeActionState(ref inputData, out action);
        ProcState(action, ref inputData);
    }

    private void computeActionState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        action = ePlayerControllerActionState.None;

        if (inputData.keyState == KeyState.Down)
        {
            computeKeyDownState(ref inputData, out action);
        }
        else if (inputData.keyState == KeyState.Up)
        {
            computeKeyUpState(ref inputData, out action);
        }
        else if (inputData.keyState == KeyState.Drag)
        {
            computeKeyDragState(ref inputData, out action);
        }
    }

    private void computeKeyDownState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        bool isEqaulClickObjAndDragObj = inputData.clickObjectID.id == inputData.dragObjectID.id ? true : false;
        bool isSelectObjectTypePlayer = inputData.clickObjectID.isInputTypeTrue(GameObjectInputType.ClickAbleObject);

        if (isAiming == false && isSelectObjectTypePlayer && isEqaulClickObjAndDragObj == true)
        {
            action = ePlayerControllerActionState.Select;
        }
        else
        {
            action = ePlayerControllerActionState.None;
        }
    }

    private void computeKeyUpState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        action = ePlayerControllerActionState.None;

        bool isSelectObjectTypeGround = inputData.clickObjectID.isInputTypeTrue(GameObjectInputType.CameraMoveAbleObject);

        if (isAiming && selectedBoat != null)
        {
            action = ePlayerControllerActionState.Shot;
        }
        else if (isAiming == false && isSelectObjectTypeGround && selectedBoat != null)
        {
            action = ePlayerControllerActionState.Move;
        }
    }

    private void computeKeyDragState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        bool isEqaulClickObjAndDragObj = inputData.clickObjectID.id == inputData.dragObjectID.id ? true : false;
        bool isSelectClickableObject = inputData.clickObjectID.isInputTypeTrue(GameObjectInputType.ClickAbleObject);

        if (isAiming == false && isSelectClickableObject && isEqaulClickObjAndDragObj == false)
        {
            action = ePlayerControllerActionState.StartAim;
        }
        else if (isSelectClickableObject && selectedBoat != null)
        {
            action = ePlayerControllerActionState.Aming;
        }
        else
        {
            action = ePlayerControllerActionState.None;
        }
    }

    private void ProcState(ePlayerControllerActionState action, ref InputData inputData)
    {
        switch (action)
        {
            case ePlayerControllerActionState.None:
                break;
            case ePlayerControllerActionState.Aming:
                notifyEvent("Aming", inputData.dragPosition);
                break;
            case ePlayerControllerActionState.Move:
                notifyEvent("Move", inputData.clickPosition);
                activeDone();
                break;
            case ePlayerControllerActionState.Select:
                activeDone();
                selectClickObject(C4_ManagerMaster.Instance.objectManager.getObject(inputData.clickObjectID).gameObject);
                notifyEvent("Select", selectedBoat.gameObject.transform);
                break;
            case ePlayerControllerActionState.StartAim:
                isAiming = true;
                notifyEvent("StartAim");
                break;
            case ePlayerControllerActionState.Shot:
                notifyEvent("Shot", inputData.dragPosition);
                activeDone();
                break;
        }
    }
}
