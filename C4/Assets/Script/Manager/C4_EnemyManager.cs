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

    [System.NonSerialized]
    public enum Action { NULL, Attack, Move };

    bool isSelected;
    int selectNum;
    C4_Enemy selectedBoat;
    C4_StartAIBehave behavior;

    float tempValue;
    [System.NonSerialized]
    public Action action;

    void Start()
    {
        isSelected = false;
        selectNum = 0;
    }

    void Update()
    {
        if (action == Action.NULL)
        {
            chooseAction();
        }
        else
        {
            if (!isSelected)
            {
                selectBoat();
            }
            else
            {
                Invoke("startBehave", 0.5f);
            }
        }
    }

    public void startBehave()
    {
        behavior.startBehave();
    }

    public void resetSelect()
    {
        action = Action.NULL;
        isSelected = false;
        selectedBoat = null;
        behavior = null;
    }

    public void showSelect()
    { 
    }

    void chooseAction()
    {
        tempValue = Random.Range(0, 2);
        if (tempValue > 1)
        {
            action = Action.Attack;
        }
        else
        {
            action = Action.Move;
        }
    }

    void selectBoat()
    {
        for (int i = selectNum; i<objectList.Count ; i++)
        {
            selectedBoat = objectList[i].GetComponent<C4_Enemy>();
            switch (action)
            {
                case Action.Move:
                    settingSelect(selectedBoat.canMove);
                    selectNum = i;
                    break;

                case Action.Attack:
                    settingSelect(selectedBoat.canShot);
                    break;
            }
        }

        if (selectNum == objectList.Count)
        {
            selectNum = 0;
        }
    }

    void settingSelect(bool flag)
    {
        if (flag)
        {
            behavior = selectedBoat.GetComponent<C4_StartAIBehave>();
            isSelected = true;
        }
    }
}
