using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeBaseAction : BehaviorNode
{
    List<string> listParams;

    public BehaviorNodeBaseAction(List<string> listParams)
        : base()
    {
        this.listParams = listParams;
    }

    override public bool traversalNode()
    {
        throw new BehaviorNodeException("Action 함수가 구현되어 있지 않습니다.");
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseAction(listParams);
    }
}