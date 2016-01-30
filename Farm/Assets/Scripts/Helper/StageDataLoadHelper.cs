using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class StageDataLoadHelper{

    XmlNodeList statgeNodeList;
    XMLLoader xmlLoader;

    public StageDataLoadHelper()
    {
        xmlLoader = new XMLLoader();
    }

    public void StageDataLoad(int _chapter, int _stage)
    {
        string stageNo = "Stage" + _chapter.ToString() + "_" + _stage.ToString();
        statgeNodeList = xmlLoader.GetNodes(stageNo);
    }

	public bool GetClearInfo(int _chapter, int _stage)
	{
		string stageNo = "Stage" + _chapter.ToString() + "_" + _stage.ToString();
		int tempClearInfo = int.Parse (xmlLoader.GetRootNode (stageNo).SelectSingleNode ("ClearInfo").InnerText);
		if (tempClearInfo == 1) 
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

    public List<StageInfo> GetStageInfo(int _chapter, int _stage)
    {
        StageDataLoad(_chapter, _stage);

        List<StageInfo> stageInfoList = new List<StageInfo>();
        StageInfo stageInfo;

        foreach (XmlNode node in statgeNodeList)
        {
            stageInfo = new StageInfo();

            stageInfo.wave = int.Parse(node["wave"].InnerText);
            stageInfo.line = int.Parse(node["line"].InnerText);
            stageInfo.time = int.Parse(node["time"].InnerText);
            stageInfo.id = int.Parse(node["id"].InnerText);

            stageInfoList.Add(stageInfo);
        }


        return stageInfoList;
    }
}
