using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class BluePrint{

    XmlNode bluePrintNode;
    XmlDocument bluePrintDoc;
    XmlNodeList bluePrintNodeList;

    public void LoadData()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/BluePrint");
        bluePrintDoc = new XmlDocument();
        bluePrintDoc.LoadXml(textAsset.text);
        bluePrintNode = bluePrintDoc.SelectSingleNode("BluePrintData");
        bluePrintNodeList = bluePrintNode.SelectNodes("BluePrint");
    }

    public void PrintData()
    {
        foreach (XmlNode tempNode in bluePrintNodeList)
            Debug.Log(tempNode["id"].InnerText);
    }

    public List<int> GetToolIDList()
    {
        List<int> ToolIDList = new List<int>();

        foreach (XmlNode tempNode in bluePrintNodeList) 
		{
			ToolIDList.Add (int.Parse (tempNode ["id"].InnerText));
		}
        return ToolIDList;
    }

	public void OpenBluePrint(int _id)
	{
		XmlElement newBluePrint = bluePrintDoc.CreateElement ("BluePrint");
		XmlElement idElement = bluePrintDoc.CreateElement ("id");
		idElement.InnerText = _id.ToString ();
		newBluePrint.AppendChild (idElement);
		bluePrintDoc.DocumentElement.InsertAfter (newBluePrint, bluePrintNode.LastChild);
		bluePrintDoc.Save("Assets/Resources/Data/BluePrint.xml");
	}
}
