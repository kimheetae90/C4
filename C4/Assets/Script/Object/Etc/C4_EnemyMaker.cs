using UnityEngine;
using System.Collections;

public class C4_EnemyMaker : MonoBehaviour {

    public GameObject enemyGameObejct;
    public float regenerationTime;
	// Use this for initialization
	void Start () {
        InvokeRepeating("makeEnemy", 0, regenerationTime);
	}

    void makeEnemy()
    {
        GameObject initEnemyGameObject = Instantiate(enemyGameObejct, transform.position, transform.rotation) as GameObject;
        Vector3 missileInitPosition = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
        GameObject initMissileGameObject = Instantiate(initEnemyGameObject.GetComponent<C4_BoatFeature>().missile, missileInitPosition, transform.rotation) as GameObject;
        initEnemyGameObject.GetComponent<C4_BoatFeature>().missile = initMissileGameObject;
    }
}
