using UnityEngine;
using System.Collections.Generic;

public static class BehaviorNodeActionFactory
{
    public static IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBaseAction(listParam);
    }
}