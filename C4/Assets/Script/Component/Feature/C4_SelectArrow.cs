using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_SelectArrow : MonoBehaviour {

    public Canvas playerArrow;
    public bool isActive;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
        
        if (C4_PlayManager.Instance.selectedBoat != null)
        {
            Debug.Log("select");
            //this.gameObject.SetActive(true);
            Vector3 pos = C4_PlayManager.Instance.ourBoat.transform.position;
            pos.y += 6.5f;
            pos.z += 11;
            
            playerArrow.transform.position = pos;
            
            
        }
        else
        {
            Debug.Log("cancle");
            enabled = false;
        }
        
        
	}
}
