using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeBaseSelector : BehaviorNode
{
    List<int> listProperlity;
    string param;

    public BehaviorNodeBaseSelector(GameObject targetObject, string param)
        : base(targetObject)
    {
        listProperlity = new List<int>();
        this.param = param;
        buildProperbility(param);
    }

    override public bool traversalNode()
    {
        int count = listChild.Count;

        if (count <= 0 && count != listProperlity.Count)
        {
            throw new BehaviorNodeException("파라미터 개수가 맞지 않습니다.");
        }

        int r = Random.Range(0, count - 1);

        return listChild[r].traversalNode();
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseSelector(targetObject, param);
    }

    private void buildProperbility(string param)
    {
        string[] properlities = param.Split('@');

        foreach(var properlity in properlities)
        {
            int iProperlity = 0;
            if (System.Int32.TryParse(properlity, out iProperlity))
            {
                listProperlity.Add(iProperlity);
            }
            
        }
    }
}