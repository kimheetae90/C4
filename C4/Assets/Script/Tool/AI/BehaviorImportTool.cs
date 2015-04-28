using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class BehaviorImportTool : EditorWindow
{
	AssetsSelectFilesInfo gxmls = new AssetsSelectFilesInfo();
	AssetsSelectFilesInfo filteredXmls = new AssetsSelectFilesInfo();

	public static string basePathToLoad = Application.dataPath + "/Resources/Data/AI/Raw/";
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
		if (gxmls.bNeedUpdate) {
			reloadRawDataDirectory();
			gxmls.bNeedUpdate = false;
		}

		if (filteredXmls.bNeedUpdate) {
			reloadFilteredDataDirectory();
			filteredXmls.bNeedUpdate = false;
		}
    }

    void OnProjectChange()
    {
		gxmls.bNeedUpdate = true;
		filteredXmls.bNeedUpdate = true;
    }

    private void reloadRawDataDirectory()
    {
        gxmls.listFilePaths.Clear();

		PathUtility.DirSearch(basePathToLoad, ".xgml", ref gxmls.listFilePaths);

		gxmls.strArrayFilePaths = gxmls.listFilePaths.ToArray();
    }

    private void reloadFilteredDataDirectory()
    {
		filteredXmls.listFilePaths.Clear();
		
		PathUtility.DirSearch(basePathToSave, ".xml", ref filteredXmls.listFilePaths);
		
		filteredXmls.strArrayFilePaths = filteredXmls.listFilePaths.ToArray();
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

		gxmls.selectedFileIndex = EditorGUILayout.Popup(gxmls.selectedFileIndex, gxmls.strArrayFilePaths, EditorStyles.popup);

        if (GUILayout.Button("Load"))
        {
            procLoadBtn();
        }
    }

    void procLoadBtn()
    {
		if (gxmls.listFilePaths.Count > 0)
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
		filteredXmls.selectedFileIndex = EditorGUILayout.Popup(filteredXmls.selectedFileIndex, filteredXmls.strArrayFilePaths, EditorStyles.popup);

        if (GUILayout.Button("Test"))
        {
            procTestBtn();
        }
    }

    void procTestBtn()
    {
        if (filteredXmls.listFilePaths.Count > 0)
        {
            try
            {
				string path = filteredXmls.listFilePaths[filteredXmls.selectedFileIndex].Replace("Assets/Resources/","");
				path = path.Replace(".xml","");
				builder.buildBehaviorNode(path);
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
		catch(ToolException e)
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
		if(gxmls.selectedFileIndex < gxmls.listFilePaths.Count && gxmls.listFilePaths.Count != 0)
		{
			targetPath = gxmls.listFilePaths[gxmls.selectedFileIndex];
			savePath = basePathToSave + Path.GetFileNameWithoutExtension(targetPath) + ".xml";
		}
		else
		{
			throw new ToolException("Parse Path is Invalid");
		}
	}
}
#endif