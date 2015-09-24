﻿using UnityEngine;
using System.Collections;

public class CToolFireRange : CToolAttackRange {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
    protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Play_Monster"))
        {
            if (tool.CheckCanAttack())
            {
                StartCoroutine("ToolAttack");
            }

            if (tool.GetToolState() == ObjectState.Play_Tool_Shot) {
                tool.DirectAttackToMonster(other.GetComponent<CMonster>().id,tool.damage);
            }

        }
    }
}
