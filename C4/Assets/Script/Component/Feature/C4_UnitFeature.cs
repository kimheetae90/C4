using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
///  배의 속성
///  gageDown : 행동으로 인한 게이지소모
///  gageUp : 게이지 충전
/// </summary>

public class C4_UnitFeature : MonoBehaviour
{

    public GameObject missile;
    public int attackRange;
    public int fullHP;
    public int moveSpeed;
    public int moveRange;
    public int gageChargingSpeed;
    public int rageFullGage;
    public int regeConsumeSpeed;
    public int rageGageChargeInAttack;
    public int rageGageChargeInDamage;
    public int rageGage;
    public readonly int fullGage = 300;

    //[System.NonSerialized]
    public bool israge;
    [System.NonSerialized]
    public int hp;
    [System.NonSerialized]
    public float gage;
    
    void Start()
    {
        gage = 0;
        hp = fullHP;
        rageGage = 0;
        israge = false;
		GetComponent<C4_Move>().setMoveSpeed(moveSpeed);
		StartUpdate ();
    }

    IEnumerator UpdateFeature()
    {
		yield return null;
        
		gageUp();
		StartUpdate ();
    }   

	void StartUpdate()
	{
		StartCoroutine ("UpdateFeature");
	}

	public void StopUpdate(float stopTime)
	{
		StopCoroutine ("UpdateFeature");
		Invoke ("StartUpdate", stopTime);
	}

    public void activeDone()
    {
        gage = 0;
    }

    void gageUp()
    {
        if (gage < fullGage)
        {
            gage += Time.deltaTime * gageChargingSpeed * 30;
        }
    }

    
    public void rageUp(int gagecharge)
    {
       
        if (israge == false)
        {
            rageGage += rageGageChargeInDamage;
            if (rageGage >= rageFullGage)
            {
                rageGage = rageFullGage;
                israge = true;
                modechange();
                StartCoroutine("ragemode");
            }
            GetComponentInChildren<C4_RageUI>().rageChanged();
        }
    }
    public void rageDown()
    {
        rageGage -= regeConsumeSpeed;
        if (rageGage <= 0)
        {
            israge = false;
        }
    }
    public void rageEnd()
    {
        rageGage = 0;
        israge = false;
        modechange();
        GetComponentInChildren<C4_RageUI>().rageChanged();
        StopCoroutine("ragemode");
    }
    IEnumerator ragemode()
    {

        yield return null;
        rageDown();
        GetComponentInChildren<C4_RageUI>().rageChanged();
        if (israge==false)
        {
            rageEnd();
        }
        
        if (israge == true)
        {
            StartCoroutine("ragemode");
        }
        else
        {
            StopCoroutine("ragemode");
        }
        

    }
    public void modechange()
    {
        C4_Unit type = transform.GetComponent<C4_Unit>();
        
        if (israge)
        {

            switch (type.GetType().ToString())
            {
                case "C4_WaterPark":
                    missile.GetComponent<C4_MissileFeature>().misslerange *= 2;
                    break;
                case "C4_Breaker": 
                    attackRange *=2;
                    missile.GetComponent<C4_MissileFeature>().power *= 2;
                    break;
            }
        }
        else
        {

            switch (type.GetType().ToString())
            {
                case "C4_WaterPark": 
                    missile.GetComponent<C4_MissileFeature>().misslerange /= 2;
                    break;
                case "C4_Breaker": 
                    attackRange /= 2;
                    missile.GetComponent<C4_MissileFeature>().power /= 2;
                    break;
            }
        }
    }
}
