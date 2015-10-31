using UnityEngine;
using System.Collections;
using System.Xml;

public class UserInfo 
{
    XmlNode gold;
    XmlNode chapter;
    XmlNode stage;
    XmlNode tool1Id;
    XmlNode tool2Id;
    XmlNode tool3Id;
    XmlNode tool1Level;
    XmlNode tool2Level;
    XmlNode tool3Level;

    XmlNode userInfoNode;
    XmlDocument userInfoDoc;

    public void LoadData()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/UserInfo");
        userInfoDoc = new XmlDocument();
        userInfoDoc.LoadXml(textAsset.text);
        userInfoNode = userInfoDoc.SelectSingleNode("UserData");

        gold = userInfoNode["Gold"];
        chapter = userInfoNode["Chapter"];
        stage = userInfoNode["Stage"];
        tool1Id = userInfoNode["Tool1Id"];
        tool2Id = userInfoNode["Tool2Id"];
        tool3Id = userInfoNode["Tool3Id"];
        tool1Level = userInfoNode["Tool1Level"];
        tool2Level = userInfoNode["Tool2Level"];
        tool3Level = userInfoNode["Tool3Level"];
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

    public int GetChapter()
    {
        return int.Parse(chapter.InnerText);
    }

    public void SetChapter(int _chapterNo)
    {
        chapter.InnerText = _chapterNo.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public int GetStage()
    {
        return int.Parse(stage.InnerText);
    }

    public void SetStage(int _stageNo)
    {
        stage.InnerText = _stageNo.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public int GetTool1ID()
    {
        return int.Parse(tool1Id.InnerText);
    }

    public int GetTool2ID()
    {
        return int.Parse(tool2Id.InnerText);
    }

    public int GetTool3ID()
    {
        return int.Parse(tool3Id.InnerText);
    }

    public int GetTool1Level()
    {
        return int.Parse(tool1Level.InnerText);
    }

    public int GetTool2Level()
    {
        return int.Parse(tool2Level.InnerText);
    }

    public int GetTool3Level()
    {
        return int.Parse(tool3Level.InnerText);
    }

    public void SetTool1ID(int _id)
    {
        tool1Id.InnerText = _id.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool2ID(int _id)
    {
        tool2Id.InnerText = _id.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool3ID(int _id)
    {
        tool3Id.InnerText = _id.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool1Level(int _level)
    {
        tool1Level.InnerText = _level.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool2Level(int _level)
    {
        tool2Level.InnerText = _level.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool3Level(int _level)
    {
        tool3Level.InnerText = _level.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void PrintData()
    {
        Debug.Log(GetGold() + ", " + GetChapter() + ", " + GetStage()
            + ", " + GetTool1ID() + ", " + GetTool1Level()
            + ", " + GetTool2ID() + ", " + GetTool2Level()
            + ", " + GetTool3ID() + ", " + GetTool3Level());
    }
}
