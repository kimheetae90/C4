using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLoader
{
    static XmlDocument xmlDoc = new XmlDocument();
    static XmlNode xmlNode;
    static XmlNodeList xmlNodeList;
    
    public static XmlDocument GetFile(string _fileName)
    {
        string fileName = "Data/" + _fileName;
        
        TextAsset textAsset = (TextAsset)Resources.Load(fileName);
        xmlDoc.LoadXml(textAsset.text);

        return xmlDoc;
    }

    public static XmlNode GetRootNode(string _dataName)
    {
        string dataName = _dataName + "Data";        
        xmlNode = GetFile(_dataName).SelectSingleNode(dataName);
        return xmlNode;
    }

    public static XmlNodeList GetNodes(string _dataName)
    {
        xmlNodeList = GetRootNode(_dataName).SelectNodes(_dataName);
        return xmlNodeList;
    }
}
