using UnityEngine;
using System.Collections;

public class C4_MovingCheckAndSetActive : MonoBehaviour {

    public void startChecking()
    {
        StartCoroutine("checkMoving");
    }

    public void stopChecking()
    {
        StopCoroutine("checkMoving");
		gameObject.SetActive(false);
		try{  
			  gameObject.GetComponent<C4_MissileFeature> ().unit.gameObject.
			  transform.Find ("avoidCheckCollider").gameObject.SetActive (false);
               
		} catch (UnityException e){
				Debug.Log ("is not enemy");
		}
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
