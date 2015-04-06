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
    public int fullHP;
    public int moveSpeed;
    public int power;
    public int moveRange;
    public int fullGage;
    
    [System.NonSerialized]
    public int hp;
    
    public float gage;
    
    void Start()
    {
        gage = 0;
        hp = fullHP;
        GetComponent<C4_Move>().setMoveSpeed(moveSpeed);
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
            gage += Time.deltaTime * 50;
        }
    }
}
