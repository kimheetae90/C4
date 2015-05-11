using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// BehaviorNodeFindObjectInAttackRangePrecondition에서 얻은 데이터의 오브젝트를 입력한 수치의 각도만큼 어긋난 공격을한다.
/// </summary>
public class BehaviorNodeMissAttackObject : BehaviorNodeBaseAction
{
	
	float errorRange;
	
	public BehaviorNodeMissAttackObject(List<string> listParams)
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
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();
		C4_Unit unitComponent = targetObject.GetComponent<C4_Unit>();
		C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();
		
		if (unitComponent == null || unitFeature == null)
		{
			throw new BehaviorNodeException("BehaviorNodeMissAttackCloseObject AI Target에 해당 컴퍼넌트가 없습니다.");
		}

        List<C4_Object> list = behaviorComponent.cachedStruct.objectsInFireRange;

        if (list.Count == 0) return true;

        Vector3 targetVector;

        targetVector = -list[0].transform.position;
		
		targetVector.x += Random.Range(-errorRange , errorRange);
		targetVector.z += Random.Range(-errorRange , errorRange);
		
		unitComponent.shot (targetVector);
		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeMissAttackObject(listParams);
	}
	
}