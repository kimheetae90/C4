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

   
  

    //[System.NonSerialized]
    public int stackCount;
    [System.NonSerialized]
    public int hp;
    public int gage;
    public int fullGage;
    
    void Start()
    {
        gage = 0;
        hp = fullHP;
        fullGage = numOfStack * oneGageStack;
        stackCount = 0;
    }

    void Update()
    {
        gageUp();
        stackCount = gage / oneGageStack;

       
        
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

    }
}
