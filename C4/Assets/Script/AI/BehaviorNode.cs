using UnityEngine;
using System.Collections.Generic;

public class BehaviorNode : IBehaviorNode
{
    protected GameObject targetObject;
    protected IBehaviorNode parents;
    protected List<IBehaviorNode> listChilds;

    public BehaviorNode()
    {
        targetObject = null;
        parents = null;
        listChilds = new List<IBehaviorNode>();
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
        listChilds.Add(node);
    }

    public void setTargetObject(GameObject obj)
    {
        targetObject = obj;

        foreach (IBehaviorNode child in listChilds)
        {
            child.setTargetObject(obj);
        }
    }
}