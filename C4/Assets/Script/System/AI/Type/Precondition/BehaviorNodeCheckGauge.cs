using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 입력한 수치 만큼 게이지가 충전되어 있는지 확인한다.
/// 게이지 한칸이 100이므로 100은 이동가능한 게이지, 200은 공격가능한 게이지를 확인하는데 사용된다.
/// </summary>
public class BehaviorNodeCheckGauge : BehaviorNodeBasePrecondition
{
	int gauge;
	
	public BehaviorNodeCheckGauge(List<string> _listParams)
		: base(_listParams)
	{
		if(listParams.Count != 1)
		{
			throw new BehaviorNodeException("BehaviorNodeCheckGauge 파라미터의 개수가 맞지 않습니다.");
		}

		gauge = int.Parse(listParams[0]);
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		float currentGage = targetObject.GetComponent<C4_UnitFeature>().gage;
	
		if (currentGage >= gauge)
			return true;
		else
			return false;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeCheckGauge(listParams);
	}
}