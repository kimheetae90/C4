using UnityEngine;
using System.Collections;

/// <summary>
/// 왼쪽이 트루면 오른쪽의 노드 들을 실행안함
/// </summary>
public class BehaviorNodeIfElseSequence : BehaviorNode
{
	public BehaviorNodeIfElseSequence()
		: base()
	{
		
	}
	
	override public bool traversalNode(GameObject targetObjec)
	{
		for(int i = 0 ; i < listChilds.Count ; ++i )
		{

			bool bRet = listChilds[i].traversalNode(targetObjec);

            if (C4_AIManager.Instance.ShowAILog)
            {
                Debug.Log(listChilds[i].GetType().ToString() + " " + bRet);
            }

            if(bRet == true)
            {
                break;
            }
		}
		
		return true;
	}
	
	override public object Clone()
	{
		return new BehaviorNodeIfElseSequence();
	}
}