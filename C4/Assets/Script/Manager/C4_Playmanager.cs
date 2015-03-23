﻿using UnityEngine;
using System.Collections;
//PlayManager
public class C4_PlayManager : C4_SceneManager
{
    public C4_PlayerController playerController;
    public C4_EnemyController enemyController;

	// Use this for initialization
    public void Start()
    {
        base.Start();
        addController(GameObjectType.Player,playerController);
        addController(GameObjectType.Enemy, enemyController);
	}
}
