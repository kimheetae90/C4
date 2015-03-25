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
        GameObject initObject = Instantiate(enemyGameObejct, transform.position, transform.rotation) as GameObject;
        C4_Object enemy = initObject.GetComponent<C4_Object>();
        C4_ManagerMaster.Instance.objectManager.registerObjectToAll(ref enemy, GameObjectType.Enemy);
    }
}
