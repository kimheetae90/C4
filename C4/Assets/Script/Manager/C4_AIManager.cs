using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_AIManager : MonoBehaviour
{

    [System.NonSerialized]
    BehaviorNodeFactory factory;
    [System.NonSerialized]
    public Dictionary<string, IBehaviorNode> DicNodes;

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
                container.name = "C4_EnemyManager";
                _instance = container.AddComponent(typeof(C4_AIManager)) as C4_AIManager;
            }
        }
    }

    public void Awake()
    {
        factory = new BehaviorNodeFactory();
        DicNodes = new Dictionary<string, IBehaviorNode>();
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

    private IBehaviorNode createBehaviorNode(string behaviorFileName)
    {
       string targetPath = Application.dataPath + "/" + behaviorFileName;

       IBehaviorNode node = factory.buildBehaviorNode(targetPath);

       if(node == null)
       {
           throw new BehaviorNodeException("Invalid File Path..");
       }

       DicNodes.Add(behaviorFileName, node);

       return node;
    }

}