using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodePreconditionFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBasePrecondition(listParam);
    }
}