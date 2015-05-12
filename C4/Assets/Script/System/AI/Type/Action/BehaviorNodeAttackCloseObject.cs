using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeAttackCloseObject : BehaviorNodeBaseAction
{
	Vector3 targetVector;

	public BehaviorNodeAttackCloseObject(List<string> listParams)
		: base(listParams)
	{
		if (listParams.Count != 0)
		{
			throw new BehaviorNodeException("BehaviorNodeAttackCloseObject 파라미터가 존재할 수 없습니다.");
		}
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		C4_Unit unitComponent = targetObject.GetComponent<C4_Unit>();

		BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

		C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();

		if (unitComponent == null || unitFeature == null)
		{
			throw new BehaviorNodeException("BehaviorNodeAttackCloseObject AI Target에 해당 컴퍼넌트가 없습니다.");
		}
		
		C4_Object obj = behaviorComponent.cachedStruct.getNearestObject();
		
		if (obj == null) {
			Debug.Log ("nearest object is null");
			return false;
		}
		targetVector = (obj.transform.position);

        unitComponent.turn (targetVector);
		
        unitComponent.shot (targetVector);

        C4_EnemyAttackUI ui = targetObject.GetComponentInChildren<C4_EnemyAttackUI>();

        if(ui != null)
        {
            ui.showUI();
        }
		
		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeAttackCloseObject(listParams);
	}
	
}