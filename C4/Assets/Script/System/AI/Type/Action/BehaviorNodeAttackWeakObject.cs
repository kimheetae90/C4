using UnityEngine;
using System.Collections.Generic;

//not yet
public class BehaviorNodeAttackWeakObject : BehaviorNodeBaseAction
{
    float shotDelayTime = 1.5f;

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
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        C4_BehaviorActionFunc actionFunc = targetObject.GetComponent<C4_BehaviorActionFunc>();

        if (behaviorComponent == null || actionFunc == null)
        {
            throw new BehaviorNodeException("BehaviorNodeAttackObject AI Target에 해당 컴퍼넌트가 없습니다.");
        }

        List<C4_Object> list = behaviorComponent.cachedStruct.objectsInFireRange;

        if (list.Count == 0)
        {
            Debug.Log("nearest object is null");
            return false;
        }

        C4_Object minObject = list[0];
        int minHP = getHP(list[0]);

        for (int i = 1; i < list.Count; ++i)
        {
            int curHP = getHP(list[i]);

            if (curHP != -1 && minHP > curHP)
            {
                minHP = curHP;
                minObject = list[i];
            }
        }

        Vector3 targetVector = (minObject.transform.position);

        actionFunc.AttackTargetPos(targetVector);

        return true;
	}

    private int getHP(C4_Object obj)
    {
        C4_UnitFeature feature = obj.GetComponent<C4_UnitFeature>();

        if (feature != null)
        {
            return feature.hp;
        }

        return -1;
    }
	
	override public object Clone()
	{
		return new BehaviorNodeAttackWeakObject(listParams);
	}
	
}