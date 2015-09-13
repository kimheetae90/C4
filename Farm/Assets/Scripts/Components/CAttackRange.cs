using UnityEngine;
using System.Collections;

public class CAttackRange : MonoBehaviour
{

    BoxCollider attackRangeCollider;
    public float attackRange;
    CMonster monster;
    CTool tool;
    // Use this for initialization

    void Start()
    {
        attackRangeCollider = GetComponent<BoxCollider>();

        if (transform.parent.gameObject.tag == "Play_Tool")
        {
            attackRange = GetComponentInParent<CTool>().attackRange;
            tool = transform.parent.GetComponent<CTool>();
            attackRangeCollider.center = new Vector3(attackRange / 2, 0, 0);
        }
        else if (transform.parent.gameObject.tag == "Play_Monster")
        {
            attackRange = GetComponentInParent<CMonster>().attackRange;
            monster = transform.parent.GetComponent<CMonster>();
            attackRangeCollider.center = new Vector3(-attackRange / 2, 0, 0);
        }

        attackRangeCollider.size = new Vector3(attackRange, 2, 2);
    }

    void OnTriggerStay(Collider other)
    {
        if (tool != null)//tool일때
        {
            if (other.CompareTag("Play_Monster"))
            {
                if (tool.CheckCanAttack())
                {
                    StartCoroutine("ToolAttack");
                }
                
            }
        }

        else if (monster != null)//monster일때
        {
            if (other.CompareTag("Play_Player"))
            {
                if (other.GetComponent<CPlayer>().isAlive)
                {
                    monster.touchedWithPlayer = true;
                    monster.MonsterMoveStop();
                    if (monster.CheckCanAttack())
                    {
                        StartCoroutine("MonsterAttack");
                    }
                }
                else {
                    monster.touchedWithPlayer = false;
                }
            }
            else if (other.CompareTag("Play_Tool"))
            {
                if (other.GetComponent<CTool>().isAlive)
                {
                    monster.touchedWithTool = true;
                    monster.MonsterMoveStop();
                    if (monster.CheckCanAttack())
                    {
                        StartCoroutine("MonsterAttack");
                    }
                }
                else {
                    monster.touchedWithTool = false;
                }
            }
            else if (other.CompareTag("Play_Fence"))
            {
                monster.MonsterMoveStop();
                monster.touchedFenceID = other.GetComponent<CFence>().id;
                if (monster.CheckCanAttack())
                {
                    StartCoroutine("MonsterAttack");
                }

            }
           
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (monster != null)
        {
            if (other.CompareTag("Play_Player"))
            {
                monster.touchedWithPlayer = false;
            }

            else if (other.CompareTag("Play_Tool"))
            {
                monster.touchedWithTool = false;
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
            
            if (tool.isAlive&&tool.canHeld)
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

    IEnumerator MonsterAttack()
    {
        if (monster.CheckCanAttack())
        {
            monster.attackable = false;

            if (monster.isAlive)
            {
                monster.ChangeStateToReadyForAttack();
                yield return new WaitForSeconds(monster.attackReadyTime);
                if (monster.GetMonsterState() == ObjectState.Play_Monster_ReadyForAttack)
                {
                    monster.ChangeStateToAttack();
                }
            }
            yield return new WaitForSeconds(monster.attackTime);

            monster.attackable = true;
            if (monster.CheckTouched() == false)
            {
                if (monster.GetMonsterState() != ObjectState.Play_Monster_Return && monster.GetMonsterState() != ObjectState.Play_Monster_Ready && monster.GetMonsterState() != ObjectState.Play_Monster_Die)
                {
                    monster.ChangeStateToMove();
                }
            }
            
        }
        else
        {
            yield return null;
        }
    }
}
