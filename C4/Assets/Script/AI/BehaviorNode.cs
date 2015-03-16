using UnityEngine;
using System.Collections.Generic;

public class BehaviorNode : IBehaviorNode
{
    protected GameObject targetObject;
    protected IBehaviorNode parents;
    protected List<IBehaviorNode> listChild;

    public BehaviorNode(GameObject targetObject)
    {
        this.targetObject = targetObject;
        parents = null;
        listChild = new List<IBehaviorNode>();
    }

    virtual public bool traversalNode()
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
        listChild.Add(node);
    }   
}