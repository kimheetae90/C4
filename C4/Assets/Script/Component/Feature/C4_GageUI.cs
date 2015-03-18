using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_GageUI : MonoBehaviour {

    public Image imgGageWhite;
    public Image imgGageYellow;
    public C4_BoatFeature boatFeature;

    public int gage;
    public int fullGage;
    public int stackCount;
    public int numOfStack;
	// Use this for initialization
	void Start () {

        boatFeature = transform.GetComponent<C4_BoatFeature>();
       // fullGage = boatFeature.fullGage;
        fullGage = boatFeature.oneGageStack * boatFeature.numOfStack;
        numOfStack = boatFeature.numOfStack;
	}
	
	// Update is called once per frame
	void Update () {
        
        gage = boatFeature.gage;
        stackCount = boatFeature.stackCount;
        imgGageWhite.fillAmount = (float)gage / fullGage;
        imgGageYellow.fillAmount = (float)stackCount / numOfStack;
	}
}
