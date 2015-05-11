using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 입력한 타입의 객체를 입력한 반경 내로 찾는 노드
/// </summary>
public class BehaviorNodeFindObjectPrecondition : BehaviorNodeBasePrecondition 
{
    GameObjectType type;
    float radious;

    public BehaviorNodeFindObjectPrecondition(List<string> _listParams)
        : base(_listParams)
    {
        
        if(listParams.Count != 2)
        {
            throw new BehaviorNodeException("BehaviorNodeFindObjectPrecondition 파라미터의 개수가 맞지 않습니다.");
        }

        type = (GameObjectType)Enum.Parse(typeof(GameObjectType), listParams[0]);
        radious = float.Parse(listParams[1]);
    }

    override public bool traversalNode(GameObject targetObject)
    {
        C4_FindObjectInRadiousCollision component = targetObject.GetComponentInChildren<C4_FindObjectInRadiousCollision>();

        if (component == null) return false;

        return component.FindObjectsInRadious(radious, type);
    }

    override public object Clone()
    {
        return new BehaviorNodeFindObjectPrecondition(listParams);
    }
}