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
   
    public void rageUpAtt()
    {

        if (unitFeature.israge == false)
        {
            
            unitFeature.rageGage += unitFeature.rageGageChargeInAttack;
            
            if (unitFeature.rageGage >= unitFeature.rageFullGage)
            {
                
                unitFeature.rageGage = unitFeature.rageFullGage;
                unitFeature.israge = true;
                StartCoroutine("ragemode");

            }
            rageChanged();
        }
    }
    public void rageUpDmg()
    {
        if (unitFeature.israge == false)
        {
            unitFeature.rageGage += unitFeature.rageGageChargeInDamage;
            if (unitFeature.rageGage >= unitFeature.rageFullGage)
            {
                unitFeature.rageGage = unitFeature.rageFullGage;
                unitFeature.israge = true;
               
            }
            rageChanged();
        }
    }
    public void rageChanged()
    {
        rageUIImage.fillAmount = (float)unitFeature.rageGage / unitFeature.rageFullGage;
        
        


    }
    public void rageDown()
    {
        unitFeature.rageGage -= unitFeature.regeConsumeSpeed;
        if (unitFeature.rageGage <= 0)
        {
            unitFeature.rageGage = 0;
            unitFeature.israge = false;
        }
    }
    public void rageEnd()
    {
        unitFeature.rageGage = 0;
        unitFeature.israge = false;
        rageChanged();
        StopCoroutine("ragemode");
    }
    IEnumerator ragemode()
    {
        yield return null;

        rageDown();
        rageChanged();
        if (unitFeature.israge == true)
        {
            StartCoroutine("ragemode");
        }
        else
        {
            StopCoroutine("ragemode");
        }

    }
}
