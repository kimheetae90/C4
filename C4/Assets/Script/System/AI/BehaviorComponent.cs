using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviorComponent : MonoBehaviour
{
    public BehaviorCacheStruct cachedStruct;

    IBehaviorNode node;
    
    public TextAsset behaviorFile;

    // Use this for initialization
    void Start()
    {
        Init();

        clearCachedStruct();

    }

    private void Init()
    {
        try
        {
            node = C4_AIManager.Instance.LoadBehaviorNode(behaviorFile, this.gameObject);

            if (node == null)
            {
                throw new BehaviorNodeException("node is null");
            }
        }
        catch (BehaviorNodeException e)
        {
            Debug.LogError(e.Message);
        }

        gameObject.AddComponent<C4_BehaviorActionFunc>();
    }

    private void clearCachedStruct()
    {
        cachedStruct.betweenObjectInFireObjects = new List<C4_Object>();
        cachedStruct.objectsInFireRange = new List<C4_Object>();
        cachedStruct.checkedSelectedObject = null;

    }

	public void traversalNode()
	{
		if (node != null)
		{
			node.traversalNode(this.gameObject);
		}
	}

}