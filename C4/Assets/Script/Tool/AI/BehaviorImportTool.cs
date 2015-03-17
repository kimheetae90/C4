using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BehaviorImportTool : EditorWindow
{
	// Use this for initialization
    public List<string> listXmlFile = new List<string>();
	string[] listFilePath = {""};
    bool needUpdateRawDataPath = true;
    int selectedIndex = 0;

    public static string basePathToLoad = Application.dataPath + "/Data/AI/Raw";
	public static string basePathToSave = Application.dataPath + "/Data/AI/";
   
    BehaviorRawDataParser parser = new BehaviorRawDataParser();
	BehaviorRawDataSaver saver = new BehaviorRawDataSaver();


    [MenuItem("Window/AI/Behavior Import Tool")]
    public static void showWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(BehaviorImportTool));
    }

    void Update()
    {
        reloadProcess();
    }

    void reloadProcess()
    {
        if (needUpdateRawDataPath)
        {
            reloadRawDataDirectory();
            needUpdateRawDataPath = false;
        }
    }

    void OnProjectChange()
    {
        needUpdateRawDataPath = true;
    }

    private void reloadRawDataDirectory()
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

		listFilePath = listXmlFile.ToArray();
    }

    void OnGUI()
    {
        showParserUI();
    }

    void showParserUI()
    {
        GUILayout.Label("GXML 파일을 로드해주세요", EditorStyles.boldLabel);
		selectedIndex = EditorGUILayout.Popup(selectedIndex,listFilePath, EditorStyles.popup);

        if (GUILayout.Button("Load"))
        {
            procLoadBtn();
        }
    }

    void procLoadBtn()
    {
        if (listXmlFile.Count > 0)
        {
			parseRawDataAndSaveFilteredData();
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                  "읽을 파일이 존재하지 않습니다.", "OK");
        }
    }

	private void parseRawDataAndSaveFilteredData()
    {
		string targetPath = "";
		string savePath = ""; 

		try
		{
			getTargetAndSavePath(out targetPath,out savePath);
			parser.parseRawBehaviorData(targetPath);
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
		if(selectedIndex < listXmlFile.Count && listXmlFile.Count != 0)
		{
			targetPath = listXmlFile[selectedIndex];
			savePath = basePathToSave + Path.GetFileNameWithoutExtension(targetPath) + ".xml";
		}
		else
		{
			throw new BehaviorRawDataParseException("Parse Path is Invalid");
		}
	}
}
