using UnityEngine;
using System.Collections.Generic;

public interface IBehaviorNodeFactory
{
    IBehaviorNode createNode(string className, List<string> listParam);
}