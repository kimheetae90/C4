using UnityEngine;
using System.Collections;

public class C4_SelectAllyMode : C4_SceneMode {

	void Awake () {
		C4_GameManager.Instance.StartPlayScene();
	}

	public override void Start()
	{
		base.Start();
	}

	public void setSelectAllyButton(C4_SelectAllyButton selectAllyButton)
	{
		if (selectAllyButton.isSelected) 
		{
			cancelSelectedAlly(selectAllyButton);
		}
		else 
		{
			selectedAllyButton(selectAllyButton);
		}
	}

	public void cancelSelectedAlly(C4_SelectAllyButton selectAllyButton)
	{
		if (C4_GameManager.Instance.selectedAlly.removeSelectedAlly (selectAllyButton.ally)) 
		{
			selectAllyButton.isSelected = false;
			//Button Image Change
			//Show Model
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
		}
		else 
		{
			//Input Sound
		}
	}
}
