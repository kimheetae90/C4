using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_GageUI : MonoBehaviour {
    [System.NonSerialized]
    public Image imgGageWhite;
    [System.NonSerialized]
    public Image imgGageYellow;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;
    [System.NonSerialized]
    public int gage;
    [System.NonSerialized]
    public int fullGage;
    [System.NonSerialized]
    public int stackCount;
    [System.NonSerialized]
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
