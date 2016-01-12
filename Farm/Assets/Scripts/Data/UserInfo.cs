using UnityEngine;
using System.Collections;
using System.Xml;

public class UserInfo 
{
    XmlNode gold;
    XmlNode chapter;
    XmlNode stage;
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
        userInfoNode = userInfoDoc.SelectSingleNode("UserData");

        gold = userInfoNode["Gold"];
        chapter = userInfoNode["Chapter"];
        stage = userInfoNode["Stage"];
		tool1 = userInfoNode["Tool1Instance"];
		tool2 = userInfoNode["Tool2Instance"];
		tool3 = userInfoNode["Tool3Instance"];
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
    
    public int GetTool1()
    {
        return int.Parse(tool1.InnerText);
    }

    public int GetTool2()
    {
        return int.Parse(tool2.InnerText);
    }

    public int GetTool3()
    {
        return int.Parse(tool3.InnerText);
    }

    public void SetTool1(int _instance)
    {
		tool1.InnerText = _instance.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool2(int _instance)
    {
		tool2.InnerText = _instance.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }

    public void SetTool3(int _instance)
    {
		tool3.InnerText = _instance.ToString();
        userInfoDoc.Save("Assets/Resources/Data/UserInfo.xml");
    }


    public void PrintData()
    {
        Debug.Log (GetGold () + ", " + GetChapter () + ", " + GetStage ()
			+ ", " + GetTool1 () + ", " + GetTool2 () + ", " + GetTool3 ());
    }
}
