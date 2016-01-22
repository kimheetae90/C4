using UnityEngine;
using System.Collections;

public class CBomb : CPlayerSkill {

    Collider coll;
    public int power;

    void Awake() {
        coll = GetComponent<Collider>();
        coll.enabled = false;
        cooldown = 5f;
        power = 100;
    }
    
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (objectState==ObjectState.Play_Skill_Activated&&other.tag == "Play_Monster")
        {
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterDamaged);
            gameMsg.Insert("monster_id", other.GetComponent<CMonster>().id);
            gameMsg.Insert("power", power);
            SendGameMessageToSceneManage(gameMsg);
        }

        if (objectState == ObjectState.Play_Skill_Activated && (other.tag == "Play_Tool" || other.tag == "Play_Terrain"))
        {
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_PlayersObjectDamagedByMonster);
            gameMsg.Insert("object_id", other.GetComponent<BaseObject>().id);
            gameMsg.Insert("monster_power", power);
            SendGameMessageToSceneManage(gameMsg);
        }
    }

    public override void Used() {
        
        coll.enabled = true;
        StartCoroutine("Activated");
    }
    public override void Waiting()
    {

    }
    public override void Cooldown()
    {
        StartCoroutine("CooldownCheck");
    }

    IEnumerator Activated() {
        yield return new WaitForSeconds(1f);
        coll.enabled = false;
        ChangeState(ObjectState.Play_Skill_Cooldown);
    }

    IEnumerator CooldownCheck() {
        yield return new WaitForSeconds(cooldown);
        ChangeState(ObjectState.Play_Skill_Waiting);
    }

    public override void Reset()
    {
        StopCoroutine("Activated");
        StopCoroutine("CooldownCheck");
        coll.enabled = false;

        ChangeState(ObjectState.Play_Skill_Waiting);
    }

    public void ChangePos(Vector3 pos) {
        transform.position = pos;
    }
}
