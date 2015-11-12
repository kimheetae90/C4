using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class MyTool
{
	XmlNode myToolNode;
	XmlDocument myToolDoc;
	XmlNode countNode;
	XmlNodeList myToolNodeList;

	public void LoadData()
	{
		TextAsset textAsset = (TextAsset)Resources.Load("Data/MyTool");
		myToolDoc = new XmlDocument();
		myToolDoc.LoadXml(textAsset.text);
		myToolNode = myToolDoc.SelectSingleNode("MyToolData");
		countNode = myToolNode.SelectSingleNode ("Count");
		myToolNodeList = myToolNode.SelectNodes("MyTool");
	}

	public List<MyToolInfo> GetMyToolInfoList()
	{
		List<MyToolInfo> myToolInfoList = new List<MyToolInfo> ();
		MyToolInfo myToolInfo;

		foreach (XmlNode node in myToolNodeList) 
		{
			myToolInfo = new MyToolInfo();
			myToolInfo.instance = int.Parse(node["instance"].InnerText);
			myToolInfo.id = int.Parse(node["id"].InnerText);
			myToolInfo.level = int.Parse(node["level"].InnerText);
			myToolInfoList.Add(myToolInfo);
		}

		return myToolInfoList;
	}

	public MyToolInfo GetMyToolInfo(int _instance)
	{
		MyToolInfo myToolInfo = new MyToolInfo();
		
		foreach (XmlNode node in myToolNodeList) 
		{
			if(int.Parse(node["instance"].InnerText) == _instance)
			{
				myToolInfo.instance = int.Parse(node["instance"].InnerText);
				myToolInfo.id = int.Parse(node["id"].InnerText);
				myToolInfo.level = int.Parse(node["level"].InnerText);
				break;
			}
		}
		
		return myToolInfo;
	}

	public void LevelUp(int _instance)
	{
		foreach (XmlNode node in myToolNodeList) 
		{
			if(int.Parse(node["instance"].InnerText) == _instance)
			{
				int tempLevel = int.Parse(node["level"].InnerText);
				tempLevel++;
				node["level"].InnerText = tempLevel.ToString();
				break;
			}
		}

		myToolDoc.Save("Assets/Resources/Data/MyTool.xml");  
	}

	public void BuyNewTool(int _id)
	{
		XmlElement newTool = myToolDoc.CreateElement ("MyTool");
		XmlElement instanceElem = myToolDoc.CreateElement ("instance");
		XmlElement idElem = myToolDoc.CreateElement ("id");
		XmlElement levelElem = myToolDoc.CreateElement ("level");
		instanceElem.InnerText = countNode.InnerText;
		countNode.InnerText = (int.Parse (countNode.InnerText) + 1).ToString ();
		idElem.InnerText = _id.ToString ();
		levelElem.InnerText = "1";
		newTool.AppendChild (instanceElem);
		newTool.AppendChild (idElem);
		newTool.AppendChild (levelElem);
		myToolDoc.DocumentElement.InsertAfter (newTool, myToolNode.LastChild);
		myToolDoc.Save("Assets/Resources/Data/MyTool.xml");
	}

	public void PrintData()
	{
		List<MyToolInfo> myToolInfoList = GetMyToolInfoList ();

		foreach (MyToolInfo tool in myToolInfoList) 
		{
			Debug.Log(tool.instance + " -> id : " +tool.id + ", level : " + tool.level);
		}
	}
}
