using UnityEngine;
using System.Collections;

/// <summary>
/// 프리 오더로 읽는 기본 시퀀스 노드
/// </summary>
public class BehaviorNodeBaseSequence : BehaviorNode
{
    public BehaviorNodeBaseSequence()
        : base()
    {

    }

    override public bool traversalNode(GameObject targetObject)
    {
        foreach (var node in listChilds)
        {
			bool bRet = node.traversalNode(targetObject);

            if(C4_AIManager.Instance.ShowAILog)
            {
                Debug.Log(node.GetType().ToString() + " " + bRet);
            }
			
			if (bRet == false)
            {
                break;
            }
        }

        return true;
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseSequence();
    }
}