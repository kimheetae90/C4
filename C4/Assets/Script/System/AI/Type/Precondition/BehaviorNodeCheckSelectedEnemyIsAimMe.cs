using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// select된 선박이 나를 조준 중 인지 판단. 방향은 8방향으로 분할 하여 체크. 조준 중이면 TRUE 아니면 FALSE
/// </summary>
public class BehaviorNodeCheckSelectedEnemyIsAimMe : BehaviorNodeBasePrecondition
{
	public BehaviorNodeCheckSelectedEnemyIsAimMe(List<string> _listParams)
		: base(_listParams)
	{
		if(listParams.Count != 0)
		{
			throw new BehaviorNodeException("BehaviorNodeCheckSelectedEnemyIsAimMe 파라미터의 개수가 맞지 않습니다.");
		}
	}
	
	override public bool traversalNode(GameObject targetObject)
	{
		C4_Ally selectedUnit = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally)
			.GetComponent<C4_AllyController>().selectedAllyUnit;

		if (selectedUnit == null)
			return false;

        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        if (behaviorComponent == null) throw new BehaviorNodeException("BehaviorNodeCheckSelectedEnemyIsAimMe AI Target에 BehaviorComponent 컴퍼넌트가 없습니다.");
        
        //Vector3 pos = selectedUnit.getCurrentAimPos();

        //Vector3 aimDirection = selectedUnit.transform.position - pos;
        
        //aimDirection.Normalize();
        
        Vector3 targetDirection = targetObject.transform.position - selectedUnit.transform.position;
        
        targetDirection.Normalize();

        //float dot = Vector3.Dot(aimDirection, targetDirection);

//        if (dot > Mathf.Cos(45 * 180 / Mathf.PI) && dot > Mathf.Cos(90 * 180 / Mathf.PI))
//        {
//            behaviorComponent.cachedStruct.SetAimingSelectedObject(selectedUnit);
//            return true;
//        }
//        else
//        {
//            behaviorComponent.cachedStruct.ClearAimingSelectedObject();
//        }


		return false;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeCheckSelectedEnemyIsAimMe(listParams);
	}
}