using UnityEngine;
using System.Collections;

public class C4_BehaviorActionFunc : MonoBehaviour
{
    float moveDelayTime;
    
    float attackDelayTime;
    
    Transform mTransform;

    C4_Unit mUnit;

    C4_UnitFeature mUnitFeature;

    C4_EnemyAttackUI attackUI;

    void Start()
    {
        moveDelayTime = 1f;
        
        attackDelayTime = 1.5f;
        
        mTransform = transform;
        
        mUnit = GetComponent<C4_Unit>();

        mUnitFeature = GetComponent<C4_UnitFeature>();

        attackUI = GetComponentInChildren<C4_EnemyAttackUI>();
    }

    public void MoveTo(Vector3 pos)
    {
        StartCoroutine(moveTo(pos));
    }

    IEnumerator moveTo(Vector3 pos)
    {
        if (mTransform == null || mUnit == null || mUnitFeature == null) yield return null;

        yield return new WaitForSeconds(moveDelayTime);

        mUnit.move(pos);

        mUnit.turn(pos);

        yield return null;
    }

    public void MoveTo(C4_Object targetObject)
    {
        StartCoroutine(moveTo(targetObject));
    }

    IEnumerator moveTo(C4_Object targetObject)
    {
        if (mTransform == null || mUnit == null || mUnitFeature == null) yield return null;

        yield return new WaitForSeconds(moveDelayTime);

        Vector3 targetPos = targetObject.transform.position;

        Vector3 dir = targetPos - mTransform.position;

        float len = Vector3.Distance(targetPos, mTransform.position);

        dir.Normalize();

        Vector3 toMove = Vector3.zero;

        if(len > mUnitFeature.moveRange)
        {
            toMove = targetObject.transform.position + dir * mUnitFeature.moveRange;
        }
        else
        {
            toMove = targetObject.transform.position + dir * (len - mUnitFeature.attackRange);
        }

        mUnit.move(toMove);

        mUnit.turn(toMove);

        yield return null;
    }

    public void AttackTargetPos(Vector3 pos)
    {
        StartCoroutine(attackTargetPos(pos));
    }

    IEnumerator attackTargetPos(Vector3 pos)
    {
        if (mTransform == null || mUnit == null || attackUI == null) yield return null;

        if (mUnitFeature.gage >= mUnitFeature.fullGage)
        {
            attackUI.showUI();
        }

        yield return new WaitForSeconds(attackDelayTime);

        Vector3 toAttack = pos;

        mUnit.turn(toAttack);

        mUnit.shot(toAttack);

        yield return null;
    }

}
