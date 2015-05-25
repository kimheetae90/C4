using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_SelectAllyMode : C4_SceneMode {

	void Awake () 
	{
		C4_GameManager.Instance.StartSelectAllyMode();
	}

	public override void Start()
	{
		base.Start();
	}

	public void setSelectAllyButton(C4_SelectAllyButton selectAllyButton)
	{
		if (selectAllyButton.isSelected) 
		{
			cancleSelectedAlly(selectAllyButton);
		}
		else 
		{
			selectedAllyButton(selectAllyButton);
		}
	}

	public void cancleSelectedAlly(C4_SelectAllyButton selectAllyButton)
	{
		if (C4_GameManager.Instance.selectedAlly.removeSelectedAlly (selectAllyButton.ally)) 
		{
			selectAllyButton.isSelected = false;
			//Button Image Change
			//Show Model
			Debug.Log("cancle SelectedAlly");
		}
		else 
		{
			//Error
		}
	}
	
	public void selectedAllyButton(C4_SelectAllyButton selectAllyButton)
	{
		if (C4_GameManager.Instance.selectedAlly.addSelectedAlly (selectAllyButton.ally)) 
		{
			selectAllyButton.isSelected = true;
			//Button Image Change
			//Hide Model
			Debug.Log("add SelectedAlly");
		}
		else 
		{
			//Input Sound
		}
	}
}
