using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ToolDataLoadHelper : MonoBehaviour {

    XmlNodeList toolNodeList;
    XMLLoader xmlLoader;

    ToolInfo toolInfo;
    List<ToolInfo> toolInfoList;

    void Awake()    
    {
        xmlLoader = new XMLLoader();
        toolNodeList = xmlLoader.GetNodes("Tool");
    }

    //void Start()
    //{
    //    Debug.Log("툴로더");

    //    Debug.Log("툴 1개 정보");
    //    toolInfo = GetToolInfo(11201);
    //    Debug.Log(
    //        "power : " + toolInfo.power + ", " + 
    //        "range : " + toolInfo.range + ", " + 
    //        "attackSpeed : " + toolInfo.attackSpeed + ", " + 
    //        "hp : " + toolInfo.hp + ", " + 
    //        "piercingForce : " + toolInfo.piercingForce + ", " + 
    //        "moveSpeed : " + toolInfo.moveSpeed + ", " + 
    //        "upgradePower : " + toolInfo.upgradePower + ", " + 
    //        "upgradeRange : " + toolInfo.upgradeRange + ", " + 
    //        "upgradeAttackSpeed : " + toolInfo.upgradeAttackSpeed + ", " + 
    //        "upgradeHp : " + toolInfo.upgradeHp + ", " + 
    //        "upgradePiercingForce : " + toolInfo.upgradePiercingForce + ", " + 
    //        "upgradeMoveSpeed : " + toolInfo.upgradeMoveSpeed + ", " + 
    //        "price : " + toolInfo.price + ", " + 
    //        "upgradePrice : " + toolInfo.upgradePrice + ", " + 
    //        "open : " + toolInfo.open + ", " + 
    //        "have : " + toolInfo.have
    //        );

    //    Debug.Log("전체 툴 정보");
    //    toolInfoList = GetToolInfoList();
    //    foreach (ToolInfo tool in toolInfoList)
    //    {
    //        Debug.Log(
    //            "power : " + tool.power + ", " +
    //            "range : " + tool.range + ", " +
    //            "attackSpeed : " + tool.attackSpeed + ", " +
    //            "hp : " + tool.hp + ", " +
    //            "piercingForce : " + tool.piercingForce + ", " +
    //            "moveSpeed : " + tool.moveSpeed + ", " +
    //            "upgradePower : " + tool.upgradePower + ", " +
    //            "upgradeRange : " + tool.upgradeRange + ", " +
    //            "upgradeAttackSpeed : " + tool.upgradeAttackSpeed + ", " +
    //            "upgradeHp : " + tool.upgradeHp + ", " +
    //            "upgradePiercingForce : " + tool.upgradePiercingForce + ", " +
    //            "upgradeMoveSpeed : " + tool.upgradeMoveSpeed + ", " +
    //            "price : " + tool.price + ", " +
    //            "upgradePrice : " + tool.upgradePrice + ", " +
    //            "open : " + tool.open + ", " +
    //            "have : " + tool.have
    //            );
    //    }

    //}

    public ToolInfo GetToolInfo(int _id)
    {
        toolInfo = new ToolInfo();

        foreach (XmlNode node in toolNodeList)
        {
            if (node["id"].InnerText == _id.ToString())
            {
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
                toolInfo.have = int.Parse(node["have"].InnerText);
                break;
            }
        }

        return toolInfo;
    }

    public List<ToolInfo> GetToolInfoList()
    {
        toolInfoList = new List<ToolInfo>();

        foreach (XmlNode node in toolNodeList)
        {
            toolInfo = new ToolInfo();

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
            toolInfo.have = int.Parse(node["have"].InnerText);
            
            toolInfoList.Add(toolInfo);
        }

        return toolInfoList;
    }
}
