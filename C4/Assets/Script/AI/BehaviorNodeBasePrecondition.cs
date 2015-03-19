using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeBasePrecondition : BehaviorNode
{
    List<string> listParams;

    public BehaviorNodeBasePrecondition(List<string> listParams)
        : base()
    {
        this.listParams = listParams;
    }

    override public bool traversalNode()
    {
        throw new BehaviorNodeException("precondition 함수가 구현되어 있지 않습니다.");
    }

    override public object Clone()
    {
        return new BehaviorNodeBasePrecondition(listParams);
    }
}