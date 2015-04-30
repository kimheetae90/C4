using UnityEngine;
using System.Collections;

public class C4_AnimControl : MonoBehaviour, C4_IControllerListener {

	Animator anim;
	C4_Ally ally;
	C4_Move moveScript;

	void Start()
	{
		anim = GetComponentInChildren<Animator> ();
		ally = GetComponent<C4_Ally> ();
		moveScript = GetComponent<C4_Move> ();
	}

	public void aim()
	{
		if (ally.canActive) 
		{
			anim.SetBool ("Aim", true);
		}
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

		if(!moveScript.isMove)
		{
			anim.SetBool("Move",false);
			StopCoroutine("checkMoving");
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

	public void onEvent(string message, params object[] p)
	{
		switch(message)
		{
		case "Aim":
		{
			aim ();
		}
			break;
		case "Move":
		{
			move ();
		}
			break;
		case "Shot":
		{
			shot();
		}
			break;
		}
	}


}
