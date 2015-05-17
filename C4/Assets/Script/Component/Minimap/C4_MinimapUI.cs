using UnityEngine;
using System.Collections;

public class C4_MinimapUI : MonoBehaviour {

	public GameObject miniMapObject;
	public GameObject EnemyUnitUI;
	public GameObject AllyUnitUI;
	
	void Start()
	{
		miniMapObject.SetActive (true);
	}
	
}