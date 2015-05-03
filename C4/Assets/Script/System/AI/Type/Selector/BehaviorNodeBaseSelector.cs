using UnityEngine;
using System.Collections.Generic;

public class BehaviorNodeBaseSelector : BehaviorNode
{
    List<int> listProperlity;
    List<string> listParams;

    public BehaviorNodeBaseSelector(List<string> listParams)
    {
        listProperlity = new List<int>();
        this.listParams = listParams;
        buildProperbility();
    }

    override public bool traversalNode(GameObject targetObjec)
    {
        int count = listChilds.Count;

        if (count <= 0 && count != listProperlity.Count)
        {
            throw new BehaviorNodeException("파라미터 개수가 맞지 않습니다.");
        }

        int r = Random.Range(0, count - 1);

        return listChilds[r].traversalNode(targetObjec);
    }

    override public object Clone()
    {
        return new BehaviorNodeBaseSelector(listParams);
    }

    private void buildProperbility()
    {
        foreach (var properlity in listParams)
        {
            int iProperlity = 0;
            if (System.Int32.TryParse(properlity, out iProperlity))
            {
                listProperlity.Add(iProperlity);
            }
            
        }
    }
}