using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class BehaviorNodeBuilder
{
    public IBehaviorNode buildBehaviorNode(string targetpath)
    {
        XmlElement root = loadXML(targetpath);

        return null;
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




}