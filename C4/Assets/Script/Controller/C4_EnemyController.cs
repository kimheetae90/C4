using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_EnemyController : C4_Controller
{
    Queue<C4_Enemy> QueFullGageEnemy;
    public C4_Enemy selectedEnemyUnit;
    public float EnemyTurnExcuteTime = 2.0f;

    public override void Awake()
    {
        base.Awake();
        QueFullGageEnemy = new Queue<C4_Enemy>();

        StartCoroutine(ExcuteEnemeyTurn());
    }

    void resetSelect()
    {
    }

    void startBehave()
    {
        Debug.Log("START_BEHAVE");

        if (selectedEnemyUnit == null) return;

        BehaviorComponent behavior = selectedEnemyUnit.GetComponent<BehaviorComponent>();

        if (behavior != null)
        {
            behavior.traversalNode();

            selectedEnemyUnit.SendGageFullMessageToController = false;

            resetSelect();
        }
    }

    public void addFullGageEnemy(C4_Enemy enemyObject)
    {
        QueFullGageEnemy.Enqueue(enemyObject);
    }

    IEnumerator ExcuteEnemeyTurn()
    {
        while (C4_GameManager.Instance.IsPlaying)
        {
            if (QueFullGageEnemy.Count > 0)
            {
                selectedEnemyUnit = QueFullGageEnemy.Dequeue();
              
                startBehave();

                yield return new WaitForSeconds(EnemyTurnExcuteTime);
            }
            else
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }

}

