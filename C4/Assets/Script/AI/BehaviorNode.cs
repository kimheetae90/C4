using UnityEngine;
using System.Collections.Generic;

public class BehaviorNode : IBehaviorNode
{
    public GameObject targetObject;
    public BehaviorNode parents;
    public List<BehaviorNode> listChild;

    public BehaviorNode(GameObject targetObject)
    {
        this.targetObject = targetObject;
        parents     = null;
        listChild   = new List<BehaviorNode>();
    }

    virtual public bool traversalNode()
    {
        return false;
    }

    virtual public object Clone()
    {
        return null;
    }
    
}