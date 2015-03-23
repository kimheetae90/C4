using UnityEngine;
using System.Collections;

public class C4_ManagerMaster : MonoBehaviour {

    private static C4_ManagerMaster _instance;
    public static C4_ManagerMaster Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_ManagerMaster)) as C4_ManagerMaster;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_ManagerMaster";
                    _instance = container.AddComponent(typeof(C4_ManagerMaster)) as C4_ManagerMaster;
                }
            }

            return _instance;
        }
    }   

    [System.NonSerialized]
    public C4_InputManager inputManager;
    [System.NonSerialized]
    public C4_ObjectManager objectManager;
    [System.NonSerialized]
    public C4_SceneManager sceneManager;

    public void StartPlayScene()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<C4_InputManager>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<C4_ObjectManager>();
        sceneManager = GameObject.Find("PlayManager").GetComponent<C4_SceneManager>();
    }
}
