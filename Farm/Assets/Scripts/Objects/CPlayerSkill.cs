using UnityEngine;
using System.Collections;

public abstract class CPlayerSkill : BaseObject{


    public float cooldown;
	// Use this for initialization
	void Start () {
        objectState = ObjectState.Play_Skill_Waiting;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override void UpdateState()
    {

    }
    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;

        switch (objectState)
        {                
            case ObjectState.Play_Skill_Waiting:
                Waiting();
                break;
            case ObjectState.Play_Skill_Activated:
                Used();
                break;
            case ObjectState.Play_Skill_Cooldown:
                Cooldown();
                break;
        }
    }
    public void ChangeStateToUsed() {
        ChangeState(ObjectState.Play_Skill_Activated);
    
    }

    public abstract void Used();

    public abstract void Cooldown();

    public abstract void Waiting();
}
