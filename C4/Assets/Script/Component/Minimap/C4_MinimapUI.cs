using UnityEngine;
using System.Collections;

public class C4_MinimapUI : MonoBehaviour {

	public GameObject miniMapObject;
	public GameObject EnemyUnitUI;
	public GameObject AllyUnitUI;

	bool minimapSwitch;

	void Start()
	{
		miniMapObject.SetActive (true);
		minimapSwitch = true;
	}

	public void OnOffMinimap(){
		if(minimapSwitch){		// Off
			gameObject.SetActive (false);
			minimapSwitch = false;
		}
		else{					// On
			gameObject.SetActive (true);
			minimapSwitch = true;
		}
	}

}