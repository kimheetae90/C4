using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeActionFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBaseAction(listParam);
    }
}