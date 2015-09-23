using UnityEngine;
using System.Collections;
using System.Xml;

public class UserInfo 
{
    XmlNode gold;
    XmlNode chapterNo;
    XmlNode stageNo;
    XmlNode tool1;
    XmlNode tool2;
    XmlNode tool3;

    XmlNode userInfoNode;
    XmlDocument userInfoDoc;

    public void LoadData()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/UserInfo");
        userInfoDoc = new XmlDocument();
        userInfoDoc.LoadXml(textAsset.text);
        userInfoNode = userInfoDoc.SelectSingleNode("UserInfo");

        gold = userInfoNode["Gold"];
        chapterNo = userInfoNode["Chapter"];
        stageNo = userInfoNode["Stage"];
        tool1 = userInfoNode["Tool1"];
        tool2 = userInfoNode["Tool2"];
        tool3 = userInfoNode["Tool3"];
    }

    public int GetGold()
    {
        return int.Parse(gold.InnerText);
    }

    public void SetGold(int _gold)
    {
        gold.InnerText = _gold.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");  
    }

    public int GetChapterNo()
    {
        return int.Parse(chapterNo.InnerText);
    }

    public void SetChapterNo(int _chapterNo)
    {
        gold.InnerText = _chapterNo.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public int GetStageNo()
    {
        return int.Parse(stageNo.InnerText);
    }

    public void SetStageNo(int _stageNo)
    {
        gold.InnerText = _stageNo.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public int GetTool1ID()
    {
        return int.Parse(tool1["id"].InnerText);
    }

    public int GetTool2ID()
    {
        return int.Parse(tool2["id"].InnerText);
    }

    public int GetTool3ID()
    {
        return int.Parse(tool3["id"].InnerText);
    }

    public int GetTool1Level()
    {
        return int.Parse(tool1["level"].InnerText);
    }

    public int GetTool2Level()
    {
        return int.Parse(tool2["level"].InnerText);
    }

    public int GetTool3Level()
    {
        return int.Parse(tool3["level"].InnerText);
    }

    public void SetTool1ID(int _id)
    {
        tool1["id"].InnerText = _id.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool2ID(int _id)
    {
        tool2["id"].InnerText = _id.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool3ID(int _id)
    {
        tool3["id"].InnerText = _id.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool1Level(int _level)
    {
        tool1["level"].InnerText = _level.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool2Level(int _level)
    {
        tool2["level"].InnerText = _level.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool3Level(int _level)
    {
        tool3["level"].InnerText = _level.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void PrintData()
    {
        Debug.Log(GetGold() + ", " + GetChapterNo() + ", " + GetStageNo()
            + ", " + GetTool1ID() + ", " + GetTool1Level()
            + ", " + GetTool2ID() + ", " + GetTool2Level()
            + ", " + GetTool3ID() + ", " + GetTool3Level());
    }
}
