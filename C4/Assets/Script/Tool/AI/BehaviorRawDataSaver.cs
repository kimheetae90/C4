using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;


public class BehaviorRawDataSaver {
	public string savePath;
	public List<BehaviorRawNodeData> listNodeData;
	public List<BehaviorRawEdgeData> listEdgeData;
	public BehaviorRawNodeData root;

	public void saveFileterdData(string savePath, List<BehaviorRawNodeData> listNodeData, List<BehaviorRawEdgeData> listEdgeData)
	{
		root = null;
		this.savePath = savePath;
		this.listNodeData = listNodeData;
		this.listEdgeData = listEdgeData;

		buildTreeStruct();
		createXmlFile();
	}

	public void buildTreeStruct()
	{
		foreach (BehaviorRawEdgeData node in listEdgeData) 
		{
			var source = listNodeData.FirstOrDefault(i => i.id == node.source);
			
			var targets = from n in listNodeData
				where n.id == node.target 
					orderby n.priority
					select n;
			
			foreach(var target in targets)
			{
				target.parant = source;
				source.childs.Add(target);
			}
		}

		root = listNodeData.FirstOrDefault(i => i.parant == null);

		if(root == null)
		{
			throw new ToolException("RawData is not TreeStruct");
		}
	}

	void createXmlFile()
	{
		XmlDocument doc = new XmlDocument();
		
		XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
		doc.AppendChild(docNode);
		
		XmlNode xmlRoot = doc.CreateElement("BehaviorTree");
		doc.AppendChild(xmlRoot);

		addXmlNode(doc, xmlRoot, root);

		doc.Save(savePath);

		string filename = Path.GetFileName(savePath);
		AssetDatabase.ImportAsset("Assets/Data/AI/" + filename);
	}

	void addXmlNode(XmlDocument doc, XmlNode xmlParent, BehaviorRawNodeData data)
	{
		XmlNode xmlNode =  doc.CreateElement("node");

		XmlAttribute id = doc.CreateAttribute("id");
		id.Value = data.id.ToString();
		XmlAttribute type = doc.CreateAttribute("type");
		type.Value = data.type;

        XmlAttribute value = doc.CreateAttribute("value");
        value.Value = data.param;

		xmlNode.Attributes.Append(id);
		xmlNode.Attributes.Append(type);
        xmlNode.Attributes.Append(value);

		xmlParent.AppendChild(xmlNode);

		foreach (var childData in data.childs) 
		{
			addXmlNode(doc,xmlNode,childData);
		}
	}
}
#endif