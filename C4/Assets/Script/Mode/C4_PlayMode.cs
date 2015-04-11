using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//PlayManager
public class C4_PlayMode : C4_SceneMode
{
    public C4_AllyController allyController;
    public C4_EnemyController enemyController;
	public C4_CameraController cameraController;

    public C4_PlayerUI playerUI;
    public List<GameObject> ListAllyGameObject;
    public GameObject allyUnitGameObject1;   //나중에 게임오브젝트로부터 받을 것(List이어야함)
    public GameObject allyUnitGameObject2;   //나중에 게임오브젝트로부터 받을 것(List이어야함)

    void Awake()
    {
        C4_GameManager.Instance.StartPlayScene();

        ListAllyGameObject.Add(allyUnitGameObject1);
        ListAllyGameObject.Add(allyUnitGameObject2);

        Vector3 initPos = transform.position;

        foreach (GameObject allyGameObject in ListAllyGameObject)
        {
            instantiatePlayer(allyGameObject, initPos, transform.rotation);
            initPos.z += 20;
        }
    }

    public override void Start()
    {
        base.Start();
     
        C4_GameManager.Instance.objectManager.resetAllObjectData();
        addSubObjectManagers();
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

    void addSubObjectManagers()
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
