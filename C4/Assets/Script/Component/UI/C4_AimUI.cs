using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AimUI : MonoBehaviour {
	
	
	public GameObject aimUIGameObject;
	
	public GameObject cannotActiveAimUIGameObject;
	
	void Start()
	{
		
		aimUIGameObject.SetActive(false);
		cannotActiveAimUIGameObject.SetActive(false);
	}
	
	public void showUI(Vector3 clickPosition, float maxAttackRange)
	{
		cannotActiveAimUIGameObject.SetActive(false);
		aimUIGameObject.SetActive(true);
		C4_Ally selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit;
		
		
		float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
		if (distance >= maxAttackRange) 
		{
			distance = maxAttackRange;
		}
		
		Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
		aimDirection.y = 0;

        aimUIGameObject.transform.rotation = Quaternion.LookRotation(aimDirection);
        //aimUIGameObject.transform.Rotate(Vector3.right, 90);
        aimUIGameObject.transform.localScale = new Vector3(1, 1, distance*0.1f);
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

        cannotActiveAimUIGameObject.transform.rotation = Quaternion.LookRotation(aimDirection);
        //cannotActiveAimUIGameObject.transform.Rotate(Vector3.right, 90);
        cannotActiveAimUIGameObject.transform.localScale = new Vector3(1, 1, distance*0.1f);
	}
	public void hideUI()
	{
		aimUIGameObject.SetActive(false);
		cannotActiveAimUIGameObject.SetActive (false);
	}
}