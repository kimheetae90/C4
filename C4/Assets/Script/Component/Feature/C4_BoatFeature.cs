using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
///  배의 속성
///  gageDown : 행동으로 인한 게이지소모
///  gageUp : 게이지 충전
/// </summary>

public class C4_BoatFeature : MonoBehaviour
{
    public GameObject missile;
    public int numOfStack;
    public int fullHP;
    public int oneGageStack;
    public int moveSpeed;
    public int power;
    public int needGageStackToMove;
    public int needGageStackToShot;
    public int moveRange;
    
    [System.NonSerialized]
    public int stackCount;
    [System.NonSerialized]
    public int hp;
    [System.NonSerialized]
    public int gage;
    [System.NonSerialized]
    public int fullGage;
    
    void Start()
    {
        gage = 0;
        hp = fullHP;
        fullGage = numOfStack * oneGageStack;
        stackCount = 0;
        GetComponent<C4_Move>().setMoveSpeed(moveSpeed);
    }

    void Update()
    {
        gageUp();
        stackCount = gage / oneGageStack;
    }   

    public void gageDown(int gageStack)
    {
        gage -= gageStack*oneGageStack;
    }

    void gageUp()
    {
        if (gage < fullGage)
        {
            gage++;
        }

    }
}
