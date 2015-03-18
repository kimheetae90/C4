using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_HPUI : MonoBehaviour {
    
    
    public Image imgHPbar;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;
    [System.NonSerialized]
    public int hp;
    [System.NonSerialized]
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
