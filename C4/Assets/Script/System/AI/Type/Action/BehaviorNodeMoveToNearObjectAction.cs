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
        BehaviorComponent behaviorComponent = targetObject.GetComponent<BehaviorComponent>();

        C4_BehaviorActionFunc actionFunc = targetObject.GetComponent<C4_BehaviorActionFunc>();

        C4_UnitFeature unitFeature = targetObject.GetComponent<C4_UnitFeature>();

        if (behaviorComponent == null || actionFunc == null || unitFeature == null)
        {
            throw new BehaviorNodeException("BehaviorNodeMoveToNearObjectAction AI Target에 해당 컴퍼넌트가 없습니다.");
        }

        C4_Object obj = behaviorComponent.cachedStruct.getNearestObject();

        if (obj == null)
        {
            return false;
        }

        actionFunc.MoveTo(obj);

        return true;
    }

    override public object Clone()
    {
        return new BehaviorNodeMoveToNearObjectAction(listParams);
    }
  
}