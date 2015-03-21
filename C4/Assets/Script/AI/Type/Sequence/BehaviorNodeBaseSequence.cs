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
        bool bRet = true;

        foreach (var node in listChilds)
        {
            if (node.traversalNode(targetObjec) == false)
            {
                bRet = false;
                break;
            }
        }

        return bRet;
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseSequence();
    }
}