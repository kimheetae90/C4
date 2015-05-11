using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 공격을 회피 또는 적과 나 사이에 아군이 있을경우 아군 공격을 방지 하기 위한 움직임이다.
/// BehaviorNodeCheckObjectConditionIsAttackPrecondition에서 얻은 데이터를 통해 공격 방향과 수직이 되는 방향으로 입력한 거리만큼 이동한다.
/// 입력한 수치는 선박의 능력으로 지정된 이동거리와 곱해진 거리이다. 
/// </summary>
public class BehaviorNodeMoveForAvoid : BehaviorNodeBaseAction
{
	// Use this for initialization
	float range;
	
	public BehaviorNodeMoveForAvoid(List<string> listParams)
		: base(listParams)
	{
		if (listParams.Count != 1)
		{
			throw new BehaviorNodeException("BehaviorNodeMoveForAvoid 파라미터의 개수가 맞지 않습니다.");
		}
		
		range = float.Parse(listParams[0]);
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		C4_Enemy charComponent = targetObject.GetComponent<C4_Enemy>();

        C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();
        
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

		if (charComponent == null || behaviorComponent == null || unitFeature == null)
		{
			throw new BehaviorNodeException("BehaviorNodeMoveForAvoid AI Target에 해당 컴퍼넌트가 없습니다.");
		}

        C4_Ally obj = behaviorComponent.cachedStruct.checkedSelectedObject;

		if (obj == null)
			return false;

        Vector3 direction = obj.transform.position - targetObject.transform.position;

        direction.Normalize();

        Vector3 moveDirection = Vector3.Cross(direction, targetObject.transform.forward);

        moveDirection.Normalize();

        moveDirection *= (int)(unitFeature.moveRange * range);
		
		charComponent.move(obj.transform.position + moveDirection);
		
		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeMoveForAvoid(listParams);
	}
	
}