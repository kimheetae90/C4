using UnityEngine;
using System.Collections.Generic;

public static class BehaviorNodePreconditionFactory 
{
    public static IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBasePrecondition(listParam);
    }
}