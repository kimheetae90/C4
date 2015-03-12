using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class BehaviorRawDataParser
{
    public BehaviorRawDataParser()
    {

    }

    void clear()
    {

    }

    XmlElement LoadXML(string targetpath)
    {
        var sr = new StreamReader(targetpath);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(sr);
        XmlElement root = xmldoc.DocumentElement;
        return root;
    }

    public bool Parse(string targetpath)
    {
        XmlElement root = LoadXML(targetpath);
        XmlNodeList nodes = root.ChildNodes;

        foreach(XmlNode node in nodes)
        {
            readSection(node);
        }

        return true;
    }

    void readSection(XmlNode node)
    {
        if (node.Name != "section" && node.Attributes["name"] == null ) return;

        string value = node.Attributes["name"].Value;

        switch (value)
        {
            case "graph":
                {
                    readGraph(node);
                }
                break;
            case "node":
                {
                    readNode(node);
                }
                break;
            case "edge":
                {
                    readEdge(node);
                }
                break;
        }
    }

    void readGraph(XmlNode node)
    {
        XmlNodeList childNodes = node.ChildNodes;
        foreach (XmlNode childNode in childNodes)
        {
            readSection(childNode);
        }
    }

    void readNode(XmlNode node)
    {
        BehaviorRawNodeData data = new BehaviorRawNodeData();
        XmlNodeList childNodes = node.ChildNodes;
        foreach (XmlNode childNode in childNodes)
        {
            parseNode(childNode,data);
        }
    }

    void parseNode(XmlNode node, BehaviorRawNodeData data)
    {
        switch(node.Name)
        {
            case "attribute":
                {
                    parseNodeAttribute(node, data);
                }
                break;
            case "section":
                {
                    if (node.Attributes["name"] != null && node.Attributes["name"].Value == "graphics")
                    {
                        XmlNodeList childNodes = node.ChildNodes;
                        foreach (XmlNode childNode in childNodes)
                        {
                            parseNodeAttribute(childNode, data);
                        }
                    }
                }
                break;
        }
    }

    void parseNodeAttribute(XmlNode node, BehaviorRawNodeData data)
    {
        if (node.Name != "attribute" && node.Attributes["key"] == null) return;

        string key = node.Attributes["key"].Value;

        switch (key)
        {
            case "id":
                {
                    //set
                    Debug.Log("asdf");
                }
                break;
            case "label":
                {
                    //set
                    Debug.Log("asdf");
                }
                break;
            case "type":
                {
                    //set
                    Debug.Log("asdf");
                }
                break;
        }
    }

    //concrete function
    void readEdge(XmlNode node)
    {
        BehaviorRawEdgeData data = new BehaviorRawEdgeData();
        XmlNodeList childNodes = node.ChildNodes;
        foreach (XmlNode childNode in childNodes)
        {
            parseEdge(childNode, data);
        }
    }

    void parseEdge(XmlNode node, BehaviorRawEdgeData data)
    {
        if (node.Attributes["key"] == null) return;

        string key = node.Attributes["key"].Value;

        switch (key)
        {
            case "source":
                {
                    //set
                    Debug.Log("asdf");
                }
                break;
            case "target":
                {
                    //set
                    Debug.Log("asdf");
                }
                break;
        }
    }
}
