using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ToolDataLoadHelper{

    XmlNodeList toolNodeList;
    XMLLoader xmlLoader;

    public ToolDataLoadHelper()
    {
        xmlLoader = new XMLLoader();
        toolNodeList = xmlLoader.GetNodes("Tool");
    }


    public ToolInfo GetToolInfo(int _id)
    {
        ToolInfo toolInfo = new ToolInfo();

        foreach (XmlNode node in toolNodeList)
        {
            if (node["id"].InnerText == _id.ToString())
            {
                toolInfo.id = _id;
                toolInfo.name = node["name"].InnerText.ToString();
                toolInfo.power = int.Parse(node["power"].InnerText);
                toolInfo.range = float.Parse(node["range"].InnerText);
                toolInfo.attackSpeed = float.Parse(node["attackSpeed"].InnerText);
                toolInfo.hp = int.Parse(node["hp"].InnerText);
                toolInfo.piercingForce = int.Parse(node["piercingForce"].InnerText);
                toolInfo.moveSpeed = float.Parse(node["moveSpeed"].InnerText);
                toolInfo.upgradePower = int.Parse(node["upgradePower"].InnerText);
                toolInfo.upgradeRange = float.Parse(node["upgradeRange"].InnerText);
                toolInfo.upgradeAttackSpeed = float.Parse(node["upgradeAttackSpeed"].InnerText);
                toolInfo.upgradeHp = int.Parse(node["upgradeHp"].InnerText);
                toolInfo.upgradePiercingForce = int.Parse(node["upgradePiercingForce"].InnerText);
                toolInfo.upgradeMoveSpeed = float.Parse(node["upgradeMoveSpeed"].InnerText);
                toolInfo.price = int.Parse(node["price"].InnerText);
                toolInfo.upgradePrice = int.Parse(node["upgradePrice"].InnerText);
                break;
            }
        }

        return toolInfo;
    }

    public List<ToolInfo> GetToolInfoList()
    {
        List<ToolInfo> toolInfoList = new List<ToolInfo>();

        ToolInfo toolInfo;

        foreach (XmlNode node in toolNodeList)
        {
            toolInfo = new ToolInfo();

            toolInfo.id = int.Parse(node["id"].InnerText);
            toolInfo.name = node["name"].InnerText.ToString();
            toolInfo.power = int.Parse(node["power"].InnerText);
            toolInfo.range = float.Parse(node["range"].InnerText);
            toolInfo.attackSpeed = float.Parse(node["attackSpeed"].InnerText);
            toolInfo.hp = int.Parse(node["hp"].InnerText);
            toolInfo.piercingForce = int.Parse(node["piercingForce"].InnerText);
            toolInfo.moveSpeed = float.Parse(node["moveSpeed"].InnerText);
            toolInfo.upgradePower = int.Parse(node["upgradePower"].InnerText);
            toolInfo.upgradeRange = float.Parse(node["upgradeRange"].InnerText);
            toolInfo.upgradeAttackSpeed = float.Parse(node["upgradeAttackSpeed"].InnerText);
            toolInfo.upgradeHp = int.Parse(node["upgradeHp"].InnerText);
            toolInfo.upgradePiercingForce = int.Parse(node["upgradePiercingForce"].InnerText);
            toolInfo.upgradeMoveSpeed = float.Parse(node["upgradeMoveSpeed"].InnerText);
            toolInfo.price = int.Parse(node["price"].InnerText);
            toolInfo.upgradePrice = int.Parse(node["upgradePrice"].InnerText);
            toolInfo.open = int.Parse(node["open"].InnerText);
            toolInfoList.Add(toolInfo);
        }

        return toolInfoList;
    }
}
