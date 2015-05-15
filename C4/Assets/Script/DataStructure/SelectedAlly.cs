using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SelectedAlly{

	int maxNumOfAlly;
	List<GameObject> selectedAllyList;

	public void printSelectedAllylist()
	{
		Debug.Log (selectedAllyList.Count);
		for (int i=0; i<selectedAllyList.Count; i++) 
		{
			Debug.Log(selectedAllyList[i].name);
		}
	}

	public SelectedAlly(int num)
	{
		maxNumOfAlly = num;
		selectedAllyList = new List<GameObject> ();
	}

	public void refreshAllyList()
	{
		selectedAllyList.Clear ();
	}

	public bool addSelectedAlly(GameObject selectedAllyObject)
	{
		if (selectedAllyList.Count < maxNumOfAlly) 
		{
			if(selectedAllyList.Contains(selectedAllyObject))
			{
				Debug.Log("Already Exist");
				return false;
			}
			else
			{
				selectedAllyList.Add (selectedAllyObject);
				return true;
			}
		}
		else 
		{
			Debug.Log("List Full");
			return false;
		}
	}

	public bool removeSelectedAlly(GameObject selectedAllyObject)
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
