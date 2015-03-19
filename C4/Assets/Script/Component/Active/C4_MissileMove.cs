using UnityEngine;
using System.Collections;

/// <summary>
///  미사일이 목표지점에 도달했을 때 사라지는 코드
///  방법 : 미사일의 isMove가 false일 때 Missile GameObject의 Active를 비활성화시키고 다시 배의 아래쪽으로 옮겨온다.
///  주의 : Update는 항상체크하므로 성능을 저하함, 움직일때만 사용되게 코루틴사용 
/// </summary>
public class C4_MissileMove : C4_Move
{
    [System.NonSerialized]
    public GameObject missile;
    // Use this for initialization
    void Start()
    {
        moveSpeed = GetComponent<C4_MissileFeature>().moveSpeed;
        missile = transform.gameObject;
    }

    public void startMove() // 진입하는 함수(Missile에서 호출)
    {
        setMoving();
        StartCoroutine("moveCheck");
    }

    IEnumerator moveCheck()
    {
        yield return null;
        if (isMove)
        {
            StartCoroutine("moveCheck");
        }
        else
        {
            missile.SetActive(false);
            transform.Translate(0, -15, 0);
            StopCoroutine("moveCheck");
        }
    }
}
