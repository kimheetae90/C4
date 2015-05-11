using UnityEngine;
using System;
using System.Collections.Generic;
/// <summary>
/// BehaviorNodeCheckEnemyIsAimMe 확인한 선박이 상태가 변하는지 확인하는 노드.
/// 상태중에서도 SHOOT상태가 되면 TRUE, 다른 상태는 FALSE를 반환한다.
/// 체크 시간은 1초이다. 1초동안 변하지 않아도 FALSE를 반환한다,.
/// </summary>
public class BehaviorNodeCheckEnemyStateIsShoot : BehaviorNodeBasePrecondition
{
	public BehaviorNodeCheckEnemyStateIsShoot(List<string> _listParams)
		: base(_listParams)
	{
		if(listParams.Count != 0)
		{
			throw new BehaviorNodeException("BehaviorNodeCheckEnemyStateIsShoot 파라미터의 개수가 맞지 않습니다.");
		}
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        if (behaviorComponent == null) throw new BehaviorNodeException("BehaviorNodeCheckSelectedEnemyIsAimMe AI Target에 BehaviorComponent 컴퍼넌트가 없습니다.");

        C4_Ally obj = behaviorComponent.cachedStruct.checkedSelectedObject;

        if (obj == null) return false;

        DateTime lastShotTime = obj.getLastShotTime();
        DateTime curTime = DateTime.Now;

        if (curTime.Subtract(lastShotTime).TotalSeconds > 1.0f)
        {
            return false;
        }

        return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeCheckEnemyStateIsShoot(listParams);
	}
}