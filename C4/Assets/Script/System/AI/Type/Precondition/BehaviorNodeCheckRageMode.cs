using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 자신의 상태가 레이지 모드인지 확인하는 노드. 레이지 모드라면 true 아니라면 false이다.
/// </summary>
public class BehaviorNodeCheckRageMode : BehaviorNodeBasePrecondition
{
	int gauge;
	
	public BehaviorNodeCheckRageMode(List<string> listParams)
		: base(listParams)
	{
		if(listParams.Count != 0)
		{
            throw new BehaviorNodeException("BehaviorNodeCheckRageMode 파라미터의 개수가 맞지 않습니다.");
		}
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature> ();

		float currentRage = unitFeature.rageGage;
		float rageMode = unitFeature.rageFullGage;

		if (currentRage >= rageMode)
			return true;
		else
			return false;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeCheckRageMode(listParams);
	}
}