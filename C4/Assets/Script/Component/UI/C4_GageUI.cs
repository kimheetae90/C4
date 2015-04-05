using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_GageUI : MonoBehaviour {

    public Image gageUIImage;
    C4_UnitFeature unitFeature;

   
	// Use this for initialization
	void Start () {
        unitFeature = transform.GetComponentInParent<C4_UnitFeature>();
	}
	
	// Update is called once per frame
	void Update () {
        gageUIImage.fillAmount = (float)unitFeature.gage / unitFeature.fullGage;
	}
}
