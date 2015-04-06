using UnityEngine;
using System.Collections;
//PlayManager
public class C4_PlayMode : C4_SceneMode
{
    public C4_AllyController allyController;
    public C4_EnemyController enemyController;
	public C4_CameraController cameraController;

    public C4_PlayerUI playerUI;
    public GameObject allyUnitGameObject1;   //나중에 게임오브젝트로부터 받을 것(List이어야함)
    public GameObject allyUnitGameObject2;   //나중에 게임오브젝트로부터 받을 것(List이어야함)

    void Awake()
    {
        C4_GameManager.Instance.StartPlayScene();
    }

    public override void Start()
    {
        base.Start();
     
        C4_GameManager.Instance.objectManager.resetAllObjectData();
        addPlaySceneManager();

        Vector3 initPos = transform.position;
        initPos.z -= 15;
        instantiatePlayer(allyUnitGameObject1, initPos, transform.rotation);
        initPos.z += 15;
        instantiatePlayer(allyUnitGameObject2, initPos, transform.rotation);
        addController(GameObjectType.Ally,allyController);
        addController(GameObjectType.Enemy, enemyController);
		addController (GameObjectType.Camera, cameraController);
        addPlayerControllerListener();
        addCameraControllerListener();
	}

    void instantiatePlayer(GameObject playerGameObject, Vector3 pos, Quaternion angle)
    {
        GameObject initPlayerGameObject = Instantiate(playerGameObject, pos, angle) as GameObject;
        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
        GameObject initMissileGameObject = Instantiate(initPlayerGameObject.GetComponent<C4_UnitFeature>().missile, missileInitPosition, transform.rotation) as GameObject;
        initPlayerGameObject.GetComponent<C4_UnitFeature>().missile = initMissileGameObject;
    }

    void addPlaySceneManager()
    {
        C4_ObjectManager objectManager = C4_GameManager.Instance.objectManager;
        C4_AllyObjectManager playerObjectManager = GameObject.Find("PlayerObjectManager").GetComponent<C4_AllyObjectManager>();
        C4_EnemyObjectManager enemyObjectManager = GameObject.Find("EnemyObjectManager").GetComponent<C4_EnemyObjectManager>();
        C4_MissileObjectManager missileObjectManager = GameObject.Find("MissileObjectManager").GetComponent<C4_MissileObjectManager>();
        objectManager.addSubObjectManager(GameObjectType.Ally, playerObjectManager);
        objectManager.addSubObjectManager(GameObjectType.Enemy, enemyObjectManager);
        objectManager.addSubObjectManager(GameObjectType.Missile, missileObjectManager);
    }

    private void addPlayerControllerListener()
    {
        if (playerUI != null && allyController != null)
        {
            allyController.addListener(playerUI as C4_IControllerListener);
        }
    }

    private void addCameraControllerListener()
    {
        C4_Camera camObject = Camera.main.transform.root.GetComponent<C4_Camera>();

        if (camObject != null)
        {
            cameraController.addListener(camObject as C4_IControllerListener);
        }
    }
}
