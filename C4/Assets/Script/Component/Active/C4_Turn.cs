using UnityEngine;
using System.Collections;

/// <summary>
///  회전하는 기능의 스크립트
///  setToTurn : 회전할 방향을 계산하고 코루틴을 시작한다.
///  turn : 회전하는 코루틴, 회전 방향이 일치할 때까지 회전하며 회전이완료되면 코루틴이 종료된다.
/// </summary>

public class C4_Turn : MonoBehaviour {

    float turnSpeed;
    Quaternion toTurn;

    void Start()
    {
        toTurn = Quaternion.LookRotation(transform.forward);
        turnSpeed = 10f;	
	}
	
    public void setToTurn(Vector3 click)
	{
        
		toTurn = Quaternion.LookRotation((click - transform.position).normalized);
		toTurn.x = 0;
		toTurn.z = 0;
        StartCoroutine(turn());
	}

	IEnumerator turn()
	{
		yield return null;

		if (toTurn != Quaternion.LookRotation (transform.forward)) {
			transform.rotation = Quaternion.Lerp (transform.rotation, toTurn, turnSpeed * Time.deltaTime);
			StartCoroutine("turn");
		}
		else 
		{
			StopCoroutine("turn");
		}
	}
}