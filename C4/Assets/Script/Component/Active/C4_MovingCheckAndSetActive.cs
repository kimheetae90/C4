using UnityEngine;
using System.Collections;

public class C4_MovingCheckAndSetActive : MonoBehaviour {

	GameObject avoidCollider;

    public void startChecking()
    {
        StartCoroutine("checkMoving");
    }

    public void stopChecking()
    {
        StopCoroutine("checkMoving");
		gameObject.SetActive(false);
		//나중에 고쳐야함
		avoidCollider = GetComponent<C4_MissileFeature> ().unit.gameObject.
			transform.Find ("avoidCheckCollider").gameObject;

		avoidCollider.SetActive (false);

    }

    IEnumerator checkMoving()
    {
        yield return null;

        if (GetComponent<C4_Move>().isMove)
        {
            startChecking();
        }
        else
        {
            stopChecking();
        }
    }
}
