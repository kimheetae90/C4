using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	//////////////////////////////////////////////////////////////////////////////
	private static GameMaster _instance;
	public static GameMaster Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
				if (!_instance)
				{
					GameObject container = new GameObject();
					container.name = "GameMaster";
					_instance = container.AddComponent(typeof(GameMaster)) as GameMaster;
				}
			}
			
			return _instance;
		}
	}   
	//////////////////////////////////////////////////////////////////////////////

	SceneManager sceneManager;
	InputHelper inputHelper;
	[System.NonSerialized]
	public GameMessagePooler gameMessageManager;

	public GameMessage tempData;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
		gameMessageManager = new GameMessagePooler ();
		tempData = new GameMessage ();
		inputHelper = InputHelper.Instance;
	}
	
	void Start()
	{
		this.sceneManager = GameObject.FindObjectOfType (typeof(SceneManager)) as SceneManager;
	}

	public SceneManager GetSceneManager()
	{
		return sceneManager;
	}

	public void SetSceneManager(SceneManager _sceneManager)
	{
		sceneManager = _sceneManager;
	}
}
