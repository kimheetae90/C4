using UnityEngine;
using System.Collections;

/// <summary>
///  미사일에 대한 정보
///  boat 아래에 -15만큼 위치하고 active 비활성화였다가 선택되면 활성화되고 발사된다.
///  다른 배가 선택되거나 미사일이 충돌되거나 목표지점에 다다르면 비활성화된다.
///  startMove : 이동 시작
/// </summary>

public class C4_MissileFeature : MonoBehaviour
{
    public int power;
    public float moveSpeed;
    public int type;
    public GameObject unit;
	public Vector3 startPosition;

    void Start()
    {
        GetComponent<C4_Move>().setMoveSpeed(moveSpeed);
    }
}
