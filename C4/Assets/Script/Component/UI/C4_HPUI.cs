using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_HPUI : MonoBehaviour
{
    public Image hpUIImage;
    [System.NonSerialized]
    public C4_UnitFeature unitFeature;

    // Use this for initialization
    void Start()
    {
        unitFeature = transform.GetComponentInParent<C4_UnitFeature>();
    }

    // Update is called once per frame

    void Update()
    {
        hpUIImage.fillAmount = (float)unitFeature.hp / unitFeature.fullHP;
    }

}
