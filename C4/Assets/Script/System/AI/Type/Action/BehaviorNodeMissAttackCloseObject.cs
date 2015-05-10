using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeMissAttackCloseObject : BehaviorNodeBaseAction
{
	Vector3 targetVector;
	float errorRange;

	public BehaviorNodeMissAttackCloseObject(List<string> listParams)
		: base(listParams)
	{
		if (listParams.Count != 0)
		{
            throw new BehaviorNodeException(System.String.Format("{0} 파라미터의 개수가 맞지 않습니다. {1}",
                              this.GetType().Name, listParams.Count));
		}

		//errorRange = float.Parse(listParams [0]);
		errorRange = 10.0f;
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		Debug.Log ("close miss attack traversal");

		C4_Unit unitComponent = targetObject.GetComponent<C4_Unit>();
		C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();
		C4_FindObjectInRadiousCollision findComponent = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();
		
		if (unitComponent == null || findComponent == null || unitFeature == null)
		{
			throw new BehaviorNodeException("BehaviorNodeMissAttackCloseObject AI Target에 해당 컴퍼넌트가 없습니다.");
		}
		
		C4_Object obj = findComponent.getNearestObject();
		
		if (obj == null) return false;

		targetVector = -obj.transform.position;

		targetVector.x += Random.Range(-errorRange , errorRange);
		targetVector.z += Random.Range(-errorRange , errorRange);

		unitComponent.shot (targetVector);
		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeMissAttackCloseObject(listParams);
	}
	
}