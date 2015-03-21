using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IBehaviorNode : ICloneable
{
    bool traversalNode(GameObject targetObject);
    void setParents(IBehaviorNode node);
    void addChild(IBehaviorNode node);
}