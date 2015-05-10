using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodePreconditionFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        IBehaviorNode node = null;

        switch (className)
        {
            case "BehaviorNodeFindObjectPrecondition":
                {
                    node = new BehaviorNodeFindObjectPrecondition(listParam);
                }
                break;
            case "BehaviorNodeCheckGauge":
                {
                    node = new BehaviorNodeCheckGauge(listParam);
                }
                break;
            case "BehaviorNodeCheckRageMode":
                {
                    node = new BehaviorNodeCheckRageMode(listParam);
                }
                break;
            case "BehaviorNodeFindCloseObjectInAttackRange":
                {
                    node = new BehaviorNodeFindCloseObjectInAttackRange(listParam);
                }  
                break;
            case "BehaviorNodeIsThereAllyBetweenTargetAndMe":
                {
                    node = new BehaviorNodeIsThereAllyBetweenTargetAndMe(listParam);
                }
                break;
            case "BehaviorNodeCheckSelectedEnemyIsAimMe":
                {
                    node = new BehaviorNodeCheckSelectedEnemyIsAimMe(listParam);
                }
                break;
            case "BehaviorNodeCheckEnemyStateIsShoot":
                {
                    node = new BehaviorNodeCheckEnemyStateIsShoot(listParam);
                }
                break;
            case "BehaviorNodeBasePrecondition":
            default:
                {
                    node = new BehaviorNodeBasePrecondition(listParam);
                }
                break;
        }

        return node;
    }
}