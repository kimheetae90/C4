using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeActionFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        IBehaviorNode node = null;

        switch(className)
        {
            case "BehaviorNodeMoveToNearObjectAction":
                {
                    node = new BehaviorNodeMoveToNearObjectAction(listParam);
                }
                break;
            case "BehaviorNodeBaseAction":
            default:
                {
                    node = new BehaviorNodeBaseAction(listParam);
                }
                break;
        }

        return node;
    }
}