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
    int selectedFBXFileIndex = 0;
    string[] listFBXFilePath = { "" };

    public static string basePathToLoad = Application.dataPath + "/Resources/Fbx";

    [MenuItem("Window/Animation/Animation Event Tool")]
    public static void showWindow()
    {
        var window = EditorWindow.GetWindow(typeof(AnimationTool));
        window.position = new Rect(200, 200, 200 , 200);
    }
	
    void Update()
    {
        reloadProcess();
		
		updateCamera();
    }

    void reloadProcess()
    {
        if (needUpdateFBXDataPath)
        {
            reloadFBXDirectory();
            needUpdateFBXDataPath = false;
        }
    }
	
	void updateCamera ()
	{
		if (EditorApplication.isPlaying && SceneView.currentDrawingSceneView != null) {
			Camera.main.transform.position = SceneView.currentDrawingSceneView.camera.transform.position;
			Camera.main.transform.LookAt (SceneView.currentDrawingSceneView.pivot);
		}
	}
	
    void reloadFBXDirectory()
    {
        listFBXFile.Clear();
        
		PathUtility.DirSearch(basePathToLoad, ".fbx",ref listFBXFile);

        listFBXFilePath = listFBXFile.ToArray();
    }

    void OnProjectChange()
    {
        needUpdateFBXDataPath = true;
    }



    void OnGUI()
    {
        GUILayout.Label("Select FBX File", EditorStyles.boldLabel);
        selectedFBXFileIndex = EditorGUILayout.Popup(selectedFBXFileIndex, listFBXFilePath, EditorStyles.popup);

        if (GUILayout.Button("Select"))
        {
            procLoadBtn();
        }
    }

    void procLoadBtn()
    {
        if (listFBXFile.Count > 0)
        {
            EditorApplication.NewScene();
			UnityEngine.Object pPrefab = Resources.LoadAssetAtPath(listFBXFile[selectedFBXFileIndex],typeof(GameObject));

			GameObject fbx = Instantiate(pPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

			fbx.AddComponent(typeof(AnimationEventEditor));

			Camera.main.nearClipPlane = 0.1f;

			EditorApplication.isPlaying = true;
        }
        else
        {
            EditorUtility.DisplayDialog("파일 읽기 실패",
                  "읽을 파일이 존재하지 않습니다.", "OK");
        }
    }
}
#endif