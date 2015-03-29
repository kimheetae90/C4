using UnityEngine;
using System.Collections;
//PlayManager
public class C4_PlayMode : C4_SceneMode
{
    public C4_PlayerController playerController;
    public C4_EnemyController enemyController;
	public C4_CameraController cameraController;

    public C4_PlayerUI playerUI;
    public GameObject playerBoatGameObject;   //나중에 게임오브젝트로부터 받을 것(List이어야함)

    void Awake()
    {
        C4_ManagerMaster.Instance.StartPlayScene();
    }

    public override void Start()
    {
        base.Start();
     
        C4_ManagerMaster.Instance.objectManager.resetAllObjectData();
        addPlaySceneManager();
        
        GameObject initPlayerGameObject = Instantiate(playerBoatGameObject, transform.position, transform.rotation) as GameObject;
        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
        GameObject initMissileGameObject = Instantiate(playerBoatGameObject.GetComponent<C4_BoatFeature>().missile, missileInitPosition, transform.rotation) as GameObject;
        C4_Object player = initPlayerGameObject.GetComponent<C4_Object>();
        C4_Object missile = initMissileGameObject.GetComponent<C4_Object>();
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref missile, GameObjectType.Missile, GameObjectInputType.Invalid);
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref player, GameObjectType.Player, GameObjectInputType.SelectAbleObject | GameObjectInputType.ClickAbleObject);
        player.GetComponent<C4_BoatFeature>().missile = missile.gameObject;
        addController(GameObjectType.Player,playerController);
        addController(GameObjectType.Enemy, enemyController);
		addController (GameObjectType.Camera, cameraController);
        addPlayerControllerListener();
	}

    void addPlaySceneManager()
    {
        C4_ObjectManager objectManager = C4_ManagerMaster.Instance.objectManager;
        C4_PlayerObjectManager playerObjectManager = GameObject.Find("PlayerObjectManager").GetComponent<C4_PlayerObjectManager>();
        C4_EnemyObjectManager enemyObjectManager = GameObject.Find("EnemyObjectManager").GetComponent<C4_EnemyObjectManager>();
        objectManager.addSubObjectManager(GameObjectType.Player, playerObjectManager);
        objectManager.addSubObjectManager(GameObjectType.Enemy, enemyObjectManager);
    }

    private void addPlayerControllerListener()
    {
        if (playerUI != null && playerController != null)
        {
            playerController.addListener(playerUI as C4_IControllerListener);
        }
    }
}
