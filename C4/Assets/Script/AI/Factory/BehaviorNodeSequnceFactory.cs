using UnityEngine;
using System.Collections.Generic;

public static class BehaviorNodeSequnceFactory 
{
    public static IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBaseSequence();
    }
}
