using UnityEngine;
using System.Collections;
//PlayManager
public class C4_PlayMode : C4_SceneMode
{
    public C4_PlayerController playerController;
    public C4_EnemyController enemyController;
    public C4_PlayerUI playerUI;

	// Use this for initialization
    public override void Start()
    {
        base.Start();
        addController(GameObjectType.Player,playerController);
        addController(GameObjectType.Enemy, enemyController);

        addPlayerControllerListener();
	}

    private void addPlayerControllerListener()
    {
        if (playerUI != null && playerController != null)
        {
            playerController.addListener(playerUI as C4_IControllerListener);
        }
    }
}
