using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AutomoveCancelButton : MonoBehaviour {

    
   	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame

    public void showButton()
    {
        this.gameObject.SetActive(true);
        
    }

    public void hideButton()
    {
        this.gameObject.SetActive(false);
    }
    
}
