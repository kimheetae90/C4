using UnityEngine;
using System.Collections;

public class CMonsterAnimation : MonoBehaviour {

    Animator anim;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
    }
	// Use this for initialization
	void Start () {
	
	}
    public void Reset() {
        transform.localScale = new Vector3(1, 1, 1);
        anim.SetBool("Walk", false);
        anim.SetBool("Ready", false);
        anim.SetBool("Attack", false);
        
    }
    public void Idle() {
        anim.SetTrigger("Idle");
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

    public void Return() {
        transform.localScale = new Vector3(-1, 1, 1);
        anim.SetBool("Walk", true);
    }
	// Update is called once per frame
	
}
