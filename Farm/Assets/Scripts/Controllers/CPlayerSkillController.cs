using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CPlayerSkillController : Controller{

    public List<GameObject> skillList;

	// Update is called once per frame
    protected override void Start()
    {

        base.Start();
        skillList = new List<GameObject>();
        skillList.Add(ObjectPooler.Instance.GetGameObject("Play_Skill_Bomb"));
        skillList[0].GetComponent<CPlayerSkill>().SetController(this);
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_PlayerSkill1Used:
                PlayerSkill1Used();
                break;

        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    /// 
    public void PlayerSkill1Used() {

        skillList[0].GetComponent<CPlayerSkill>().ChangeStateToUsed();
    }
}
