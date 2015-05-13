using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// BehaviorNodeFindObjectInAttackRangePrecondition에서 얻은 데이터의 오브젝트를  공격한다.
/// </summary>
public class BehaviorNodeAttackObject : BehaviorNodeBaseAction
{
	
	public BehaviorNodeAttackObject(List<string> listParams)
		: base(listParams)
	{
		if (listParams.Count != 0)
		{
			throw new BehaviorNodeException("BehaviorNodeAttackObject 파라미터가 존재할 수 없습니다.");
		}
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        C4_BehaviorActionFunc actionFunc = targetObject.GetComponent<C4_BehaviorActionFunc>();

        if (behaviorComponent == null || actionFunc == null)
        {
            throw new BehaviorNodeException("BehaviorNodeAttackObject AI Target에 해당 컴퍼넌트가 없습니다.");
        }

        C4_Object obj = behaviorComponent.cachedStruct.getNearestObject();

        if (obj == null)
        {
            Debug.Log("nearest object is null");
            return false;
        }

        Vector3 targetVector = (obj.transform.position);

        actionFunc.AttackTargetPos(targetVector);

        return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeAttackObject(listParams);
	}
	
}