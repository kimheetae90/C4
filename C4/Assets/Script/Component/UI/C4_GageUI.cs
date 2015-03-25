using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_GageUI : MonoBehaviour {
    
    public Image imgGageWhite;
    
    public Image imgGageYellow;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;

   
	// Use this for initialization
	void Start () {

        boatFeature = transform.GetComponentInParent<C4_BoatFeature>();
  
	}
	
	// Update is called once per frame
	void Update () {


        imgGageWhite.fillAmount = (float)boatFeature.gage / boatFeature.fullGage;
        imgGageYellow.fillAmount = (float)boatFeature.stackCount / boatFeature.numOfStack;
	}
}
