using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct BehaviorCacheStruct
{
    public List<C4_Object> betweenObjectInFireObjects;
    public List<C4_Object> objectsInFireRange;
    public C4_Ally checkedSelectedObject;

    public void SetAimingSelectedObject(C4_Ally obj)
    {
        checkedSelectedObject = obj;
    }

    public void ClearAimingSelectedObject()
    {
        checkedSelectedObject = null;
    }

	public C4_Object getNearestObject()
	{
		if (objectsInFireRange.Count > 0) 
		{
			return objectsInFireRange[0];
		}

		return null;
	}
}