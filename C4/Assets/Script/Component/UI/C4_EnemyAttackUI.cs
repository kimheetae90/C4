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
	
	public void showUI(Vector3 AttackPosition)
	{
		attackUIGameObejct.SetActive(true);
		C4_Enemy selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().selectedEnemyUnit;
		float distance = Vector3.Distance(selectedBoat.transform.position, AttackPosition);
		Vector3 attackDirection = (selectedBoat.transform.position - AttackPosition).normalized;
		attackDirection.y = 0;
		
		attackUIImage.transform.position = selectedBoat.transform.position;
		attackUIImage.transform.rotation = Quaternion.LookRotation(attackDirection);
		attackUIImage.transform.Rotate(Vector3.right, 90);
		attackUIImage.transform.localScale = new Vector3(0.5f, distance / 2, 1);
	}
	
	public void hideUI()
	{
		attackUIGameObejct.SetActive(false);
	}
}
