using UnityEngine;
using System.Collections;

public class C4_EnemyManager : C4_Manager {

    private static C4_EnemyManager _instance;
    public static C4_EnemyManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_EnemyManager)) as C4_EnemyManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_EnemyManager";
                    _instance = container.AddComponent(typeof(C4_EnemyManager)) as C4_EnemyManager;
                }
            }

            return _instance;
        }
    }

    public void initInstance()
    {
        if (!_instance)
        {
            _instance = GameObject.FindObjectOfType(typeof(C4_EnemyManager)) as C4_EnemyManager;
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "C4_EnemyManager";
                _instance = container.AddComponent(typeof(C4_EnemyManager)) as C4_EnemyManager;
            }
        }
    }

    bool isSelected;
    int selectNum;
    C4_Enemy selectedBoat;

    void Start()
    {
        isSelected = false;
        selectNum = 0;
    }

    void Update()
    {
        if (!isSelected)
        {
            selectBoat();
        }
        else
        {
            //***************************** invoke 명령
        }
    }

    void selectBoat()
    {
        for (int i = selectNum; i < objectList.Count; i++)
        {
            selectedBoat = objectList[i].GetComponent<C4_Enemy>();
            if (selectedBoat.canMove)
            {
                isSelected = true;
                selectNum = i;
                break;
            }
        }

        if (selectNum == objectList.Count)
        {
            selectNum = 0;
        }
    }
}
