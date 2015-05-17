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
	public C4_ButtonUI buttonUI;
	public GameObject Minimap;
	GameObject minimapAllyUnit;

    void Awake()
    {
		C4_GameManager.Instance.StartPlayScene();
    }

    public override void Start()
    {
		base.Start();
		addSubObjectManagers();
		
		ListAllyGameObject.Add(allyUnitGameObject1);   //나중에 게임오브젝트로부터 받을 것(List이어야함)
		ListAllyGameObject.Add(allyUnitGameObject2);   //나중에 게임오브젝트로부터 받을 것(List이어야함)
		Vector3 initPos = transform.position;
		
		foreach (GameObject allyGameObject in ListAllyGameObject)
		{
			instantiatePlayer(allyGameObject, initPos, transform.rotation);
			initPos.z += 20;
		}
		
		addController(GameObjectType.Ally,allyController);
		addController(GameObjectType.Enemy, enemyController);
		addController (GameObjectType.Cam, cameraController);
		addPlayerControllerListener();
		addCameraControllerListener();
		buttonUI.initButtonUI ();
	}

    void instantiatePlayer(GameObject playerGameObject, Vector3 pos, Quaternion angle)
    {
        GameObject initPlayerGameObject = Instantiate(playerGameObject, pos, angle) as GameObject;

		minimapAllyUnit = Minimap.GetComponent<C4_MinimapUI> ().AllyUnitUI; // 나중에 미니맵에 여러가지 텍스쳐 만들면 상황에 맞게 바꿀수 있음
		GameObject initMinimapAllyUnit = Instantiate(minimapAllyUnit) as GameObject;
		initMinimapAllyUnit.transform.parent = GameObject.Find ("MinimapUnit").transform;
		initMinimapAllyUnit.GetComponent<C4_MinimapUnit> ().myBoat = initPlayerGameObject;

        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
        if (initPlayerGameObject.GetComponent<C4_UnitFeature>().missile != null)
        {
            GameObject initMissileGameObject = Instantiate(initPlayerGameObject.GetComponent<C4_UnitFeature>().missile, missileInitPosition, transform.rotation) as GameObject;
            initPlayerGameObject.GetComponent<C4_UnitFeature>().missile = initMissileGameObject;
            initMissileGameObject.GetComponent<C4_MissileFeature>().unit = initPlayerGameObject;
			initMissileGameObject.GetComponent<C4_MissileFeature>().startPosition = initPlayerGameObject.gameObject.transform.Find("FBX/MissileStartPosition").transform;
        }
    }

    void addSubObjectManagers()
    {
        C4_ObjectManager objectManager = C4_GameManager.Instance.objectManager;
        C4_AllyObjectManager allyObjectManager = GameObject.Find("AllyObjectManager").GetComponent<C4_AllyObjectManager>();
        C4_EnemyObjectManager enemyObjectManager = GameObject.Find("EnemyObjectManager").GetComponent<C4_EnemyObjectManager>();
        C4_MissileObjectManager missileObjectManager = GameObject.Find("MissileObjectManager").GetComponent<C4_MissileObjectManager>();
        objectManager.addSubObjectManager(GameObjectType.Ally, allyObjectManager);
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
