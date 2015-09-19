using UnityEngine;
using System.Collections;

public class CPlayerAnimation : MonoBehaviour {

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    // Use this for initialization
    void Start()
    {

    }
    public void Reset()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Move", false);
        anim.SetBool("IdleWithTool", false);
        anim.SetBool("MoveFrontWithTool", false);
        anim.SetBool("MoveBackWithTool", false);

    }
    public void Idle()
    {
        anim.SetBool("Idle", true);
    }

    public void Move()
    {
        anim.SetBool("Move", true);
    }

    public void IdleWithTool() {
        anim.SetBool("IdleWithTool", true);
    }

    public void MoveFrontWithTool() {
        anim.SetBool("MoveFrontWithTool", true);
    }
    public void MoveBackWithTool() {
        anim.SetBool("MoveBackWithTool", true);
    }

    public void IdleOuch() {
        anim.SetTrigger("IdleOuch");
    }

    public void MoveOuch() {
        anim.SetTrigger("MoveOuch");
    }

    public void IdleWithToolOuch() {
        anim.SetTrigger("IdleWithToolOuch");
    }

    public void MoveFrontWithToolOuch() {
        anim.SetTrigger("MoveFrontWithToolOuch");
    }

    public void MoveBackWithToolOuch()
    {
        anim.SetTrigger("MoveBackWithToolOuch");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }
}
