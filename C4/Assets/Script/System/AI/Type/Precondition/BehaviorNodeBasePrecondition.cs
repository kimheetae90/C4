using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeBasePrecondition : BehaviorNode
{
    public List<string> listParams;

    public BehaviorNodeBasePrecondition(List<string> listParams)
        : base()
    {
        this.listParams = listParams;
    }

    override public bool traversalNode(GameObject targetObjec)
    {

        throw new BehaviorNodeException("precondition 함수가 구현되어 있지 않습니다.");
    }

    override public object Clone()
    {
        return new BehaviorNodeBasePrecondition(listParams);
    }
}