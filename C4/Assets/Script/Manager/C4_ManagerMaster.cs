using UnityEngine;
using System.Collections;

public class C4_ManagerMaster : MonoBehaviour {
    public static C4_ManagerMaster _instance;
    public static C4_ManagerMaster Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(C4_ManagerMaster)) as C4_ManagerMaster;

                if (_instance == null)
                    Debug.LogError("Not Found Manager !!");
            }
            return _instance;
        }
    }

    public C4_InputManager inputManager;
    public C4_ObjectManager objectManager;
    public C4_SceneManager sceneManager;

    void StartPlayScene()
    {
        inputManager = new C4_InputManager();
        objectManager = new C4_ObjectManager();
        sceneManager = new C4_PlayManager();
    }
}
