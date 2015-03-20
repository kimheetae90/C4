using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeSequnceFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        return new BehaviorNodeBaseSequence();
    }
}
