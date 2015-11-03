using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class MonsterDataLoadHelper : MonoBehaviour {
    
    XmlNodeList monsterNodeList;
    XMLLoader xmlLoader;

    MonsterInfo monsterInfo;
    List<MonsterInfo> monsterInfoList;

    void Awake()
    {
        xmlLoader = new XMLLoader();
        monsterNodeList = xmlLoader.GetNodes("Monster");        
    }

    //void Start()
    //{
    //    UserInfo userInfo = new UserInfo();
    //    userInfo.LoadData();
    //    userInfo.PrintData();
    //    Debug.Log("변경!!");
    //    userInfo.SetChapter(2);
    //    userInfo.SetGold(99999);
    //    userInfo.SetStage(4);
    //    userInfo.SetTool1ID(9999);
    //    userInfo.SetTool1Level(3);
    //    userInfo.SetTool2ID(2222);
    //    userInfo.SetTool2Level(2);
    //    userInfo.SetTool3ID(4444);
    //    userInfo.SetTool3Level(3);
    //    userInfo.PrintData();
    //}

    //void Start()
    //{
    //    Debug.Log("몬스터 로더");

    //    Debug.Log("몬스터 1개 정보");
    //    monsterInfo = GetMonsterInfo(21101);
    //    Debug.Log(
    //        "hp : " + monsterInfo.hp + ", " +
    //        "power : " + monsterInfo.power + ", " +
    //        "cooldownTime : " + monsterInfo.cooldownTime + ", " +
    //        "attackSpeed : " + monsterInfo.attackSpeed + ", " +
    //        "range : " + monsterInfo.range + ", " +
    //        "moveSpeed : " + monsterInfo.moveSpeed + ", " +
    //        "skillID : " + monsterInfo.skillID
    //        );

    //    Debug.Log("전체 몬스터 정보");
    //    monsterInfoList = GetMonsterInfoList();
    //    foreach (MonsterInfo monster in monsterInfoList)
    //    {
    //        Debug.Log(
    //        "hp : " + monster.hp + ", " +
    //        "power : " + monster.power + ", " +
    //        "cooldownTime : " + monster.cooldownTime + ", " +
    //        "attackSpeed : " + monster.attackSpeed + ", " +
    //        "range : " + monster.range + ", " +
    //        "moveSpeed : " + monster.moveSpeed + ", " +
    //        "skillID : " + monster.skillID
    //            );
    //    }

    //}

    public MonsterInfo GetMonsterInfo(int _id)
    {
        monsterInfo = new MonsterInfo();

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

    public List<MonsterInfo> GetMonsterInfoList()
    {
        monsterInfoList = new List<MonsterInfo>();

        foreach (XmlNode node in monsterNodeList)
        {
            monsterInfo = new MonsterInfo();

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
