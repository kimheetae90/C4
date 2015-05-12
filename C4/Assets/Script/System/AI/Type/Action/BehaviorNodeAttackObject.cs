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
		//Debug.Log ("attack object");
		C4_Unit unitComponent = targetObject.GetComponent<C4_Unit>();

        C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();
		
		if (unitComponent == null || unitFeature == null)
		{
			throw new BehaviorNodeException("BehaviorNodeAttackObject AI Target에 해당 컴퍼넌트가 없습니다.");
		}

        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        if (behaviorComponent == null) throw new BehaviorNodeException("BehaviorNodeCheckSelectedEnemyIsAimMe AI Target에 BehaviorComponent 컴퍼넌트가 없습니다.");

        List<C4_Object> objectsInFireRange = behaviorComponent.cachedStruct.objectsInFireRange;

        if (objectsInFireRange.Count == 0) return true;

        unitComponent.shot(objectsInFireRange[0].transform.position);

        C4_EnemyAttackUI ui = targetObject.GetComponent<C4_EnemyAttackUI>();

        if (ui != null)
        {
            ui.showUI();
        }

		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeAttackObject(listParams);
	}
	
}