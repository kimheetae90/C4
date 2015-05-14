using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class C4_RageUI : MonoBehaviour
{

    public Image rageUIImage;
    public C4_UnitFeature unitFeature;
    // Use this for initialization
    void Start()
    {
        unitFeature = transform.GetComponentInParent<C4_UnitFeature>();
        rageUIImage.fillAmount = 0;
    }

    // Update is called once per frame
   
    
    public void rageChanged()
    {
        rageUIImage.fillAmount = (float)unitFeature.rageGage / unitFeature.rageFullGage;
    }
   
}
