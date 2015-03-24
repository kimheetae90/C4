using UnityEngine;
using System.Collections;
//PlayManager
public class C4_PlayMode : C4_SceneMode
{
    public C4_PlayerController playerController;
    public C4_EnemyController enemyController;

	// Use this for initialization
    public override void Start()
    {
        base.Start();
        addController(GameObjectType.Player,playerController);
        addController(GameObjectType.Enemy, enemyController);
	}
}
