using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class C4_TargetUI : MonoBehaviour {

    public Image targetUIImage;
	// Use this for initialization
    void Start()
    {
        targetUIImage.gameObject.SetActive(false);
    }
	
	// Update is called once per frame

    public abstract void showUI(Vector3 targetPos);
    
    public void hideUI()
    {
        targetUIImage.gameObject.SetActive(false);
    }
}
