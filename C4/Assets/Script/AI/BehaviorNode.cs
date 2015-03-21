using UnityEngine;
using System.Collections.Generic;

public class BehaviorNode : IBehaviorNode
{
    protected IBehaviorNode parents;
    protected List<IBehaviorNode> listChilds;

    public BehaviorNode()
    {
        parents = null;
        listChilds = new List<IBehaviorNode>();
    }

    virtual public bool traversalNode(GameObject targetObject)
    {
        return false;
    }

    virtual public object Clone()
    {
        return null;
    }

    virtual public void setParents(IBehaviorNode node) 
    {
        parents = node;
    }

    virtual public void addChild(IBehaviorNode node)
    {
        listChilds.Add(node);
    }
}