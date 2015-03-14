using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class BehaviorRawDataParser
{
    List<BehaviorRawEdgeData> ListEdgeData;
    List<BehaviorRawNodeData> ListNodeData;

    public BehaviorRawDataParser()
    {
        ListEdgeData = new List<BehaviorRawEdgeData>();
        ListNodeData = new List<BehaviorRawNodeData>();
        clear();
    }

    void clear()
    {
        ListEdgeData.Clear();
        ListNodeData.Clear();
    }

    XmlElement LoadXML(string targetpath)
    {
        var sr = new StreamReader(targetpath);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(sr);
        XmlElement root = xmldoc.DocumentElement;
        return root;
    }

    public bool ParseRawBehaviorData(string targetpath)
    {
		clear();

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
        ListNodeData.Add(data);
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
					Int32.TryParse(node.InnerText, out data.ID);            
                }
                break;
            case "label":
                {
					string value = node.InnerText;
					data.param = value;
			    }
                break;
            case "type":
                {
					data.type = getBehaviorNodeType(node.InnerText);
                }
				break;
			case "x":
				{
					Double.TryParse(node.InnerText, out data.priority);
				}
                break;
        }
    }

	string getBehaviorNodeType(string text)
	{
		string ret = "";
		switch (text) 
		{
		case "diamond":
			{
				ret = "Selector";
			}
			break;
		case "rectangle":
			{
				ret = "sequence";
			}
			break;
		case "ellipse":
			{
				ret = "precondition";
			}
			break;
		case "parallelogram":
			{
				ret = "action";
			}
			break;
		default:
			{
				throw new BehaviorRawDataParseException("Invalid Node Type : "+text);
			}
		}
		return ret;
	}
	
	void readEdge(XmlNode node)
    {
        BehaviorRawEdgeData data = new BehaviorRawEdgeData();
        XmlNodeList childNodes = node.ChildNodes;
        foreach (XmlNode childNode in childNodes)
        {
            parseEdge(childNode, data);
        }
        ListEdgeData.Add(data);
    }

    void parseEdge(XmlNode node, BehaviorRawEdgeData data)
    {
        if (node.Attributes["key"] == null) return;

        string key = node.Attributes["key"].Value;

        switch (key)
        {
            case "source":
                {
					Int32.TryParse(node.InnerText,out data.Source);
			    }
                break;
            case "target":
                {
					Int32.TryParse(node.InnerText,out data.Target);
                }
                break;
        }
    }
	
	public List<BehaviorRawEdgeData> getParsedRawEdgeData()
	{
		return ListEdgeData;
	}

	public List<BehaviorRawNodeData> getParsedRawNodeData()
	{
		return ListNodeData;
	}
}
