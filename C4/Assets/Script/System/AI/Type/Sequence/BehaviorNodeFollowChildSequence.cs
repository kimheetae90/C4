using UnityEngine;
using System.Collections;

/// <summary>
/// 자식이 트루면 트루, 자식이 false면 false
/// </summary>
public class BehaviorNodeFollowChildSequence : BehaviorNode
{
	public BehaviorNodeFollowChildSequence()
		: base()
	{
		
	}
	
	override public bool traversalNode(GameObject targetObjec)
	{
		bool bRet = true;
        for (int i = 0; i < listChilds.Count; ++i)
        {
            bool tempRet = listChilds[i].traversalNode(targetObjec);

            if (C4_AIManager.Instance.ShowAILog)
            {
                Debug.Log(listChilds[i].GetType().ToString() + " " + bRet);
            }

            bRet = bRet && tempRet;
        }

		return bRet;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeFollowChildSequence();
	}
}