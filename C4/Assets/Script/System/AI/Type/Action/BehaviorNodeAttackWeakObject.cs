using UnityEngine;
using System.Collections.Generic;

//not yet
public class BehaviorNodeAttackWeakObject : BehaviorNodeBaseAction
{
	
	public BehaviorNodeAttackWeakObject(List<string> listParams)
		: base(listParams)
	{
		if (listParams.Count != 0)
		{
			throw new BehaviorNodeException("BehaviorNodeAttackWeakObject 파라미터가 존재할 수 없습니다.");
		}
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		Debug.Log ("attack weak object");

		C4_Unit unitComponent = targetObject.GetComponent<C4_Unit>();
		C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();
		C4_FindObjectInRadiousCollision findComponent = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();
		
		if (unitComponent == null || unitFeature == null || findComponent == null)
		{
			throw new BehaviorNodeException("BehaviorNodeAttackWeakObject AI Target에 해당 컴퍼넌트가 없습니다.");
		}
		
		C4_Object obj = findComponent.getNearestObject();
		
		if (obj == null) return false;
		
		unitComponent.shot (obj.transform.position);

        C4_EnemyAttackUI ui = targetObject.GetComponent<C4_EnemyAttackUI>();

        if (ui != null)
        {
            ui.showUI();
        }

		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeAttackWeakObject(listParams);
	}
	
}