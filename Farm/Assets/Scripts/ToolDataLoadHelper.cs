using UnityEngine;
using System.Collections;
using System.Xml;

public class ToolDataLoadHelper : MonoBehaviour {

    XmlDocument toolInfoDoc;
    XmlNodeList toolNodeList;

    ToolInfo toolInfo;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/Tool");
        toolInfoDoc = new XmlDocument();
        toolInfoDoc.LoadXml(textAsset.text);
        XmlNode monsterInfoNode = toolInfoDoc.SelectSingleNode("ToolInfo");
        toolNodeList = monsterInfoNode.SelectNodes("Tool");
        toolInfo = new ToolInfo();
    }

    public ToolInfo GetToolInfo(int _id)
    {
        foreach (XmlNode node in toolNodeList)
        {
            if (node["id"].InnerText == _id.ToString())
            {
                toolInfo.power = int.Parse(node["power"].InnerText);
                toolInfo.range = float.Parse(node["range"].InnerText);
                toolInfo.hp = int.Parse(node["hp"].InnerText);
                toolInfo.piercingForce = int.Parse(node["piercingForce"].InnerText);
                toolInfo.moveSpeed = float.Parse(node["moveSpeed"].InnerText);
                break;
            }
        }

        return toolInfo;
    }
}
