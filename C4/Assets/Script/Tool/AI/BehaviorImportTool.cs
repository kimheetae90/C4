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

    public static string BasePathToLoad = Application.dataPath + "/Data/AI/Raw";
	public static string BasePathToSave = Application.dataPath + "/Data/AI/";
   
    BehaviorRawDataParser parser = new BehaviorRawDataParser();
	BehaviorRawDataSaver saver = new BehaviorRawDataSaver();

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
			ParseRawDataAndSaveFilteredData();
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                  "읽을 파일이 존재하지 않습니다.", "OK");
        }
    }

	private void ParseRawDataAndSaveFilteredData()
    {
		string targetPath = "";
		string savePath = ""; 

		try
		{
			getTargetAndSavePath(out targetPath,out savePath);
			parser.ParseRawBehaviorData(targetPath);
			saver.saveFileterdData(savePath,parser.getParsedRawNodeData(),parser.getParsedRawEdgeData());
		}
		catch(BehaviorRawDataParseException e)
		{
			EditorUtility.DisplayDialog("파일 읽기 실패",
			                            e.Message, "OK");
			return;
		}

		EditorUtility.DisplayDialog("파일 읽기 성공",
		                            "파일이 올바르게 저장되었습니다. 파일명 : " + savePath, "OK");
    }

	private void getTargetAndSavePath(out string targetPath, out string savePath)
	{
		if(SelectedIndex < ListXmlFile.Count && ListXmlFile.Count != 0)
		{
			targetPath = ListXmlFile[SelectedIndex];
			savePath = BasePathToSave + Path.GetFileNameWithoutExtension(targetPath) + ".xml";
		}
		else
		{
			throw new BehaviorRawDataParseException("Parse Path is Invalid");
		}
	}
}
