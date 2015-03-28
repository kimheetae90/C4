using UnityEngine;
using System.Collections;

public class C4_EnemyMaker : MonoBehaviour {

    public GameObject enemyGameObejct;
    public float regenerationTime;
	// Use this for initialization
	void Start () {
        InvokeRepeating("makeEnemy", 0f, regenerationTime);
	}

    void makeEnemy()
    {
        GameObject initEnemyGameObject = Instantiate(enemyGameObejct, transform.position, transform.rotation) as GameObject;
        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
        GameObject initMissileGameObject = Instantiate(enemyGameObejct.GetComponent<C4_BoatFeature>().missile, missileInitPosition, transform.rotation) as GameObject;
        C4_Object enemy = initEnemyGameObject.GetComponent<C4_Object>();
        C4_Object missile = initMissileGameObject.GetComponent<C4_Object>();
        enemy.GetComponent<C4_BoatFeature>().missile = initMissileGameObject;
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref missile, GameObjectType.Missile, GameObjectInputType.Invalid);
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref enemy, GameObjectType.Enemy, GameObjectInputType.Invalid);

    }
}
