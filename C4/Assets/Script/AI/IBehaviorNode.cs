using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IBehaviorNode : ICloneable
{
    bool traversalNode();
    void setParents(IBehaviorNode node);
    void addChild(IBehaviorNode node);
    void setTargetObject(GameObject obj);
}