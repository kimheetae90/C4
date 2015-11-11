using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CMarkerController : Controller {

	List<GameObject> markerPrefabList;

	void Awake()
	{
		markerPrefabList = new List<GameObject> ();

		for (int i=0; i<10; i++) 
		{
			string prefabName = "marker" + i.ToString();
			GameObject prefab = Resources.Load("Tool/Prefabs/" + prefabName);
		}
	}

	protected override void Start ()
	{
		base.Start ();
	}
	
	public override void DispatchGameMessage (GameMessage _gameMessage)
	{

	}

}
