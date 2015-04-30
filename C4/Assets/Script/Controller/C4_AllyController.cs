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

public class C4_AllyController : C4_Controller
{

    [System.NonSerialized]
    public C4_Ally selectedAllyUnit;
    bool isAiming;
    const float cameraMoveCheckArea = 5f;


    enum ePlayerControllerActionState
    {
        None,
        StartAim,
        AimCancle,
        Aming,
        Shot,
        Move,
        Select,
    }

    public override void Awake()
    {
        base.Awake();
    }

    override public void selectClickObject(GameObject clickGameObject)
    {
        selectedAllyUnit = clickGameObject.GetComponentInParent<C4_Ally>();
        addListener(selectedAllyUnit);
    }

	public Vector3 calcMissileTargetPoint(Vector3 clickPosition)
	{
		float maxAttackRange = selectedAllyUnit.GetComponent<C4_UnitFeature> ().attackRange;

		Vector3 direction = (selectedAllyUnit.transform.position - clickPosition).normalized;
		float value = Vector3.Distance (selectedAllyUnit.transform.position,clickPosition);
		if (maxAttackRange <= value) 
		{
			value = maxAttackRange;
		}
		value *= 4f;
		Vector3 targetPos = selectedAllyUnit.transform.position + value * direction;
		return targetPos;
	}

    public void activeDone()
    {
        isAiming = false;
        notifyEvent("ActiveDone");
        removeListener(selectedAllyUnit);
        selectedAllyUnit = null;
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

        bool isSelectObjectTypeGround = inputData.clickObjectID.isInputTypeTrue(GameObjectInputType.ToMoveAbleObject);

        if (isAiming && selectedAllyUnit != null)
        {
            action = ePlayerControllerActionState.Shot;
        }
        else if (isAiming == false && isSelectObjectTypeGround && selectedAllyUnit != null && Vector2.Distance(inputData.clickDevicePosition, inputData.dragDevicePosition) < cameraMoveCheckArea)
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
        else if (isEqaulClickObjAndDragObj && isAiming)
        {
            action = ePlayerControllerActionState.AimCancle;
        }
        else if (isAiming && isSelectClickableObject && selectedAllyUnit != null)
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
                notifyEvent("Aming", inputData.dragPosition, selectedAllyUnit, calcMissileTargetPoint(inputData.dragPosition));
                break;
            case ePlayerControllerActionState.Move:
                notifyEvent("Move", inputData.clickPosition);
                activeDone();
                break;
            case ePlayerControllerActionState.Select:
                activeDone();
                selectClickObject(C4_GameManager.Instance.objectManager.getObject(inputData.clickObjectID).gameObject);
                notifyEvent("Select", selectedAllyUnit.gameObject.transform);
                break;
            case ePlayerControllerActionState.StartAim:
                isAiming = true;
                notifyEvent("StartAim");
                break;
            case ePlayerControllerActionState.Shot:
                notifyEvent("Shot", calcMissileTargetPoint(inputData.dragPosition));
                activeDone();
                break;
			case ePlayerControllerActionState.AimCancle:
				isAiming = false;
				notifyEvent("AimCancle");
				break;
        }
    }
}
