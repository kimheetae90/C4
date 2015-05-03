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

    [System.NonSerialized]
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
        missile.transform.SetParent(this.gameObject.transform);
    }

    void Update()
    {
        gageUp();
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
}
