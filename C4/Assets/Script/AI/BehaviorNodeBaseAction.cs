using UnityEngine;
using System.Collections;

public class BehaviorNodeBaseAction : BehaviorNode
{
    string param;

    public BehaviorNodeBaseAction(GameObject targetObject, string param)
        : base(targetObject)
    {
        this.param = param;
    }

    override public bool traversalNode()
    {
        throw new BehaviorNodeException("Action 함수가 구현되어 있지 않습니다.");
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseAction(targetObject, param);
    }
}