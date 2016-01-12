using UnityEngine;
using System.Collections;

public class CFlash : CPlayerSkill
{
    public GameObject particle;
    CPlayer player;

    void Awake(){
        cooldown = 5f;
        particle.SetActive(false);
    }
    void Start() {
        player = FindObjectOfType<CPlayer>();
    }

    public override void Used()
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.position = pos;
        particle.SetActive(true);
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterBlinded);
        SendGameMessageToSceneManage(gameMsg);
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
        yield return new WaitForSeconds(3f);
        particle.SetActive(false);
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterBlindOver);
        SendGameMessageToSceneManage(gameMsg);
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
        particle.SetActive(false);
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterBlindOver);
        SendGameMessageToSceneManage(gameMsg);

        ChangeState(ObjectState.Play_Skill_Waiting);
    }
}
