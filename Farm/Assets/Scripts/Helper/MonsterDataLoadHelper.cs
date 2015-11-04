using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class MonsterDataLoadHelper{
    
    XmlNodeList monsterNodeList;
    XMLLoader xmlLoader;

    public MonsterDataLoadHelper()
    {
        xmlLoader = new XMLLoader();
        monsterNodeList = xmlLoader.GetNodes("Monster");        
    }

    public MonsterInfo GetMonsterInfo(int _id)
    {
        MonsterInfo monsterInfo = new MonsterInfo();

        foreach (XmlNode node in monsterNodeList)
        {
            if (node["id"].InnerText == _id.ToString())
            {
                monsterInfo.id = _id;
                monsterInfo.hp = int.Parse(node["hp"].InnerText);
                monsterInfo.power = int.Parse(node["power"].InnerText);
                monsterInfo.cooldownTime = float.Parse(node["cooldownTime"].InnerText);
                monsterInfo.attackSpeed = float.Parse(node["attackSpeed"].InnerText);
                monsterInfo.range = float.Parse(node["range"].InnerText);
                monsterInfo.moveSpeed = float.Parse(node["moveSpeed"].InnerText);
                monsterInfo.skillID = int.Parse(node["skillID"].InnerText);
                break;
            }
        }

        return monsterInfo;
    }

    public List<MonsterInfo> GetMonsterInfoList()
    {
        List<MonsterInfo> monsterInfoList = new List<MonsterInfo>();

        MonsterInfo monsterInfo;

        foreach (XmlNode node in monsterNodeList)
        {
            monsterInfo = new MonsterInfo();

            monsterInfo.id = int.Parse(node["id"].InnerText); ;
            monsterInfo.hp = int.Parse(node["hp"].InnerText);
            monsterInfo.power = int.Parse(node["power"].InnerText);
            monsterInfo.cooldownTime = float.Parse(node["cooldownTime"].InnerText);
            monsterInfo.attackSpeed = float.Parse(node["attackSpeed"].InnerText);
            monsterInfo.range = float.Parse(node["range"].InnerText);
            monsterInfo.moveSpeed = float.Parse(node["moveSpeed"].InnerText);
            monsterInfo.skillID = int.Parse(node["skillID"].InnerText);

            monsterInfoList.Add(monsterInfo);
        }

        return monsterInfoList;
    }
}
