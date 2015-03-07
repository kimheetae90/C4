﻿using UnityEngine;
using System.Collections;

/// <summary>
///  배가 moveRage만큼 이동할 때 마다 gageBlock 소모하게 만들기
///  방법 : 이 클래스 내에서 이너머레이터로 이동한 거리를 체크하여 이동한 거리가 moveRage의 크기만큼 도달했을 때 블럭을 소모한다
///  팁 : 거리 체크후 해당하는 배의 C4_Boat 스크립트를 가져와서 gageDown함수 호출
///  주의 : Update는 항상체크하므로 성능을 저하함, 움직일때만 사용되게 코루틴사용 
/// </summary>


public class C4_BoatMove : Move {

    [System.NonSerialized]
    public C4_Boat boat;

	// Use this for initialization
	void Start () {
        boat = transform.GetComponent<C4_Boat>();
	}
	
    public void startMove(Vector3 toMove)  // 진입하는 함수(Boat에서 호출)
    {
        setToMove(toMove);
    }
}
