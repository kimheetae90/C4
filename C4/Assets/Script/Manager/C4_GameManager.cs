using UnityEngine;
using System.Collections;

public class C4_GameManager : MonoBehaviour {

    private static C4_GameManager _instance;
    public static C4_GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_GameManager)) as C4_GameManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
					container.name = "C4_GameManager";
                    _instance = container.AddComponent(typeof(C4_GameManager)) as C4_GameManager;
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
    public C4_SceneMode sceneMode;
	[System.NonSerialized]
	public SelectedAlly selectedAlly;


    private bool isPlaying;
    
    public bool IsPlaying
    {
        get { return isPlaying; }
    }

	void Awake()
	{
        isPlaying = false;
		selectedAlly = new SelectedAlly(2);
		LoadingScene ();
	}

	public void LoadingScene()
	{
		inputManager = GameObject.Find("InputManager").GetComponent<C4_InputManager>();
		objectManager = GameObject.Find("ObjectManager").GetComponent<C4_ObjectManager>();
	}

	public void StartLoadingMode()
	{
		sceneMode = GameObject.Find ("LoadingMode").GetComponent<C4_LoadingMode> ();
	}

	public void StartSelectAllyMode()
	{
		sceneMode = GameObject.Find ("SelectAllyMode").GetComponent<C4_SelectAllyMode> ();
	}

    public void StartPlayMode()
	{
		LoadingScene ();
        sceneMode = GameObject.Find("PlayMode").GetComponent<C4_SceneMode>();
        isPlaying = true;
    }
}
