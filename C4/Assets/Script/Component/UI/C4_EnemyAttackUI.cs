using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_EnemyAttackUI : MonoBehaviour {
	
	public Image attackUIImage;
	GameObject attackUIGameObejct;
	
	void Start()
	{
		attackUIGameObejct = attackUIImage.gameObject;
		attackUIGameObejct.SetActive(false);
	}
	
	public void showUI()
	{
		Invoke ("hideUI", 1f);
		attackUIGameObejct.SetActive(true);
	}
	
	public void hideUI()
	{
		attackUIGameObejct.SetActive(false);
	}
}
