using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;


public class BehaviorRawDataSaver {
	public string savePath;
	public List<BehaviorRawNodeData> ListNodeData;
	public List<BehaviorRawEdgeData> ListEdgeData;
	public BehaviorRawNodeData root;

	public void saveFileterdData(string savePath, List<BehaviorRawNodeData> ListNodeData, List<BehaviorRawEdgeData> ListEdgeData)
	{
		root = null;
		this.savePath = savePath;
		this.ListNodeData = ListNodeData;
		this.ListEdgeData = ListEdgeData;

		BuildTreeStruct();
		createXmlFile();
	}

	public void BuildTreeStruct()
	{
		foreach (BehaviorRawEdgeData node in ListEdgeData) 
		{
			var source = ListNodeData.FirstOrDefault(i => i.ID == node.Source);
			
			var targets = from n in ListNodeData
				where n.ID == node.Target
					orderby n.priority
					select n;
			
			foreach(var target in targets)
			{
				target.parant = source;
				source.childs.Add(target);
			}
		}

		root = ListNodeData.FirstOrDefault(i => i.parant == null);

		if(root == null)
		{
			throw new BehaviorRawDataParseException("RawData is not TreeStruct");
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
		id.Value = data.ID.ToString();
		XmlAttribute type = doc.CreateAttribute("type");
		type.Value = data.type;

		xmlNode.InnerText = data.param;

		xmlNode.Attributes.Append(id);
		xmlNode.Attributes.Append(type);

		xmlParent.AppendChild(xmlNode);

		foreach (var childData in data.childs) 
		{
			addXmlNode(doc,xmlNode,childData);
		}
	}
}
