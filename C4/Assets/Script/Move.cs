using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public float moveSpeed;
    Vector3 toMove;
    C4_Boat boatFeature;

    [System.NonSerialized]
    public bool isMove;
    bool isCoroutine;

	// Use this for initialization
    void Start()
    {
        boatFeature = transform.GetComponent<C4_Boat>();
        moveSpeed = boatFeature.moveSpeed;
        toMove = transform.position;
        isMove = false;
        isCoroutine = false;
	}

    public void setToMove(Vector3 click)
    {
        toMove = click;
        isMove = true;
        if (!isCoroutine)
        {
            StartCoroutine(move());
            isCoroutine = true;
        }
    }
    
    IEnumerator move()
    {
        yield return null;

        if (isMove)
        {
            float distance = Vector3.Distance(toMove, transform.position);
            if (distance > 0.5f)
            {
                transform.Translate((toMove - transform.position).normalized * moveSpeed * Time.deltaTime);
                StartCoroutine("move");
            }
            else
            {
                isMove = false;
                isCoroutine = false;
                StopCoroutine("move");
            }
        }
        else
        {
            isCoroutine = false;
            StopCoroutine("move");
        }
    }
}