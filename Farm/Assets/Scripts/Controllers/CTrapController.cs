using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CTrapController : Controller
{
    public List<GameObject> trapList;
    public int maxAmount;
    int activatedAmount;
    CPlayer player;
    CTrapRoot trapRoot;

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        maxAmount = 20;
        trapList = new List<GameObject>();
        player = FindObjectOfType<CPlayer>();
        trapRoot = FindObjectOfType<CTrapRoot>();
        activatedAmount = 0;
        Generate();
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
           

        }
    }

    ///////////////////////////////////////////////

    void Generate() {

        for (int i = 0; i < maxAmount; i++) {
            GameObject trap = ObjectPooler.Instance.GetGameObject("Play_Skill_Trap");
            trap.GetComponent<CTrap>().SetController(this);
            trap.GetComponent<CTrap>().power = trapRoot.power;
            trap.GetComponent<CTrap>().stunTime = trapRoot.stuntime;
            trap.SetActive(false);
            trapList.Add(trap);
        }
    
    }

    public void Used() {
        Vector3 pos = new Vector3(player.transform.position.x+2, player.transform.position.y, player.transform.position.z);
        if (activatedAmount < maxAmount)
        {
            foreach (GameObject trap in trapList)
            {
                if (trap.activeInHierarchy == false)
                {
                    trap.transform.position = pos;
                    trap.GetComponent<Collider>().enabled = true;
                    trap.GetComponent<CLineHelper>().OrderingYPos(trap);
                    trap.SetActive(true);
                    activatedAmount++;
                    break;
                }
            }
        }/*
        else {
            GameObject trap = ObjectPooler.Instance.GetGameObject("Play_Skill_Trap");
            trap.GetComponent<CTrap>().SetController(this);
            trap.transform.position = pos;
            trap.GetComponent<Collider>().enabled = true;
            trap.GetComponent<CLineHelper>().OrderingYPos(trap);
            trap.SetActive(true);
            trapList.Add(trap);
            activatedAmount++;
            maxAmount++;
        
        }
    */
    }

    public void Reset() {

        foreach (GameObject trap in trapList) {
            trap.SetActive(false);
        }

    }
}
