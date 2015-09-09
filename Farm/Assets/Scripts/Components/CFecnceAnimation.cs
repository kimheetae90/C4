using UnityEngine;
using System.Collections;

public class CFecnceAnimation : MonoBehaviour {

	// Use this for initialization
    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Idle",true);
    }
    // Use this for initialization
    
    public void Hitted()
    {
        anim.SetTrigger("Hitted");
    }
}
