using UnityEngine;
using System.Collections;

public class CTrapRoot : CPlayerSkill
{

    public int power;
    public float stuntime;
    CTrapController trapController;
    void Awake()
    {
        cooldown = 5f;
        power = 10;
        stuntime = 2f;
    }
    void Start() {
        trapController = GetComponent<CTrapController>();
    }

   
    public override void Used()
    {
        trapController.Used();
        StartCoroutine("Activated");
    }
    public override void Waiting()
    {

    }
    public override void Cooldown()
    {
        StartCoroutine("CooldownCheck");
    }

    IEnumerator Activated()
    {
        yield return new WaitForSeconds(1f);
        ChangeState(ObjectState.Play_Skill_Cooldown);
    }

    IEnumerator CooldownCheck()
    {
        yield return new WaitForSeconds(cooldown);
        ChangeState(ObjectState.Play_Skill_Waiting);
    }
    public override void Reset()
    {
        StopCoroutine("Activated");
        StopCoroutine("CooldownCheck");
        trapController.Reset();
        ChangeState(ObjectState.Play_Skill_Waiting);
    }
}
