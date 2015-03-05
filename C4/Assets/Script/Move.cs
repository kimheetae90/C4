using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {


    public float moveSpeed;
    public Turn turning;
    Vector3 toMove;

    [System.NonSerialized]
    public bool isMove;

	// Use this for initialization
    void Start()
    {
        toMove = transform.position;
        isMove = false;	
	}

    void setToMove(Vector3 click, float speed)
    {
        moveSpeed = speed;
        toMove = click;
        isMove = true;
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
                StopCoroutine("move");
            }
        }
        else
        {
            StopCoroutine("move");
        }
    }
}