using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeMoveToNearObjectAction : BehaviorNodeBaseAction
{
    // Use this for initialization
    float velocity;

    public BehaviorNodeMoveToNearObjectAction(List<string> listParams)
        : base(listParams)
    {
        if (listParams.Count < 1)
        {
            throw new BehaviorNodeException("BehaviorNodeFindObjectPrecondition 파라미터의 개수가 맞지 않습니다.");
        }

        velocity = float.Parse(listParams[0]);
    }

    override public bool traversalNode()
    {
        C4_Character charComponent = targetObject.GetComponent<C4_Character>();
        C4_BoatFeature boatFeature = targetObject.GetComponent<C4_BoatFeature>();
        C4_FindObjectInRadiousCollision findComponent = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();

        if (charComponent == null || findComponent == null || boatFeature == null)
        {
            throw new BehaviorNodeException("BehaviorNodeMoveToNearObjectAction AI Target에 해당 컴퍼넌트가 없습니다.");
        }

        C4_Object obj = findComponent.getNearestObject();

        if (obj == null) return false;

        boatFeature.moveSpeed = (int)velocity;
        charComponent.move(obj.transform.position);
        
        return true;
    }

    override public object Clone()
    {
        return new BehaviorNodeMoveToNearObjectAction(listParams);
    }
  
}