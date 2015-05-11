using UnityEngine;
using System;
using System.Collections.Generic;
/// <summary>
/// BehaviorNodeFindObjectInAttackRange에서 정한 적과 나 사이에 적이 있는가
/// </summary>
public class BehaviorNodeIsThereAllyBetweenTargetAndMe : BehaviorNodeBasePrecondition
{
	//GameObjectType type;
	
	public BehaviorNodeIsThereAllyBetweenTargetAndMe(List<string> _listParams)
		: base(_listParams)
	{
		if(listParams.Count != 1)
		{
			throw new BehaviorNodeException("BehaviorNodeIsThereAllyBetweenTargetAndMe 파라미터의 개수가 맞지 않습니다.");
		}

		//type = (GameObjectType)Enum.Parse(typeof(GameObjectType), listParams[0]);
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
        C4_Object me = targetObject.GetComponent<C4_Object>();

        C4_FindObjectInRadiousCollision findComponent = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();

        C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();

        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        behaviorComponent.cachedStruct.betweenObjectInFireObjects.Clear();

        if (me == null) throw new BehaviorNodeException("BehaviorNodeIsThereAllyBetweenTargetAndMe AI Target에 C4_Object 컴퍼넌트가 없습니다.");

        if (behaviorComponent == null) throw new BehaviorNodeException("BehaviorNodeIsThereAllyBetweenTargetAndMe AI Target에 behaviorComponent 컴퍼넌트가 없습니다.");

        if (findComponent == null) throw new BehaviorNodeException("BehaviorNodeIsThereAllyBetweenTargetAndMe AI Target에 C4_FindObjectInRadiousCollision 컴퍼넌트가 없습니다.");

        if (unitFeature == null) throw new BehaviorNodeException("BehaviorNodeIsThereAllyBetweenTargetAndMe AI Target에 C4_UnitFeature 컴퍼넌트가 없습니다.");

		List<C4_Object> list = behaviorComponent.cachedStruct.objectsInFireRange;

        if (list.Count == 0) return false;

        Transform thisTransform = targetObject.transform;

        for (int i = 0; i < list.Count; ++i )
        {
            Vector3 fwdDirection = list[i].transform.position - thisTransform.position;

            RaycastHit hitInfo;

            if (Physics.Raycast(thisTransform.position, fwdDirection, out hitInfo, unitFeature.attackRange))
            {
                if(hitInfo.collider.gameObject.name != list[i].gameObject.name)
                {
                    C4_Object obj = hitInfo.collider.gameObject.GetComponent<C4_Object>();

                    if (obj != null && obj.isType(me.objectAttr.type))
                    {
                        behaviorComponent.cachedStruct.betweenObjectInFireObjects.Add(obj);
                    }
                }
            }
        }

        return behaviorComponent.cachedStruct.betweenObjectInFireObjects.Count > 0 ? true : false;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeIsThereAllyBetweenTargetAndMe(listParams);
	}
}