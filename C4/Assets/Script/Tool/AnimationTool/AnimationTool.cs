using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.IO;

//씬생성
//오브젝트배치
//저장
public class AnimationTool : EditorWindow
{
    // Use this for initialization
    public List<string> listFBXFile = new List<string>();

    bool needUpdateFBXDataPath = true;
    bool isOpenEditSceen = false;
    int selectedFBXFileIndex = 0;
    string[] listFBXFilePath = { "" };

    public static string basePathToLoad = Application.dataPath + "/Resources/Fbx";

    [MenuItem("Window/Animation/Animation Event Tool")]
    public static void showWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        var window = EditorWindow.GetWindow(typeof(AnimationTool));
        window.position = new Rect(200, 200, 200 , 200);
    }

    void Update()
    {
        reloadProcess();
    }

    void reloadProcess()
    {
        if (needUpdateFBXDataPath)
        {
            reloadFBXDirectory();
            needUpdateFBXDataPath = false;
        }
    }

    private void reloadFBXDirectory()
    {
        listFBXFile.Clear();
        var info = new DirectoryInfo(basePathToLoad);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".fbx")
            {
               
                listFBXFile.Add(file.Name);
            }
        }

        listFBXFilePath = listFBXFile.ToArray();
    }

    void OnProjectChange()
    {
        needUpdateFBXDataPath = true;
    }

    void OnGUI()
    {
        GUILayout.Label("Import", EditorStyles.boldLabel);
        GUILayout.Label("Select FBX File", EditorStyles.boldLabel);
        selectedFBXFileIndex = EditorGUILayout.Popup(selectedFBXFileIndex, listFBXFilePath, EditorStyles.popup);

        if (GUILayout.Button("Select"))
        {
            procLoadBtn();
        }

        if (GUILayout.Button("Save"))
        {
            isOpenEditSceen = false;
        }
    }

    void procLoadBtn()
    {
        if (listFBXFile.Count > 0)
        {
            EditorApplication.NewScene();
            //EditorApplication.OpenSceneAdditive(strScenePath);
            /*string strScenePath = "";
            if (strScenePath == null || !strScenePath.Contains(".unity"))
            {
                EditorUtility.DisplayDialog("Select Scene", "You Must Select a Scene!", "Ok");
                EditorApplication.Beep();
                return;
            } 
*/

          //  GameObject.findobject

/*
            if (assetPath.EndsWith("_model.fbx"))
            {
                string prefabPath = assetPath.Replace("_model.fbx", ".prefab");
                Object prefab = EditorUtility.CreateEmptyPrefab(prefabPath);
                // prefab.AddGameObject(gameObject); ?
            }
            */
            UnityEngine.Object pPrefab = Resources.LoadAssetAtPath("Assets/Resources/Fbx/" + listFBXFile[selectedFBXFileIndex],typeof(GameObject));
            var myPrefab = Instantiate(pPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			EditorApplication.isPlaying = true;
            isOpenEditSceen = true;
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                  "읽을 파일이 존재하지 않습니다.", "OK");
        }
    }
}
#endif