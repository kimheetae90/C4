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
    
    enum eAnimationFileType
    {
        eAnimationFileType_FBX,
        eAnimationFileType_Controller,
        eAnimationFileType_Count,
    }

    AssetsSelectFilesInfo fbxFiles = new AssetsSelectFilesInfo();
    AssetsSelectFilesInfo controllerFiles = new AssetsSelectFilesInfo();
   
    public static string basePathToLoad = Application.dataPath + "/Resources/Fbx";

    [MenuItem("Window/Animation/Animation Event Tool")]
    public static void showWindow()
    {
        var window = EditorWindow.GetWindow(typeof(AnimationTool));

        window.position = new Rect(200, 200, 200, 200);
    }

    void Update()
    {
        reloadProcess();

        updateCamera();
    }

    void reloadProcess()
    {
        if (fbxFiles.bNeedUpdate)
        {
            reloadFBXDirectory();
            fbxFiles.bNeedUpdate = false;
        }

        if (controllerFiles.bNeedUpdate)
        {
            reloadAnimController();
            controllerFiles.bNeedUpdate = false;
        }
    }

    void updateCamera()
    {
        if (EditorApplication.isPlaying && SceneView.currentDrawingSceneView != null)
        {
            Camera.main.transform.position = SceneView.currentDrawingSceneView.camera.transform.position;
            Camera.main.transform.LookAt(SceneView.currentDrawingSceneView.pivot);
        }
    }

    void reloadFBXDirectory()
    {
        fbxFiles.listFilePaths.Clear();

        PathUtility.DirSearch(basePathToLoad, ".fbx", ref fbxFiles.listFilePaths);

        fbxFiles.strArrayFilePaths = fbxFiles.listFilePaths.ToArray();
    }

    void reloadAnimController()
    {
        controllerFiles.listFilePaths.Clear();
        
        PathUtility.DirSearch(basePathToLoad, ".controller", ref controllerFiles.listFilePaths);

        controllerFiles.strArrayFilePaths = controllerFiles.listFilePaths.ToArray();
    }

    void OnProjectChange()
    {
        setDirty();

    }

    void setDirty()
    {
        controllerFiles.bNeedUpdate = true;
        fbxFiles.bNeedUpdate = true;
    }

    void OnGUI()
    {
        GUILayout.Label("Select FBX File", EditorStyles.boldLabel);

        fbxFiles.selectedFileIndex = EditorGUILayout.Popup(fbxFiles.selectedFileIndex, fbxFiles.strArrayFilePaths, EditorStyles.popup);

        if (fbxFiles.selectedFileIndex >= 0 && fbxFiles.isValidIndex())
        {
            GUILayout.Label("Select AnimationController", EditorStyles.boldLabel);

            controllerFiles.selectedFileIndex = EditorGUILayout.Popup(controllerFiles.selectedFileIndex, controllerFiles.strArrayFilePaths, EditorStyles.popup);

            if (GUILayout.Button("Select") && controllerFiles.isValidIndex())
            {
                procLoadBtn();
            }
        }

        if (GUILayout.Button("Reload"))
        {
            setDirty();
        }
    }

    void procLoadBtn()
    {
        if (fbxFiles.listFilePaths.Count > 0 && controllerFiles.listFilePaths.Count > 0)
        {
            EditorApplication.NewScene();

            UnityEngine.Object prefab = Resources.LoadAssetAtPath(fbxFiles.listFilePaths[fbxFiles.selectedFileIndex], typeof(GameObject));
            RuntimeAnimatorController controller = Resources.LoadAssetAtPath(controllerFiles.listFilePaths[controllerFiles.selectedFileIndex], typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

            GameObject fbx = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            Animator anim = fbx.GetComponent<Animator>();

            fbx.AddComponent(typeof(AnimationEventEditor));
            fbx.AddComponent(typeof(Animation));

            anim.runtimeAnimatorController = controller;

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