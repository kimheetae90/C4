using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BehaviorImportTool : EditorWindow
{
	// Use this for initialization
    List<string> ListXmlFile = new List<string>();

    bool NeedUpdateRawDataPath = true;
    int SelectedIndex = 0;
    string BasePathToLoad = Application.dataPath + "/Data/AI/Raw";
    string BasePathToSave = Application.dataPath + "/Data/AI/";
   
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
        if (NeedUpdateRawDataPath)
        {
            ReloadRawDataDirectory();
            NeedUpdateRawDataPath = false;
        }
    }

    void OnHierarchyChange()
    {
        NeedUpdateRawDataPath = true;
    }

    private void ReloadRawDataDirectory()
    {
        ListXmlFile.Clear();
        var info = new DirectoryInfo(BasePathToLoad);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".xgml")
            {
                ListXmlFile.Add(file.FullName);
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
        SelectedIndex = EditorGUILayout.Popup(SelectedIndex, ListXmlFile.ToArray());
        if (GUILayout.Button("Load"))
        {
            ProcLoadBtn();
        }
    }

    void ProcLoadBtn()
    {
        if (ListXmlFile.Count > 0)
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
        string targetPath = ListXmlFile[SelectedIndex];
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
