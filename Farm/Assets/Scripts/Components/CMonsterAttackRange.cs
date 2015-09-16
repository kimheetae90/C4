using UnityEngine;
using System.Collections;

public class CMonsterAttackRange : CAttackRange
{
    CMonster monster;
	// Use this for initialization
	void Start () {
        base.Start();
        attackRange = GetComponentInParent<CMonster>().attackRange;
        monster = transform.parent.GetComponent<CMonster>();
        attackRangeCollider.center = new Vector3(-attackRange / 2, 0, 0);
        attackRangeCollider.size = new Vector3(attackRange, 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
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
                else
                {
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
                else
                {
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

    void OnTriggerExit(Collider other)
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
