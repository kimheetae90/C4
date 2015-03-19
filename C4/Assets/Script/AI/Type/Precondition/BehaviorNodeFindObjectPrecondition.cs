using UnityEngine;
using System;
using System.Collections.Generic;

public class BehaviorNodeFindObjectPrecondition : BehaviorNodeBasePrecondition 
{
    GameObjectType type;
    float radious;

    public BehaviorNodeFindObjectPrecondition(List<string> _listParams)
        : base(_listParams)
    {
        
        if(listParams.Count < 2)
        {
            throw new BehaviorNodeException("BehaviorNodeFindObjectPrecondition 파라미터의 개수가 맞지 않습니다.");
        }

        type = (GameObjectType)Enum.Parse(typeof(GameObjectType), listParams[0]);
        radious = float.Parse(listParams[1]);
    }

    override public bool traversalNode()
    {
        C4_FindObjectInRadiousCollision component = targetObject.GetComponent<C4_FindObjectInRadiousCollision>();

        if (component == null) return false;

        List<C4_Object> list = component.FindTargetObject(radious, type);

        if (list != null && list.Count > 0)
        {
            return true;
        }

        return false;
    }

    override public object Clone()
    {
        return new BehaviorNodeFindObjectPrecondition(listParams);
    }
}