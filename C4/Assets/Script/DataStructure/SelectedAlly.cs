using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SelectedAlly{

	int maxNumOfAlly;
	List<C4_Ally> selectedAllyList;

	void Start()
	{
		selectedAllyList = new List<C4_Ally> ();
	}

	public void refreshAllyList()
	{
		selectedAllyList.Clear ();
	}

	public bool addSelectedAlly(C4_Ally selectedAllyObject)
	{
		if (selectedAllyList.Count < maxNumOfAlly) 
		{
			selectedAllyList.Add (selectedAllyObject);
			return true;
		}
		else 
		{
			return false;
		}
	}

	public bool removeSelectedAlly(C4_Ally selectedAllyObject)
	{
		return selectedAllyList.Remove (selectedAllyObject);
	}

	public int getMaxNumOfAlly()
	{
		return maxNumOfAlly;
	}

	public void setMaxNumOfAlly(int value)
	{
		maxNumOfAlly = value;
	}
}
