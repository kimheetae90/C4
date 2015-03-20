using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeSelectorFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBaseSelector(listParam);
    }
}
