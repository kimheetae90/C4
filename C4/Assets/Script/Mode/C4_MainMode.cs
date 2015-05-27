using UnityEngine;
using System.Collections;

public class C4_MainMode : C4_SceneMode {

	void Awake()
	{
		C4_GameManager.Instance.StartMainMode();
	}
	
	public override void Start()
	{
		base.Start();
	}
}
