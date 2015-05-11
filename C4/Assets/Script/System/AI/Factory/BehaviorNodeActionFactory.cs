using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeActionFactory : IBehaviorNodeFactory
{
    public IBehaviorNode createNode(string className, List<string> listParam)
    {
        IBehaviorNode node = null;

        switch(className)
        {
            case "BehaviorNodeMoveToNearObjectAction":
                {
                    node = new BehaviorNodeMoveToNearObjectAction(listParam);
                }
                break;
            case "BehaviorNodeAttackObject":
                {
                    node = new BehaviorNodeAttackObject(listParam);
                }
                break;
            case "BehaviorNodeMissAttackObject":
                {
                    node = new BehaviorNodeMissAttackObject(listParam);
                }
                break;
            case "BehaviorNodeMoveForAvoid":
                {
                    node = new BehaviorNodeMoveForAvoid(listParam);
                }
                break;
			case "BehaviorNodeAttackCloseObject":
				{
					node = new BehaviorNodeAttackCloseObject(listParam);
				}
				break;
			case "BehaviorNodeMissAttackCloseObject":
				{
					node = new BehaviorNodeMissAttackCloseObject(listParam);
				}
				break;
            case "BehaviorNodeBaseAction":
            default:
                {
					throw new BehaviorNodeException(className + " is not exist");
				}
        }

        return node;
    }
}