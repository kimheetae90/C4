using UnityEngine;
using System.Collections;

public class CAttack : MonoBehaviour {

    CMonster monster;
    float attackTime;
    bool attackable;
    

	// Use this for initialization
	void Start () {
        attackable = true;
        if (transform.root.GetComponent<CMonster>() != null)
        {
            monster = transform.root.GetComponent<CMonster>();
            attackTime = monster.attackTime;
        }
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (monster != null && monster.GetMonsterState()==ObjectState.Play_Monster_Attack) {

            if (other.CompareTag("Play_Player"))
            {
                Debug.Log(other.ToString());
                if (attackable)
                {
                    monster.AttackPlayer();
                    StartCoroutine("AttackCount");
                }
            }
            else if (other.CompareTag("Play_Tool"))
            {
                if (attackable)
                {
                    monster.AttackTool(other.GetComponent<CTool>());
                    StartCoroutine("AttackCount");
                }
                
            }
            else if (other.CompareTag("Play_Fence"))
            {
                if (attackable)
                {
                    monster.AttackFence(other.GetComponent<CFence>());
                    StartCoroutine("AttackCount");
                }
            }
        }
    }

    IEnumerator AttackCount() {
        attackable = false;
        yield return new WaitForSeconds(attackTime);
        attackable = true;
    }
}
