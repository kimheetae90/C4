using UnityEngine;
using System.Collections;

public class C4_StartAIBehave : MonoBehaviour {

    public int checkBound;
    public int attackOrMovePercent;
    public int attackSuccessOrFailPercent;
    int attackPercent;
    int successAttackPercent;
    C4_Ally shortestDistanceAlly;
    double distanceWithAlly;
    double checkDistanceEachAlly;
    C4_UnitFeature unitFeature;
    C4_Enemy enemy;

    Vector3 toMove;
    Vector3 perpendicularAtAllyVector;
    Vector3 allyPositionVector;
    float angleToAlly;
    float angleToPerpendicular;
    float tempValue;

    void Start()
    {
        attackPercent = 0;
        successAttackPercent = 0;
        distanceWithAlly = 0;
        unitFeature = GetComponent<C4_UnitFeature>();
        enemy = GetComponent<C4_Enemy>();
    }

    public void startBehave()
    {
        checkDistanceWithPlayer();
        if (C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().action == EnemyAction.Move)
        {
            if (distanceWithAlly > checkBound)
            {
                if (unitFeature.stackCount == 3)
                {
                    Invoke("moveToPlayer", 1f);
                }
                else
                {
                    C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().resetSelect();
                }
            }
            else
            {
                C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().resetSelect();
            }
        }
        else
        {
            if (distanceWithAlly < checkBound)
            {
                attackPercent = Random.Range(0, 10);
                if (attackPercent > attackOrMovePercent)
                {
                    Invoke("attackPlayer", 1f);
                }
                else
                {
                    Invoke("moveBesidePlayer", 1f);
                }
            }
            else
            {
                C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().resetSelect();
            }
        }
    }

    void attackPlayer()
    {
        successAttackPercent = Random.Range(0, 10);
        if (successAttackPercent > attackSuccessOrFailPercent)
        {
            toMove = shortestDistanceAlly.transform.position;
        }
        else
        {
            perpendicularAtAllyVector = (new Vector3(allyPositionVector.z, 0, -allyPositionVector.x) - transform.position).normalized;
            tempValue = Random.Range(-5, 5);
            toMove = shortestDistanceAlly.transform.position + perpendicularAtAllyVector * tempValue;
        }

        enemy.turn(toMove);
        toMove = 2 * transform.position - toMove;
        enemy.shot(toMove);
        C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().resetSelect();
    }

    void moveBesidePlayer()
    {
        allyPositionVector = (shortestDistanceAlly.transform.position - transform.position).normalized;
        perpendicularAtAllyVector = (new Vector3(allyPositionVector.z, 0, -allyPositionVector.x) - transform.position).normalized;
        tempValue = Random.Range(0,2);
        if(tempValue >1)
        {
            angleToPerpendicular = Random.Range(1, 2);
        }
        else
        {
            angleToPerpendicular = Random.Range(-2,-1);
        }
        tempValue = Random.Range(0,2);
        if(tempValue>1)
        {
            angleToAlly = Random.Range(0, 2);
        }
        else
        {
            angleToAlly = Random.Range(-2,0);
        }
        tempValue = Random.Range(unitFeature.moveRange / 2,unitFeature.moveRange * 2 + unitFeature.moveRange / 2);
        toMove = (allyPositionVector * angleToAlly + angleToPerpendicular * perpendicularAtAllyVector).normalized * tempValue + transform.position;
        enemy.move(toMove);
        enemy.turn(toMove);
        C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().resetSelect();
    }

    void moveToPlayer()
    {
        toMove = (shortestDistanceAlly.transform.position - transform.position).normalized * unitFeature.moveRange * 3 + transform.position;
        enemy.turn(toMove);
        enemy.move(toMove);
        C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>().resetSelect();
    }

    void checkDistanceWithPlayer()
    {
        shortestDistanceAlly = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(0).GetComponent<C4_Ally>();
        distanceWithAlly = Vector3.Distance(shortestDistanceAlly.transform.position, transform.position);
        for (int i = 0; i < C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectCount(); i++)
        {
            checkDistanceEachAlly = Vector3.Distance(C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).transform.position, transform.position);
            if (distanceWithAlly > checkDistanceEachAlly)
            {
                distanceWithAlly = checkDistanceEachAlly;
                shortestDistanceAlly = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).GetComponent<C4_Ally>();
            }
        }
    }
}
