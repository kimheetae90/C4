using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLoader
{
    public XmlDocument GetFile(string _fileName)
    {
        string fileName = "Data/" + _fileName;
        XmlDocument xmlDoc = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load(fileName);
        xmlDoc.LoadXml(textAsset.text);

        return xmlDoc;
    }

    public XmlNode GetRootNode(string _dataName)
    {
        string dataName = _dataName + "Data";    
        XmlNode xmlNode;
    
        xmlNode = GetFile(_dataName).SelectSingleNode(dataName);
        return xmlNode;
    }

    public XmlNodeList GetNodes(string _dataName)
    {
        XmlNodeList xmlNodeList;
        xmlNodeList = GetRootNode(_dataName).SelectNodes(_dataName);
        return xmlNodeList;
    }
}
