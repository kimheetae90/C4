using UnityEngine;
using System.Collections;

public class C4_EnemyObjectManager : C4_BaseObjectManager, C4_IntInitInstance
{
    private static C4_EnemyObjectManager _instance;
    public static C4_EnemyObjectManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_EnemyObjectManager)) as C4_EnemyObjectManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_EnemyObjectManager";
                    _instance = container.AddComponent(typeof(C4_EnemyObjectManager)) as C4_EnemyObjectManager;
                }
            }

            return _instance;
        }
    }

    public void initInstance()
    {
        if (!_instance)
        {
            _instance = GameObject.FindObjectOfType(typeof(C4_EnemyObjectManager)) as C4_EnemyObjectManager;
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "C4_EnemyObjectManager";
                _instance = container.AddComponent(typeof(C4_EnemyObjectManager)) as C4_EnemyObjectManager;
            }
        }
    }
}
