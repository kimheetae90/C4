using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeSelectorFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        IBehaviorNode node = null;

        switch (className)
        {
            case "BehaviorNodeBaseSelector":
            default:
                {
                    node = new BehaviorNodeBaseSelector(listParam);
                }
                break;
        }

        return node;
    }
}
