using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_AIManager : MonoBehaviour
{

    [System.NonSerialized]
    BehaviorNodeFactory factory;
    [System.NonSerialized]
    public Dictionary<string, IBehaviorNode> DicNodes;

    public bool ShowAILog;

    private static C4_AIManager _instance;

    public static C4_AIManager Instance
    {
        get
        {
            initInstance();
            return _instance;
        }
    }

    private static void initInstance()
    {
        if (!_instance)
        {
            _instance = GameObject.FindObjectOfType(typeof(C4_AIManager)) as C4_AIManager;
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "C4_AIManager";
                _instance = container.AddComponent(typeof(C4_AIManager)) as C4_AIManager;
            }
        }
    }

    public void Awake()
    {
        factory = new BehaviorNodeFactory();
        DicNodes = new Dictionary<string, IBehaviorNode>();
        ShowAILog = false;
    }

    public IBehaviorNode LoadBehaviorNode(string behaviorFileName,GameObject targetObject)
    {
        IBehaviorNode node = null;

        if(DicNodes.TryGetValue(behaviorFileName,out node) == false)
        {
            node = createBehaviorNode(behaviorFileName);
        }

        return node;
    }

	public IBehaviorNode LoadBehaviorNode(TextAsset behaviorFile,GameObject targetObject)
	{
		IBehaviorNode node = null;
		
		if(DicNodes.TryGetValue(behaviorFile.name,out node) == false)
		{
			node = createBehaviorNode(behaviorFile);
		}
		
		return node;
	}

    private IBehaviorNode createBehaviorNode(string behaviorFileName)
    {
       string targetPath = Application.dataPath + "/" + behaviorFileName;

       IBehaviorNode node = factory.buildBehaviorNode(targetPath);

       if(node == null)
       {
           throw new BehaviorNodeException("Invalid File Path..");
       }
	   DicNodes.Remove (behaviorFileName);
       DicNodes.Add(behaviorFileName, node);

       return node;
    }

	private IBehaviorNode createBehaviorNode(TextAsset behaviorFile)
	{
		IBehaviorNode node = factory.buildBehaviorNode(behaviorFile);
		
		if(node == null)
		{
			throw new BehaviorNodeException("Invalid File Path..");
		}
		DicNodes.Remove (behaviorFile.name);
		DicNodes.Add(behaviorFile.name, node);
		
		return node;
	}

}