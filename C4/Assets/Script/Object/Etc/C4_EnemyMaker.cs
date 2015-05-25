using UnityEngine;
using System.Collections;

public class C4_EnemyMaker : MonoBehaviour {

    public GameObject enemyUnitGameObject;
	public GameObject Minimap;
	public float regenerationTime;


	GameObject minimapEnemyUnit;

	// Use this for initialization
	void Start () {
        InvokeRepeating("makeEnemy", 0, regenerationTime);
	}

    void makeEnemy()
    {
        GameObject initEnemyGameObject = Instantiate(enemyUnitGameObject, transform.position, transform.rotation) as GameObject;

		minimapEnemyUnit = Minimap.GetComponent<C4_MinimapUI> ().EnemyUnitUI; // 나중에 미니맵에 여러가지 텍스쳐 만들면 상황에 맞게 바꿀수 있음
		GameObject initMinimapEnemyUnit = Instantiate(minimapEnemyUnit) as GameObject;
		initMinimapEnemyUnit.transform.SetParent(GameObject.Find ("MinimapUnit").transform);
		initMinimapEnemyUnit.GetComponent<C4_MinimapUnit> ().myBoat = initEnemyGameObject;

        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
       	if (initEnemyGameObject.GetComponent<C4_UnitFeature>().missile != null)
		{
			GameObject initMissileGameObject = Instantiate(initEnemyGameObject.GetComponent<C4_UnitFeature>().missile, missileInitPosition, transform.rotation) as GameObject;
			initEnemyGameObject.GetComponent<C4_UnitFeature>().missile = initMissileGameObject;
			initMissileGameObject.GetComponent<C4_MissileFeature>().unit = initEnemyGameObject;
			initMissileGameObject.GetComponent<C4_MissileFeature>().startPosition = initEnemyGameObject.transform.Find("FBX/MissileStartPosition").transform;
		}
    }
}
