using UnityEngine;
using System.Collections;

/// <summary>
///  배의 속성
///  gageDown : 행동으로 인한 게이지소모
///  gageUp : 게이지 충전
/// </summary>

public class C4_Boat : C4_Object
{
    public GameObject missile;
    public int numOfStack;
    public int fullHP;
    public int fullGage;
    public int moveSpeed;
    public int power;
    public int needGageStackToMove;
    public int needGageStackToShot;
    public int moveRangeOfOneStack;

    [System.NonSerialized]
    public int stackCount;
    [System.NonSerialized]
    public int hp;
    [System.NonSerialized]
    public int oneGageStack;
    int gage;
    
    
    void Start()
    {
        gage = 0;
        hp = fullHP;
        oneGageStack = fullGage/numOfStack;
        stackCount = 0;
    }

    void Update()
    {
        gageUp();
    }   

    /* 행동을 하였을 때 gageStack만큼 gage를 감소시키는 함수 */
    public void gageDown(int gageStack)
    {
        gage -= gageStack*oneGageStack;
    }


    /* 지속적으로 gage를 올려주면서 이동가능여부, 발포가능여부를 체크 */
    void gageUp()
    {
        if (gage < fullGage)
        {
            gage++;
        }

        stackCount = gage / oneGageStack;
    }
}
