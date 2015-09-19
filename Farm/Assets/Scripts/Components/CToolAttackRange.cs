using UnityEngine;
using System.Collections;

public class CToolAttackRange : CAttackRange {

    CTool tool;
	// Use this for initialization
	protected void Start () {
        base.Start();
        attackRange = GetComponentInParent<CTool>().attackRange;
        tool = transform.parent.GetComponent<CTool>();
        attackRangeCollider.center = new Vector3(attackRange / 2, 0, 0);
        attackRangeCollider.size = new Vector3(attackRange, 2, 2);
	}
	
    void OnTriggerStay(Collider other)
    {
            if (other.CompareTag("Play_Monster"))
            {
                if (tool.CheckCanAttack())
                {
                    StartCoroutine("ToolAttack");
                }

            }
    }

    /// <summary>
    /// ToolAttack
    /// </summary>
    /// <returns></returns>
    IEnumerator ToolAttack()
    {
        if (tool.CheckCanAttack())
        {
            tool.shotable = false;

            if (tool.isAlive && tool.canHeld)
            {
                tool.ChangeStateToReadyToShot();
                yield return new WaitForSeconds(tool.attackReadySpeed);
                if (tool.GetToolState() == ObjectState.Play_Tool_ReadyToShot)
                {
                    tool.ChangeStateToShot();
                }
            }

            if (tool.GetToolState() != ObjectState.Play_Tool_Move && tool.GetToolState() != ObjectState.Play_Tool_UnAvailable)
            {
                tool.ChangeStateToReady();
            }
            yield return new WaitForSeconds(tool.attackSpeed);
            tool.shotable = true;
        }
        else
        {
            yield return null;
        }

    }

}
