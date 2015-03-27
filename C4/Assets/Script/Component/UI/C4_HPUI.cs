using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_HPUI : MonoBehaviour
{


    public Image imgHPbar;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;

    // Use this for initialization
    void Start()
    {
        boatFeature = transform.GetComponentInParent<C4_BoatFeature>();
    }

    // Update is called once per frame

    void Update()
    {
        imgHPbar.fillAmount = (float)boatFeature.hp / boatFeature.fullHP;
    }

}
