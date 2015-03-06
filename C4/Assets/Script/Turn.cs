using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {

    float turnSpeed;
    Quaternion toTurn;

	// Use this for initialization
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