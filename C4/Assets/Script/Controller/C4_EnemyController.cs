using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_EnemyController : C4_Controller
{

    bool isSelected;
    int selectNum;
    [System.NonSerialized]
    public C4_Enemy selectedEnemyUnit;
    C4_StartAIBehave behavior;

    float tempValue;
    [System.NonSerialized]
    public EnemyAction action;
    bool isActing;
    bool canActing;

    public override void Awake()
    {
        base.Awake();

        isSelected = false;
        selectNum = 0;
        action = EnemyAction.Invalid;
        isActing = false;
        canActing = false;
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
                        //original here
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
                if (selectedEnemyUnit.canShot)
                {
                    canActing = true;
                }
                else
                {
                    resetSelect();
                }
                break;
            case EnemyAction.Move:
                if (selectedEnemyUnit.canMove)
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
      behavior.startBehave();
    }

    public void resetSelect()
    {
        action = EnemyAction.Invalid;
        isSelected = false;
        selectedEnemyUnit = null;
        behavior = null;
        isActing = false;
        canActing = false;
        //원래여기
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
        if (C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Enemy).getObjectCount() > 0)
        {
            selectNum++;
            if (selectNum >= C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Enemy).getObjectCount())
            {
                selectNum = 0;
            }
            selectedEnemyUnit = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Enemy).getObjectInList(selectNum).GetComponent<C4_Enemy>();
            behavior = selectedEnemyUnit.GetComponent<C4_StartAIBehave>();
            isSelected = true;
        }
        else
        {
            isSelected = false;
            
        }
    }

}
