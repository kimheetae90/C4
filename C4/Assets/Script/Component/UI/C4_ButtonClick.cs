﻿using UnityEngine;
using System.Collections;

public class C4_ButtonClick : MonoBehaviour {


    GameObject myCharacter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void selectAlly()
    {

        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.selectClickObject(myCharacter);
    }

    public void movetoselect()
    {
        C4_GameManager.Instance.GetComponentInChildren<C4_PlaySceneCamera>().moveToSomeObject();
    }
}
