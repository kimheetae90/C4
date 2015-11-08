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

    public int GetOpen(int _id)
    {
        foreach(XmlNode tempNode in bluePrintNodeList)
            if (tempNode["id"].InnerText == _id.ToString()) return int.Parse(tempNode["open"].InnerText);

        return int.MinValue;
    }

    public int GetHave(int _id)
    {
        foreach (XmlNode tempNode in bluePrintNodeList)
            if (tempNode["id"].InnerText == _id.ToString()) return int.Parse(tempNode["have"].InnerText);

        return int.MinValue;
    }

    public void SetOpen(int _id, int _value)
    {
        foreach (XmlNode tempNode in bluePrintNodeList)
        { 
            if (tempNode["id"].InnerText == _id.ToString())
            {
                tempNode["open"].InnerText = _value.ToString();
                bluePrintDoc.Save("Assets/Resources/Data/BluePrint.xml");
                break;
            }
        }
    }

    public void SetHave(int _id, int _value)
    {
        foreach (XmlNode tempNode in bluePrintNodeList)
        {
            if (tempNode["id"].InnerText == _id.ToString())
            {
                tempNode["have"].InnerText = _value.ToString();
                bluePrintDoc.Save("Assets/Resources/Data/BluePrint.xml");
                break;
            }
        }
    }

    public void PrintData()
    {
        foreach (XmlNode tempNode in bluePrintNodeList)
            Debug.Log(tempNode["id"].InnerText + " -> open :" + tempNode["open"].InnerText + ", have" + tempNode["have"].InnerText);
    }

    public List<int> GetToolIDListHave()
    {
        List<int> ToolIDList = new List<int>();

        foreach (XmlNode tempNode in bluePrintNodeList)
            if (int.Parse(tempNode["have"].InnerText) > 0) 
            {
                for (int i = 0; i < int.Parse(tempNode["have"].InnerText); i++ )
                {
                    ToolIDList.Add(int.Parse(tempNode["id"].InnerText));
                }
            }
        return ToolIDList;
    }
}
