using UnityEngine;
using System;
using System.Collections.Generic;

public class BehaviorNodeFindObjectPrecondition : BehaviorNodeBasePrecondition 
{
    ObjectID.Type type;
    float radious;

    public BehaviorNodeFindObjectPrecondition(List<string> listParams)
        : base(listParams)
    {
        
        if(listParams.Count < 2)
        {
            throw new BehaviorNodeException("BehaviorNodeFindObjectPrecondition 파라미터의 개수가 맞지 않습니다.");
        }

        type = Enum.Parse(typeof(ObjectID.Type as object), listParams[0]));
        radious = float.Parse(listParams[1]);
    }

    override public bool traversalNode()
    {
        //targetObject.GetComponent<
        return false;
    }

    override public object Clone()
    {
        return null;
    }
}