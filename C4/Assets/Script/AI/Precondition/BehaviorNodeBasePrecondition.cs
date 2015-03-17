using UnityEngine;
using System.Collections;

public class BehaviorNodeBasePrecondition : BehaviorNode
{
    string param;

    public BehaviorNodeBasePrecondition(GameObject targetObject, string param)
        : base(targetObject)
    {
        this.param = param;
    }

    override public bool traversalNode()
    {
        throw new BehaviorNodeException("precondition 함수가 구현되어 있지 않습니다.");
    }

    override public object Clone()
    {
        return new BehaviorNodeBasePrecondition(targetObject,param);
    }
}