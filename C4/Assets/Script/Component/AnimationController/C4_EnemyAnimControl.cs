using UnityEngine;
using System.Collections;

public class C4_EnemyAnimControl : MonoBehaviour {

	Animator anim;
	C4_Unit ally;
	C4_Move moveScript;
	
	void Start()
	{
		anim = GetComponentInChildren<Animator> ();
		ally = GetComponent<C4_Enemy> ();
		moveScript = GetComponent<C4_Move> ();
	}
	
	public void move()
	{
		if (ally.canActive)
		{
			anim.SetBool ("Move", true);
			StartCoroutine("checkMoving");
		}
	}
	
	IEnumerator checkMoving()
	{
		yield return null;
		
		if (moveScript.isMove) 
		{
			anim.SetBool ("Move", true);
			StartCoroutine("checkMoving");
		}
		else
		{
			anim.SetBool ("Move", false);
			if(moveScript.toMove == transform.position)
			{
				StopCoroutine ("checkMoving");
			}
			else
			{
				StartCoroutine("checkMoving");
			}
		}
	}
	
	public void shot()
	{
		if (ally.canActive) 
		{
			anim.SetTrigger ("Shot");
			anim.SetBool ("Aim", false);
		}
	}
	
	public void damaged()
	{
		anim.SetTrigger ("Damaged");
	}


}
