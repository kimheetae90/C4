using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_EnemyController : C4_Controller
{
    Queue<C4_Enemy> QueFullGageEnemy;
    C4_Enemy selectedEnemyUnit;
    bool isActing;

    public override void Awake()
    {
        base.Awake();
        isActing = false;
        QueFullGageEnemy = new Queue<C4_Enemy>();
    }

    void Update()
    {
        if (!isActing && QueFullGageEnemy.Count > 0)
        {
            selectedEnemyUnit = QueFullGageEnemy.Dequeue();
            startBehave();
        }
    }

    void resetSelect()
    {
        isActing = false;
    }

    void startBehave()
    {
        isActing = true;
        C4_StartAIBehave behavior = selectedEnemyUnit.GetComponent<C4_StartAIBehave>();
        behavior.startBehave();
    }

    public void addFullGageEnemy(C4_Enemy enemyObject)
    {
        QueFullGageEnemy.Enqueue(enemyObject);
    }
}
