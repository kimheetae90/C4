using UnityEngine;
using System.Collections;

public class C4_EnemyMaker : MonoBehaviour {

    public GameObject enemyGameObejct;
    GameObject missileGameObject;    
    public float regenerationTime;
	// Use this for initialization
	void Start () {
        InvokeRepeating("makeEnemy", 0, regenerationTime);
        missileGameObject = enemyGameObejct.GetComponent<C4_BoatFeature>().missile;
	}

    void makeEnemy()
    {
        GameObject initEnemyGameObject = Instantiate(enemyGameObejct, transform.position, transform.rotation) as GameObject;
        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
        GameObject initMissileGameObject = Instantiate(missileGameObject, missileInitPosition, transform.rotation) as GameObject;
        initEnemyGameObject.GetComponent<C4_BoatFeature>().missile = initMissileGameObject;
        C4_Object enemy = initEnemyGameObject.GetComponent<C4_Object>();
        C4_Object missile = initMissileGameObject.GetComponent<C4_Object>();
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref missile, GameObjectType.Missile, GameObjectInputType.Invalid);
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref enemy, GameObjectType.Enemy, GameObjectInputType.Invalid);
    }
}
