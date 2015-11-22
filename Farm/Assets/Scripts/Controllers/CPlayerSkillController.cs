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
        skillList.Add(ObjectPooler.Instance.GetGameObject("Play_Skill_Flash"));
        skillList[1].GetComponent<CPlayerSkill>().SetController(this);
        skillList.Add(ObjectPooler.Instance.GetGameObject("TrapController"));
        skillList[2].GetComponent<CPlayerSkill>().SetController(this);
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_PlayerSkill1Used:
                PlayerSkill1Used();
                break;
            case MessageName.Play_PlayerSkill2Used:
                PlayerSkill2Used();
                break;
            case MessageName.Play_PlayerSkill3Used:
                PlayerSkill3Used();
                break;

            case MessageName.Play_StageRestart:
                SkillReset();
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
    public void PlayerSkill2Used()
    {

        skillList[1].GetComponent<CPlayerSkill>().ChangeStateToUsed();
    }
    public void PlayerSkill3Used()
    {

        skillList[2].GetComponent<CPlayerSkill>().ChangeStateToUsed();
    }

    public void SkillReset() {
        foreach (GameObject skill in skillList) {
            skill.GetComponent<CPlayerSkill>().Reset();
        }
    }
}
