using UnityEngine;
using System.Collections;

public class C4_MovingCheckAndSetActive : MonoBehaviour {

    public void startChecking()
    {
        StartCoroutine(checkMoving());
    }

    public void stopChecking()
    {
        StopCoroutine(checkMoving());
        gameObject.SetActive(false);
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
