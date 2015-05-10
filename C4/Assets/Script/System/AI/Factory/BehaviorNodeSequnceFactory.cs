using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeSequnceFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        IBehaviorNode node = null;

        switch (className)
        {
            case "BehaviorNodeFollowChildSequence":
                {
                    node = new BehaviorNodeFollowChildSequence();
                }
                break;
            case "BehaviorNodeIfElseSequence":
                {
                    node = new BehaviorNodeIfElseSequence();
                }
                break;
            case "BehaviorNodeBaseSequence":
            default:
                {
                    node = new BehaviorNodeBaseSequence();
                }
                break;
        }

        return node;
    }
}
