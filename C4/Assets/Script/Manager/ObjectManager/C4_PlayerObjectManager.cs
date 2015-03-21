using UnityEngine;
using System.Collections;

public class C4_PlayerObjectManager : C4_BaseObjectManager, C4_IntInitInstance
{
    private static C4_PlayerObjectManager _instance;
    public static C4_PlayerObjectManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_PlayerObjectManager)) as C4_PlayerObjectManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_PlayerObjectManager";
                    _instance = container.AddComponent(typeof(C4_PlayerObjectManager)) as C4_PlayerObjectManager;
                }
            }

            return _instance;
        }
    }

    public void initInstance()
    {
        if (!_instance)
        {
            _instance = GameObject.FindObjectOfType(typeof(C4_PlayerObjectManager)) as C4_PlayerObjectManager;
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "C4_PlayerObjectManager";
                _instance = container.AddComponent(typeof(C4_PlayerObjectManager)) as C4_PlayerObjectManager;
            }
        }
    }
}
