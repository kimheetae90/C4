using UnityEngine;
using System.Collections;

public class CMonsterAnimation : MonoBehaviour {

    Animator anim;
    CMonster monster;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
    }
	// Use this for initialization
	void Start () {
        monster = GetComponent<CMonster>();
	
	}
    public void Reset() {

        anim.SetBool("Walk", false);
        anim.SetBool("Ready", false);
        anim.SetBool("Attack", false);
        
    }

    public void Walk() {
        anim.SetBool("Walk",true);
    }

    public void Ready() {

        anim.SetBool("Ready",true);
    }

    public void Attack() {

        anim.SetBool("Attack",true);
    }

    public void Stun() {

        anim.SetTrigger("Stun");
    }

    public void Death() {

        anim.SetTrigger("Death");
    }
	// Update is called once per frame
	
}
