using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class BehaviorImportTool : EditorWindow
{
	// Use this for initialization
    public List<string> listGXmlFile = new List<string>();
    public List<string> listFilteredXmlFile = new List<string>();

	string[] listRawFilePath = {""};
    string[] listFilteredFilePath = { "" };

    bool needUpdateRawDataPath = true;
    int selectedRawFileIndex = 0;
    int selectedFilterdFileIndex = 0;

    public static string basePathToLoad = Application.dataPath + "/Resources/Data/AI/Raw";
    public static string basePathToSave = Application.dataPath + "/Resources/Data/AI/";
   
    BehaviorRawDataParser parser = new BehaviorRawDataParser();
	BehaviorRawDataSaver saver = new BehaviorRawDataSaver();

    BehaviorNodeFactory builder = new BehaviorNodeFactory();

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
            reloadFilteredDataDirectory();
            needUpdateRawDataPath = false;
        }
    }

    void OnProjectChange()
    {
        needUpdateRawDataPath = true;
    }

    private void reloadRawDataDirectory()
    {
        listGXmlFile.Clear();
        var info = new DirectoryInfo(basePathToLoad);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".xgml")
            {
                listGXmlFile.Add(file.FullName);
            }
        }

		listRawFilePath = listGXmlFile.ToArray();
    }

    private void reloadFilteredDataDirectory()
    {
        listFilteredXmlFile.Clear();
        var info = new DirectoryInfo(basePathToSave);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".xml")
            {
                listFilteredXmlFile.Add(file.FullName);
            }
        }

        listFilteredFilePath = listFilteredXmlFile.ToArray();
    }

    void OnGUI()
    {
        showParserUI();
        showTestUI();
    }

    void showParserUI()
    {
        GUILayout.Label("GXML Import", EditorStyles.boldLabel);
        GUILayout.Label("  GXML 파일을 로드해주세요", EditorStyles.boldLabel);
        selectedRawFileIndex = EditorGUILayout.Popup(selectedRawFileIndex, listRawFilePath, EditorStyles.popup);

        if (GUILayout.Button("Load"))
        {
            procLoadBtn();
        }
    }

    void procLoadBtn()
    {
        if (listGXmlFile.Count > 0)
        {
            parseRawDataAndSaveFilteredData();
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                  "읽을 파일이 존재하지 않습니다.", "OK");
        }
    }

    void showTestUI()
    {
        GUILayout.Label("TEST CASE", EditorStyles.boldLabel);
        GUILayout.Label("  XML 파일을 로드해주세요", EditorStyles.boldLabel);
        selectedFilterdFileIndex = EditorGUILayout.Popup(selectedFilterdFileIndex, listFilteredFilePath, EditorStyles.popup);

        if (GUILayout.Button("Test"))
        {
            procTestBtn();
        }
    }

    void procTestBtn()
    {
        if (listGXmlFile.Count > 0)
        {
            try
            {
                builder.buildBehaviorNode(listFilteredFilePath[selectedFilterdFileIndex]);
            }
            catch(BehaviorNodeException e)
            {
                EditorUtility.DisplayDialog("파일 읽기 실패",
                                        e.Message, "OK");
                return;
            }

            EditorUtility.DisplayDialog("파싱 성공",
                                        "파일을 올바르게 파싱했습니다.", "OK");
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
		if(selectedRawFileIndex < listGXmlFile.Count && listGXmlFile.Count != 0)
		{
			targetPath = listGXmlFile[selectedRawFileIndex];
			savePath = basePathToSave + Path.GetFileNameWithoutExtension(targetPath) + ".xml";
		}
		else
		{
			throw new BehaviorRawDataParseException("Parse Path is Invalid");
		}
	}
}
#endif