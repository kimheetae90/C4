using UnityEngine;
using System.Collections;

public class BehaviorNodeBaseSequence : BehaviorNode
{
    public BehaviorNodeBaseSequence()
        : base()
    {

    }

    override public bool traversalNode(GameObject targetObjec)
    {
        foreach (var node in listChilds)
        {
            if (node.traversalNode(targetObjec) == false)
            {
                break;
            }
        }

        return true;
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseSequence();
    }
}