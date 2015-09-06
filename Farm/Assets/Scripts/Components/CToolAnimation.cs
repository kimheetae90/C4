using UnityEngine;
using System.Collections;

public class CToolAnimation : MonoBehaviour {

    Animator anim;
    CTool tool;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    // Use this for initialization
    void Start()
    {
        tool = GetComponent<CTool>();

    }
    public void Reset()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Move", false);

    }
    public void Idle()
    {
        anim.SetBool("Idle", true);
    }

    public void Move()
    {
        anim.SetBool("Move", true);
    }

    public void Ready()
    {
        anim.SetTrigger("Ready");
    }

    public void Shot()
    {

        anim.SetTrigger("Shot");
    }
    
    public void Die()
    {

        anim.SetTrigger("Die");
    }

}
