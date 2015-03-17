using UnityEngine;
using System.Collections.Generic;

public static class BehaviorNodeSelectorFactory
{
    public static IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBaseSelector(listParam);
    }
}
