using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeMoveToNearObjectAction : BehaviorNodeBaseAction
{
    // Use this for initialization
    float velocity;

    public BehaviorNodeMoveToNearObjectAction(List<string> listParams)
        : base(listParams)
    {
        if (listParams.Count != 1)
        {
            throw new BehaviorNodeException("BehaviorNodeFindObjectPrecondition 파라미터의 개수가 맞지 않습니다.");
        }

        velocity = float.Parse(listParams[0]);
    }

    override public bool traversalNode(GameObject targetObject)
    {
        C4_Unit unitComponent = targetObject.GetComponent<C4_Unit>();
        C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();
		C4_FindObjectInRadiousCollision findComponent = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();

		if (unitComponent == null || findComponent == null || unitFeature == null)
        {
			if(unitComponent == null) Debug.Log ("unitComponent is null");
			if(findComponent == null) Debug.Log ("findComponent is null");
			if(unitFeature == null) Debug.Log ("unitFeature is null");
            throw new BehaviorNodeException("BehaviorNodeMoveToNearObjectAction AI Target에 해당 컴퍼넌트가 없습니다.");
        }
        C4_Object obj = findComponent.getNearestObject();

        if (obj == null) return false;

        if (velocity != 0)
        {
           unitFeature.moveSpeed = (int)velocity;
        }
            
        unitComponent.move(obj.transform.position);
        
        return true;
    }

    override public object Clone()
    {
        return new BehaviorNodeMoveToNearObjectAction(listParams);
    }
  
}