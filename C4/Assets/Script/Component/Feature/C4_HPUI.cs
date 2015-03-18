using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_HPUI : MonoBehaviour {

    public Image imgHPbar;
    public C4_BoatFeature boatFeature;
    public int hp;
    public int fullHP;

	// Use this for initialization
	void Start () {
        boatFeature = transform.GetComponent<C4_BoatFeature>();
        fullHP = boatFeature.fullHP;
	}
	
	// Update is called once per frame
	void Update () {
        hp = boatFeature.hp;
        imgHPbar.fillAmount = (float)hp / fullHP;
	}
}
