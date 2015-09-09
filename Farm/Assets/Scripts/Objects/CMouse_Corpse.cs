using UnityEngine;
using System.Collections;

public class CMouse_Corpse : CMonster {

	// Use this for initialization
	protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Play_Farm"))
        {
            AttackFarm();
        }

        else if (other.CompareTag("Play_Player"))
        {
            if (other.GetComponent<CPlayer>().isAlive)
            {
                touchedWithPlayer = true;
                MonsterMoveStop();
                if (attackable)
                {
                    StartCoroutine("Attack_Player");
                }
            }
            else {
                touchedWithPlayer = false;
            }
        }

        else if (other.CompareTag("Play_Tool"))
        {
            if (other.GetComponent<CTool>().isAlive)
            {
                touchedWithTool = true;
                MonsterMoveStop();
                if (attackable)
                {
                    StartCoroutine(Attack_Tool(other.GetComponent<CTool>()));
                }

            }
            else {
                touchedWithTool = false;
            }
        }
        else if (other.CompareTag("Play_Fence"))
        {
            MonsterMoveStop();
            touchedFenceID = other.GetComponent<CFence>().id;
            if (attackable)
            {
                StartCoroutine(Attack_Fence(other.GetComponent<CFence>()));
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Play_Player"))
        {
            touchedWithPlayer = false;
        }

        else if (other.CompareTag("Play_Tool"))
        {
            touchedWithTool = false;
        }
    }
}
