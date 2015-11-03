using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class StageDataLoadHelper : MonoBehaviour {

    XmlNodeList statgeNodeList;
    XMLLoader xmlLoader;
        
    List<StageInfo> stageInfoList;
    StageInfo stageInfo;

    //void Start()
    //{
    //    stageInfoList = GetStageInfo(1,1);

    //    foreach(StageInfo node in stageInfoList)
    //    {
    //        Debug.Log(
    //            "wave : " + node.wave + ", " +
    //            "line : " + node.line + ", " +
    //            "time : " + node.time + ", " +
    //            "id : " + node.id
    //            );
    //    }
    //}

    void Awake()
    {
        xmlLoader = new XMLLoader();
    }

    public void StageDataLoad(int _chapter, int stage)
    {
        string stageNo = "Stage" + _chapter.ToString() + "_" + stage.ToString();
        statgeNodeList = xmlLoader.GetNodes(stageNo);
    }

    public List<StageInfo> GetStageInfo(int _chapter, int _stage)
    {
        StageDataLoad(_chapter, _stage);

        stageInfoList = new List<StageInfo>();

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
