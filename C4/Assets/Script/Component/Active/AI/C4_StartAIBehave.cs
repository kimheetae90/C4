using UnityEngine;
using System.Collections;

public class C4_StartAIBehave : MonoBehaviour {

    enum EnemyAction
    {
        MoveCloser,
        AttackSuccess,
        Avoid
    }

    public int checkBound;
    public int attackOrMovePercent;
    double distanceWithAlly;
    C4_Ally shortestDistanceAlly;
    C4_UnitFeature unitFeature;
    C4_Enemy enemy;

    void Start()
    {
        unitFeature = GetComponent<C4_UnitFeature>();
        enemy = GetComponent<C4_Enemy>();
    }

    public void startBehave()
    {
        startAction(decideAction());
    }

    EnemyAction decideAction()
    {
        EnemyAction action;
        checkDistanceWithPlayer();
        if (distanceWithAlly > checkBound)
        {
            action = EnemyAction.MoveCloser;
        }
        else
        {
            int attackPercent = Random.Range(0,100);
            if (attackPercent > attackOrMovePercent)
            {
                action = EnemyAction.AttackSuccess;
            }
            else
            {
                action = EnemyAction.Avoid;
            }
        }
        return action;
    }

    void startAction(EnemyAction action)
    {
        switch (action)
        {
            case EnemyAction.MoveCloser:
                Invoke("moveToPlayer", 1f);
                break;

            case EnemyAction.Avoid:
                Invoke("avoid", 1f);
                break;

            case EnemyAction.AttackSuccess:
                Invoke("attackPlayer", 1f);
                break;
        }
    }

    void checkDistanceWithPlayer()
    {
        shortestDistanceAlly = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(0).GetComponent<C4_Ally>();
        distanceWithAlly = Vector3.Distance(shortestDistanceAlly.transform.position, transform.position);
        for (int i = 0; i < C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectCount(); i++)
        {
            double checkDistanceEachAlly = Vector3.Distance(C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).transform.position, transform.position);
            if (distanceWithAlly > checkDistanceEachAlly)
            {
                distanceWithAlly = checkDistanceEachAlly;
                shortestDistanceAlly = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).GetComponent<C4_Ally>();
            }
        }
    }


    void sendCompleteMessageToController()
    {
        C4_EnemyController enemyController = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Enemy).GetComponent<C4_EnemyController>();
        enemyController.Invoke("resetSelect", 0.5f);
    }

    void attackPlayer()
    {
        Vector3 click = 2 * transform.position - shortestDistanceAlly.transform.position;

        enemy.turn(click);
        enemy.shot(click);
        sendCompleteMessageToController();
    }

    void avoid()
    {
        Vector3 allyPositionVector = (shortestDistanceAlly.transform.position - transform.position).normalized;
        Vector3 perpendicularAtAllyVector = new Vector3(allyPositionVector.z, 0, -allyPositionVector.x).normalized;
        float directionValue = Random.Range(0,2);
        float perpendicularValue = Random.Range(-5, 5);
        if(Mathf.Abs((int)perpendicularValue) < 2)
        {
            perpendicularValue = 2;
        }
        float tempValue = Random.Range(shortestDistanceAlly.GetComponent<C4_UnitFeature>().moveRange/2, shortestDistanceAlly.GetComponent<C4_UnitFeature>().moveRange);
        Vector3 toMove = new Vector3(directionValue * allyPositionVector.x + perpendicularValue * perpendicularAtAllyVector.x , 0 , directionValue * allyPositionVector.z + perpendicularValue * perpendicularAtAllyVector.z) * tempValue + transform.position;
        enemy.move(toMove);
        enemy.turn(toMove);
        sendCompleteMessageToController();
    }

    void moveToPlayer()
    {
        Vector3 toMove = (shortestDistanceAlly.transform.position - transform.position).normalized * unitFeature.moveRange * 3 + transform.position;
        enemy.turn(toMove);
        enemy.move(toMove);
        sendCompleteMessageToController();
    }
}
