using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class BehaviorNodeFactory
{
    BehaviorNodeSequnceFactory sequnceFactory;
    BehaviorNodePreconditionFactory preconditionFactory;
    BehaviorNodeSelectorFactory selectorFactory;
    BehaviorNodeActionFactory actionFactory;

    public BehaviorNodeFactory()
    {
        sequnceFactory = new BehaviorNodeSequnceFactory();
        preconditionFactory = new BehaviorNodePreconditionFactory();
        selectorFactory = new BehaviorNodeSelectorFactory();
        actionFactory = new BehaviorNodeActionFactory();
    }

    public IBehaviorNode buildBehaviorNode(string targetpath)
    {
        XmlElement xmlRoot = loadXML(targetpath);
        
        if(xmlRoot == null)
        {
            throw new BehaviorNodeException("Root node is Not Exist");
        }

        XmlNodeList nodes = xmlRoot.ChildNodes;
        
        if(nodes.Count > 1)
        {
            throw new BehaviorNodeException("Root node is over One");
        }

        IBehaviorNode root = startTraversalXmlNode(xmlRoot.ChildNodes[0]);

        return root;
    }

    //래퍼만들자
    XmlElement loadXML(string targetpath)
    {
        var sr = new StreamReader(targetpath);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(sr);
        XmlElement root = xmldoc.DocumentElement;
        return root;
    }

    IBehaviorNode startTraversalXmlNode(XmlNode xmlParent)
    {
        IBehaviorNode newNode = parseXmlNode(xmlParent);

        foreach (XmlNode node in xmlParent.ChildNodes)
        {
            traversalXmlNode(node,newNode);
        }

        return newNode;
    }

    void traversalXmlNode(XmlNode xmlParent,IBehaviorNode behaviorParents)
    {
        IBehaviorNode newNode = parseXmlNode(xmlParent);

        behaviorParents.addChild(newNode);
        newNode.setParents(behaviorParents);

        foreach (XmlNode node in xmlParent.ChildNodes)
        {
            traversalXmlNode(node,newNode);
        }
    }

    IBehaviorNode parseXmlNode(XmlNode xmlNode)
    {
        if (xmlNode == null)
        {
            throw new BehaviorNodeException("Root node is Not Exist");
        }

        string type = xmlNode.Attributes["type"].Value;
        string val = xmlNode.Attributes["value"].Value;

        IBehaviorNode newNode = behaviorNodeFactory(type, val);

        return newNode;
    }

    IBehaviorNode behaviorNodeFactory(string type, string val)
    {
        IBehaviorNode ret = null;
        
        List<string> list = new List<string>(val.Split('@'));
        
        if(list.Count <= 0)
        {
            throw new BehaviorNodeException("Param is Null..");
        }

        List<string> valList = new List<string>();

        if(list.Count > 1)
        {
            valList = list.GetRange(1, list.Count - 1);
        }

        switch(type)
        {
            case "sequence":
                ret = sequnceFactory.createNode(list[0], valList);
                break;
            case "precondition":
                ret = preconditionFactory.createNode(list[0], valList);
                break;
            case "selector":
                ret = selectorFactory.createNode(list[0], valList);
                break;
            case "action":
                ret = actionFactory.createNode(list[0], valList);
                break;
        }

        return ret;
    }


}