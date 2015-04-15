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
		attackUIGameObejct.SetActive(true);
		C4_Enemy selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().selectedEnemyUnit;

		attackUIImage.transform.position = selectedBoat.transform.position;
		attackUIImage.transform.localScale = new Vector3(1, 4, 0.1f);
	}
	
	public void hideUI()
	{
		attackUIGameObejct.SetActive(false);
	}
}
