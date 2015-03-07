using UnityEngine;
using System.Collections;

/// <summary>
///  배의 속성
///  게이지를 지속적으로 올려주면서 canMove, canShot의 상태를 체크한다.
///  기본적으로 움직임, 발포에 대한 기능을 담고 있다.
///  shot : 발포
///  startMove : 이동시작
///  startTurn : 회전시작
///  damaged : 피해받음
///  gageDown : 행동으로 인한 게이지소모
///  gageUp : 게이지 충전
/// </summary>

public class C4_Boat : C4_Object
{
    public GameObject missile;
    public int numOfBlock;
    public int fullHP;
    public int fullGage;
    public int needGageBlockToMove;
    public int needGageBlockToShot;
    public int moveRageOfOneBlock;

    [System.NonSerialized]
    public C4_Missile missileFeature;
    [System.NonSerialized]
    public C4_BoatMove moveScript;
    [System.NonSerialized]
    public Turn turnScript;
    [System.NonSerialized]
    public bool canMove;
    [System.NonSerialized]
    public bool canShot;

    int gage;
    int hp;
    int oneGageBlock;

    Vector3 missileToMove;
    Vector3 shotDirection;   


    void Start()
    {
        moveScript = transform.GetComponent<C4_BoatMove>();
        turnScript = transform.GetComponentInChildren<Turn>();
        missileFeature = missile.GetComponent<C4_Missile>();
        gage = 0;
        hp = fullHP;
        oneGageBlock = fullGage/numOfBlock;
        canMove = false;
        canShot = false;
    }

    void Update()
    {
        gageUp();
    }

    /* 발포 함수 */
    public void shot(Vector3 click)
    {
        missile.transform.position = transform.position + (transform.position - click).normalized * (transform.localScale.z + missile.transform.localScale.z + 1);
        shotDirection = (transform.position - click).normalized;
        missileToMove = 4 * transform.position - 3 * click;
        shotDirection.y = 0;
        missileToMove.y = 0;
        missile.transform.GetChild(0).transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
        missileFeature.startMove(missileToMove);
        gageDown(needGageBlockToShot);
    }

    /* 이동함수 */
    public void startMove(Vector3 toMove)
    {
        moveScript.startMove(toMove);
    }

    /* 방향전환함수 */
    public void startTurn(Vector3 toMove)
    {
        turnScript.setToTurn(toMove);
    }


    /* 공격을 받았을 때 damage만큼 피해를 입는 함수 */
    public void damaged(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            if (transform.CompareTag("enemy"))
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.gameObject.SetActive(false);
            }
        }
    }


    /* 행동을 하였을 때 gageBlock만큼 gage를 감소시키는 함수 */
    public void gageDown(int gageBlock)
    {
        if (gage >= gageBlock * oneGageBlock)
        {
            gage -= gageBlock*oneGageBlock;
        }

        if (gage < needGageBlockToMove * oneGageBlock)
        {
            canMove = false;
        }

        if (gage < needGageBlockToShot * oneGageBlock)
        {
            canShot = false;
        }
    }


    /* 지속적으로 gage를 올려주면서 이동가능여부, 발포가능여부를 체크 */
    void gageUp()
    {
        if (gage > needGageBlockToMove * oneGageBlock)
        {
            canMove = true;
        }

        if (gage > needGageBlockToShot * oneGageBlock)
        {
            canShot = true;            
        }

        if (gage < fullGage)
        {
            gage++;
        }
    }
}
