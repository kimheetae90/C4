using UnityEngine;
using System.Collections;

public class C4_EnemyMaker : MonoBehaviour {

    public GameObject enemyGameObejct;
    public float regenerationTime;
    int objectID;
    GameObject initObject;

    C4_Object enemy;
	// Use this for initialization
	void Start () {
        InvokeRepeating("makeEnemy", 0f, regenerationTime);
	}


    void makeEnemy()
    {
        initObject = Instantiate(enemyGameObejct, transform.position, transform.rotation) as GameObject;
        enemy = initObject.GetComponent<C4_Object>();
        enemy.objectID.id = C4_ObjectManager.Instance.currentObjectCode++;
        enemy.objectID.type = ObjectID.Type.Enemy;
        C4_ObjectManager.Instance.addObjectToAll(enemy);
    }
}
