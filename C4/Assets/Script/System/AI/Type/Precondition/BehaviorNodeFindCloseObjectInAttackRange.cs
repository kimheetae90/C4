using UnityEngine;
using System;
using System.Collections.Generic;
/// <summary>
/// 입력한 타입의 객체를 자신의 공격 범위내에서 찾아는다 없으면 false, 있으면 true이며 찾은 선박들을 스택에 모두 저장
/// </summary>
public class BehaviorNodeFindCloseObjectInAttackRange : BehaviorNodeBasePrecondition
{
	GameObjectType type;

	public BehaviorNodeFindCloseObjectInAttackRange(List<string> _listParams)
		: base(_listParams)
	{
		if(listParams.Count != 1)
		{
			throw new BehaviorNodeException("BehaviorNodeFindCloseObjectInAttackRange 파라미터의 개수가 맞지 않습니다.");
		}

		type = (GameObjectType)Enum.Parse(typeof(GameObjectType), listParams[0]);
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

		C4_FindObjectInRadiousCollision findComponent = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();
		
        C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();

		float attackRange = unitFeature.attackRange;

        if (findComponent == null || unitFeature == null || behaviorComponent == null)
		{
			throw new BehaviorNodeException("BehaviorNodeFindCloseObjectInAttackRange AI Target에 해당 컴퍼넌트가 없습니다.");
		}

		behaviorComponent.cachedStruct.objectsInFireRange.Clear();

        bool bRet = findComponent.FindObjectsInRadious(attackRange, type);
  
        behaviorComponent.cachedStruct.objectsInFireRange = findComponent.getLatestFindObjects();

        return bRet;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeFindCloseObjectInAttackRange(listParams);
	}

}
