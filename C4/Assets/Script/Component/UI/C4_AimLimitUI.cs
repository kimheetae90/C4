using UnityEngine;
using System.Collections;

public class C4_AimLimitUI : MonoBehaviour {

    public GameObject aimlimit;

	// Use this for initialization
	void Start () {
        hideUI();
	}
	
	// Update is called once per frame

    public void showUI(float maxrange)
    {
        aimlimit.transform.localScale = new Vector3(maxrange, 1, maxrange);
        aimlimit.SetActive(true);
    }

    public void hideUI()
    {
        aimlimit.SetActive(false);
    }
}
