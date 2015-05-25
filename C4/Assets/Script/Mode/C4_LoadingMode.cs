using UnityEngine;
using System.Collections;

public class C4_LoadingMode : C4_SceneMode {

	void Awake () 
	{
		C4_GameManager.Instance.LoadingScene();
	}
	
	public override void Start()
	{
		base.Start();
	}
}
