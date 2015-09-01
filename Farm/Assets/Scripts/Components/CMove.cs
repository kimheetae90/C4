using UnityEngine;
using System.Collections;

public class CMove : MonoBehaviour {

    Vector3 toMove;
    public bool isMove;
    public float moveSpeed;
    float m_moveSpeed;
    

    void Start()
    {
        isMove = false;
        m_moveSpeed = moveSpeed;
    }

    public void SetMoveSpeed(float inputMoveSpeed)
    {
        m_moveSpeed = inputMoveSpeed;
    }
   
    public void SetTargetPos(Vector3 _toMove)
    {
        toMove.Set(_toMove.x, _toMove.y, _toMove.z);
    }

    public void StopMoveToTarget()
    {
        isMove = false;
        StopCoroutine("move");
    }

    public void StartMove()
    {
        if (isMove != true)
        {
            isMove = true;
            StartCoroutine("move");
        }
    }

    IEnumerator move()
    {
        yield return new WaitForEndOfFrame();
        if (isMove == false)
        {
            StopMoveToTarget();
        }

            if (Vector3.Distance(this.transform.position, toMove) > 0.05f * m_moveSpeed)
            {
                transform.Translate((toMove - this.transform.position).normalized * Time.deltaTime * m_moveSpeed);
                StartCoroutine("move");
            }
            else if (Vector3.Distance(this.transform.position, toMove) > 0.01f * m_moveSpeed)
            {
                transform.Translate((toMove - this.transform.position).normalized * Time.deltaTime * m_moveSpeed * 0.4f);
                StartCoroutine("move");
            }
            else
            {
                StopMoveToTarget();
            }
        
    }

    

   
}
