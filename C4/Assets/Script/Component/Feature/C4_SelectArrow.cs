﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_SelectArrow : MonoBehaviour {

    public Canvas playerArrow;
   
    
	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {    
        
        
        
	}
    public void PlayerArrowMove()
    {
            Vector3 pos = C4_PlayManager.Instance.selectedBoat.transform.position;
            pos.y += 6.5f;
            pos.z += 11;

            playerArrow.transform.position = pos;
    }
    public void EnemyArrowMove()
    {
        Vector3 pos = C4_EnemyManager.Instance.selectedBoat.transform.position;
        pos.y += 6.5f;
        pos.z += 11;

        playerArrow.transform.position = pos;
    }
}
