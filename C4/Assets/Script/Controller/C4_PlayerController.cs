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
    public C4_Player ourBoat; //시작 시 배 불러오는 부분(나중에 지울것)
    [System.NonSerialized]
    public C4_Player selectedBoat;
    [System.NonSerialized]
    bool isAiming;
    
    enum ePlayerControllerActionState
    {
        None,
        StartAim,
        Aming,
        Shot,
        Move,
        Select,
    }

    public override void Start()
    {
        base.Start();

        ourBoat = FindObjectOfType(typeof(C4_Player)) as C4_Player;
        ourBoat.objectID.id = C4_ManagerMaster.Instance.objectManager.currentObjectCode++;
        ourBoat.objectID.type = GameObjectType.Player;
        C4_ManagerMaster.Instance.objectManager.addObjectToAll(ourBoat);
    }


/*
    / * 조준하고 있는 방향으로 회전하고 UI를 출력할 함수 * /
    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;
        selectedBoat.turn(clickPosition);
        //  playerUIScript.aiming(clickPosition);
    }


    / * 발포하고 상태를 초기화할 함수 * /
    void orderShot(Vector3 shotDirection)
    {
        selectedBoat.shot(shotDirection);
        activeDone();
    }


    / * 움직임을 명령할 함수 * /
    void orderMove(Vector3 toMove)
    {
        selectedBoat.move(toMove);
        selectedBoat.turn(toMove);
        activeDone();
    }*/


    /* 배를 선택하는 함수 */

    override public void selectClickObject(GameObject clickGameObject)
    {
        selectedBoat = clickGameObject.GetComponent<C4_Player>();
        //playerUI.transform.position = clickGameObject.transform.position;

        addListener(selectedBoat);
    }

    /* 선택 정보를 초기화 */
    void activeDone()
    {
        isAiming = false;
        notifyEvent("ActiveDone");
        removeListener(selectedBoat);
        selectedBoat = null;
    }

    /* InputManager로부터 전해받은 InputData를 분석하고 행동을 명령하는 함수 */
    override public void dispatchData(InputData inputData)
    {
        if (selectedBoat == null) return;

        ePlayerControllerActionState action = ePlayerControllerActionState.None;
        computeActionState(ref inputData, out action);
        ProcState(action, ref inputData);
        updateAimState(ref inputData);
    }

    private void computeActionState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        if (inputData.keyState == InputData.KeyState.Down)
        {
            computeKeyDownState(ref inputData, out action);
        }
        else
        {
            computeKeyUpState(ref inputData, out action);
        }
    }

    private void computeKeyDownState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        bool isClickObjAndDragObj = inputData.clickObjectID.id == inputData.dragObjectID.id ? true : false;
        bool isSelectObjectTypePlayer = inputData.clickObjectID.type == GameObjectType.Player ? true : false;

        if (isAiming)
        {
            action = ePlayerControllerActionState.Aming;
        }
        else if (isAiming == false && isSelectObjectTypePlayer && isClickObjAndDragObj == false)
        {
            action = ePlayerControllerActionState.StartAim;
        }
        else 
        {
            action = ePlayerControllerActionState.None;
        }
    }

    private void computeKeyUpState(ref InputData inputData, out ePlayerControllerActionState action)
    {
        bool isSelectObjectTypeGround = inputData.clickObjectID.type == GameObjectType.Ground ? true : false;

        if (isAiming)
        {
            action = ePlayerControllerActionState.Shot;
        }
        else if (isAiming == false && isSelectObjectTypeGround)
        {
            action = ePlayerControllerActionState.Move;
        }
        else if (isAiming == false && selectedBoat != null)
        {
            action = ePlayerControllerActionState.Select;
        }
        else
        {
            action = ePlayerControllerActionState.None;
        }
    }

    private void updateAimState(ref InputData inputData)
    {
        bool isEqualClickObjAndDragObj = inputData.clickObjectID.id == inputData.dragObjectID.id ? true : false;
        bool isSelectObjectTypePlayer = inputData.clickObjectID.type == GameObjectType.Player ? true : false;

        if (isEqualClickObjAndDragObj)
        {
            isAiming = false;
        }
        else if (isEqualClickObjAndDragObj == false && isSelectObjectTypePlayer)
        {
            isAiming = true;
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
                notifyEvent("Select",selectedBoat.transform.position);
                break;
            case ePlayerControllerActionState.StartAim:
                notifyEvent("StartAim");
                break;
            case ePlayerControllerActionState.Shot:
                notifyEvent("Shot", inputData.dragPosition);
                activeDone();
                break;
        }
    }
}
