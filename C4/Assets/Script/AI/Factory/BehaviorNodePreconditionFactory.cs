using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodePreconditionFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        IBehaviorNode node = null;

        switch (className)
        {
            case "BehaviorNodeFindObjectPrecondition":
                {
                    node = new BehaviorNodeFindObjectPrecondition(listParam);
                }
                break;
            case "BehaviorNodeBasePrecondition":
            default:
                {
                    node = new BehaviorNodeBasePrecondition(listParam);
                }
                break;
        }

        return node;
    }
}