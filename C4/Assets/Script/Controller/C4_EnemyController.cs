using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_EnemyController : C4_Controller
{

    bool isSelected;
    int selectNum;
    [System.NonSerialized]
    public C4_Enemy selectedBoat;
    C4_StartAIBehave behavior;

    float tempValue;
    [System.NonSerialized]
    public EnemyAction action;
    bool isActing;
    bool canActing;
    public GameObject enemySelectArrow;
    [System.NonSerialized]
    public C4_SelectUI selectArrow;

    void Start()
    {
        isSelected = false;
        selectNum = 0;
        action = EnemyAction.Invalid;
        isActing = false;
        canActing = false;
        enemySelectArrow = GameObject.Find("EnemySelectUI");
        selectArrow = enemySelectArrow.GetComponent<C4_SelectUI>();
    }

    void Update()
    {
        if (!isActing)
        {
            if (action == EnemyAction.Invalid)
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
                    if (canActing)
                    {
                        selectArrow.setSelect(selectedBoat);
                        isActing = true;
                        startBehave();
                    }
                    else
                    {
                        checkCanAct();
                    }
                }
            }
        }
    }

    void checkCanAct()
    {
        switch (action)
        {
            case EnemyAction.Attack:
                if (selectedBoat.canShot)
                {
                    canActing = true;
                }
                else
                {
                    resetSelect();
                }
                break;
            case EnemyAction.Move:
                if (selectedBoat.canMove)
                {
                    canActing = true;
                }
                else
                {
                    resetSelect();
                }
                break;
        }
    }

    public void startBehave()
    {
      //  behavior.startBehave();
    }

    public void resetSelect()
    {
        action = EnemyAction.Invalid;
        isSelected = false;
        selectedBoat = null;
        behavior = null;
        isActing = false;
        canActing = false;
        selectArrow.resetSelect();       
    }

    void chooseAction()
    {
        tempValue = Random.Range(0, 10);
        if (tempValue > 5)
        {
            action = EnemyAction.Attack;
        }
        else
        {
            action = EnemyAction.Move;
        }
    }

    void selectBoat()
    {
        resetSelect();
        if (C4_ManagerMaster.Instance.objectManager.getSubObjectManager(GameObjectType.Enemy).objectList.Count > 0)
        {
            selectNum++;
            if (selectNum >= C4_ManagerMaster.Instance.objectManager.getSubObjectManager(GameObjectType.Enemy).objectList.Count)
            {
                selectNum = 0;
            }
            selectedBoat = C4_ManagerMaster.Instance.objectManager.getSubObjectManager(GameObjectType.Enemy).objectList[selectNum].GetComponent<C4_Enemy>();
            behavior = selectedBoat.GetComponent<C4_StartAIBehave>();
            isSelected = true;
        }
        else
        {
            isSelected = false;
            
        }
    }

}
