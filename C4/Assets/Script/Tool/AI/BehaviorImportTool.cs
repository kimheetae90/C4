using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BehaviorImportTool : EditorWindow
{
	// Use this for initialization
    List<string> listXmlFile = new List<string>();

    bool bNeedUpdateRawDataPath = true;
    int selectedIndex = 0;
    string basePathToLoad = Application.dataPath + "/Data/AI/Raw";
    string basePathToSave = Application.dataPath + "/Data/AI/";
   
    BehaviorRawDataParser parser = new BehaviorRawDataParser();

    [MenuItem("Window/AI/Behavior Import Tool")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(BehaviorImportTool));
    }

    void Update()
    {
        ReloadProcess();
    }

    void ReloadProcess()
    {
        if (bNeedUpdateRawDataPath)
        {
            ReloadRawDataDirectory();
            bNeedUpdateRawDataPath = false;
        }
    }

    void OnHierarchyChange()
    {
        bNeedUpdateRawDataPath = true;
    }

    private void ReloadRawDataDirectory()
    {
        listXmlFile.Clear();
        var info = new DirectoryInfo(basePathToLoad);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".xgml")
            {
                listXmlFile.Add(file.FullName);
            }
        }
    }

    void OnGUI()
    {
        ShowParserUI();
    }

    void ShowParserUI()
    {
        GUILayout.Label("GXML 파일을 로드해주세요", EditorStyles.boldLabel);
        selectedIndex = EditorGUILayout.Popup(selectedIndex, listXmlFile.ToArray());
        if (GUILayout.Button("Load"))
        {
            ProcLoadBtn();
        }
    }

    void ProcLoadBtn()
    {
        if (listXmlFile.Count > 0)
        {
            ParseRawData();
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                  "읽을 파일이 존재하지 않습니다.", "OK");
        }
    }

    private void ParseRawData()
    {
        string targetPath = listXmlFile[selectedIndex];
        if (parser.Parse(targetPath))
        {
            EditorUtility.DisplayDialog("파일 읽기 성공",
                        "파일이 올바르게 저장되었습니다. 파일명 : " + targetPath, "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                        "파일 읽기에 실패하였습니다. 파일명 : " + targetPath, "OK");
        }
    }
}
