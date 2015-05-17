using UnityEngine;
using System.Collections;

public class C4_MinimapUnit : MonoBehaviour {

	public GameObject myBoat;
	C4_MinimapUI minimapUI;
	float rate;

	void Start()
	{
		minimapUI = GameObject.Find("Minimap").GetComponent<C4_MinimapUI>();
		rate = (0.4f);
	}

	void Update()
	{
		if (myBoat != null) {
			transform.position = minimapUI.miniMapObject.transform.position
				+ new Vector3 (myBoat.transform.position.x * (rate), myBoat.transform.position.z * (rate), 0);
		}

		if( myBoat == null)
			this.gameObject.SetActive (false);
	}


}
