using UnityEngine;
using System.Collections;

public class BehaviorNodeBaseSequence : BehaviorNode
{

    public BehaviorNodeBaseSequence(GameObject targetObject)
        : base(targetObject)
    {

    }

    override public bool traversalNode()
    {
        bool bRet = true;

        foreach (var node in listChild)
        {
            if (node.traversalNode() == false)
            {
                bRet = false;
                break;
            }
        }

        return bRet;
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseSequence(targetObject);
    }
}