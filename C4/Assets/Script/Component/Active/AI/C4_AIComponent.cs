using UnityEngine;
using System.Collections;

public class C4_AIComponent : MonoBehaviour
{

    IBehaviorNode node;
    public string behaviorFileName;

    // Use this for initialization
    void Start()
    {


        Init();
    }

    private void Init()
    {
        try
        {
            node = C4_AIManager.Instance.LoadBehaviorNode(behaviorFileName, this.gameObject);

            if (node == null)
            {
                throw new BehaviorNodeException("node is null");
            }
        }
        catch (BehaviorNodeException e)
        {
            Debug.LogError(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (node != null)
        {
            node.traversalNode(this.gameObject);
        }

    }
}