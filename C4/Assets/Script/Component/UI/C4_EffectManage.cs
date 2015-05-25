using UnityEngine;
using System.Collections;

public class C4_EffectManage : MonoBehaviour
{

    public Animator animator;
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void endEffect()
    {
        animator.speed = 0;
        gameObject.SetActive(false);
    }
    public void showEffect()
    {
        animator.speed = 1;
        gameObject.SetActive(true);
    }
}
