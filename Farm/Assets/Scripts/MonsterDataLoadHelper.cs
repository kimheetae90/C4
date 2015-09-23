using UnityEngine;
using System.Collections;
using System.Xml;

public class MonsterDataLoadHelper : MonoBehaviour {
    
    XmlDocument monsterInfoDoc;
    XmlNodeList monsterNodeList;

    MonsterInfo monsterInfo;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/Monster");
        monsterInfoDoc = new XmlDocument();
        monsterInfoDoc.LoadXml(textAsset.text);
        XmlNode monsterInfoNode = monsterInfoDoc.SelectSingleNode("MonsterInfo");
        monsterNodeList = monsterInfoNode.SelectNodes("Monster");
        monsterInfo = new MonsterInfo();
    }

    public MonsterInfo GetMonsterInfo(int _id)
    {
        foreach (XmlNode node in monsterNodeList)
        {
            if (node["id"].InnerText == _id.ToString())
            {
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
}
