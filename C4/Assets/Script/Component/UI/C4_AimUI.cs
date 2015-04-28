using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AimUI : MonoBehaviour {
	
	public Image aimUIImage;
	GameObject aimUIGameObject;
	
	public Image cannotActiveAimUIImage;
	GameObject cannotActiveAimUIGameObject;
	
	void Start()
	{
		aimUIGameObject = aimUIImage.gameObject;
		aimUIGameObject.SetActive(false);
		
		cannotActiveAimUIGameObject = cannotActiveAimUIImage.gameObject;
		cannotActiveAimUIGameObject.SetActive(false);
	}
	
	public void showUI(Vector3 clickPosition, float maxAttackRange)
	{
		cannotActiveAimUIGameObject.SetActive(false);
		aimUIGameObject.SetActive(true);
		C4_Ally selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit;
		
		
		float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
		if (distance >= maxAttackRange)
			distance = maxAttackRange;
		
		Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
		aimDirection.y = 0;
		
		aimUIImage.transform.position = selectedBoat.transform.position;
		aimUIImage.transform.rotation = Quaternion.LookRotation(-aimDirection);
		aimUIImage.transform.Rotate(Vector3.right, 90);
		aimUIImage.transform.localScale = new Vector3(1, distance / 2, 1);
	}
	
	public void showCannotActiveUI(Vector3 clickPosition, float maxAttackRange)
	{
		cannotActiveAimUIGameObject.SetActive(true);
		aimUIGameObject.SetActive(false);
		C4_Ally selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit;
		float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
		if (distance >= maxAttackRange)
			distance = maxAttackRange;
		
		Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
		aimDirection.y = 0;
		
		cannotActiveAimUIImage.transform.position = selectedBoat.transform.position;
		cannotActiveAimUIImage.transform.rotation = Quaternion.LookRotation(-aimDirection);
		cannotActiveAimUIImage.transform.Rotate(Vector3.right, 90);
		cannotActiveAimUIImage.transform.localScale = new Vector3(1, distance / 2, 1);
	}
	public void hideUI()
	{
		aimUIGameObject.SetActive(false);
		cannotActiveAimUIGameObject.SetActive (false);
	}
}