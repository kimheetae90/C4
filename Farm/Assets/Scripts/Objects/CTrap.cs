using UnityEngine;
using System.Collections;

public class CTrap : BaseObject
{
    public int power;
    public float stunTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;

        switch (objectState)
        {
            
        }
    }

    protected override void UpdateState()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Play_Monster") && other.GetComponent<CMonster>().isAlive){

            AttackMonster(other.GetComponent<CMonster>());
        }
    }

    void AttackMonster(CMonster _monster)
    {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterTrapped);
        gameMsg.Insert("monster_id", _monster.GetComponent<CMonster>().id);
        gameMsg.Insert("power", power);
        gameMsg.Insert("stunTime", stunTime);
        SendGameMessageToSceneManage(gameMsg);
        GetComponent<Collider>().enabled = false;
        StartCoroutine("activeCounter");
    }

    IEnumerator activeCounter() {

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
