using UnityEngine;
using System.Collections;

public class C4_EnemyMaker : MonoBehaviour {

    public GameObject enemyUnitGameObject;
    public float regenerationTime;
	// Use this for initialization
	void Start () {
        InvokeRepeating("makeEnemy", 0, regenerationTime);
	}

    void makeEnemy()
    {
        GameObject initEnemyGameObject = Instantiate(enemyUnitGameObject, transform.position, transform.rotation) as GameObject;
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
