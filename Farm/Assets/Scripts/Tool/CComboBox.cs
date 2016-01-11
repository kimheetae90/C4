using UnityEngine;
using System.Collections;

public class CComboBox : MonoBehaviour {

	public string name;
	public string[] items;
	public Rect Box;
	public string slectedItem;
	
	private bool editing = false;

	SceneManager sceneManager;

	void Start()
	{
		sceneManager = GameObject.FindObjectOfType (typeof(SceneManager)) as SceneManager;
	}

	private void OnGUI()
	{
		if (GUI.Button(Box, slectedItem))
		{
			editing = true;
		}
		
		if (editing)
		{
			for (int x = 0; x < items.Length; x++)
			{
				if (GUI.Button(new Rect(Box.x, (Box.height * x) + Box.y + Box.height, Box.width, Box.height), items[x]))
				{
					slectedItem = items[x];
					editing = false;

					GameMessage selectMessage = GameMessage.Create(MessageName.LevelTool_SelectStageInfo);
					selectMessage.Insert("infoClass",name);
					selectMessage.Insert("value",int.Parse(slectedItem));
					sceneManager.DispatchGameMessage(selectMessage);
				}
			}
		}
		
	}
}
