using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CToolButtonController : Controller {

	CToolButton selectedTool1;
	CToolButton selectedTool2;
	CToolButton selectedTool3;

	List<CToolButton> tool1List;
	List<CToolButton> tool2List;
	List<CToolButton> tool3List;

	void Awake()
	{
		Init ();
	}

	protected void Start () {
		base.Start ();
	
	}

	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
	}

	
	public void SelectTool(CToolButton _tool)
	{
		switch (_tool.order) 
		{
		case 1:
			if (selectedTool1 != null) 
			{
				selectedTool1.SetUnSelectedMode ();
			}
			selectedTool1 = _tool;
			selectedTool1.SetSelectedMode ();
			break;
			
		case 2:
			if (selectedTool2 != null) 
			{
				selectedTool2.SetUnSelectedMode ();
			}
			selectedTool2 = _tool;
			selectedTool2.SetSelectedMode ();
			break;
			
		case 3:
			if (selectedTool3 != null) 
			{
				selectedTool3.SetUnSelectedMode ();
			}
			selectedTool3 = _tool;
			selectedTool3.SetSelectedMode ();
			break;
		}
	}

	void Init()
	{
		tool1List = new List<CToolButton> ();
		tool2List = new List<CToolButton> ();
		tool3List = new List<CToolButton> ();

		CToolButton[] tools = GameObject.FindObjectsOfType<CToolButton> ();

		foreach (CToolButton node in tools) 
		{
			switch(node.order)
			{
			case 1:
				tool1List.Add(node);
				break;
				
			case 2:
				tool2List.Add(node);
				break;
				
			case 3:
				tool3List.Add(node);
				break;
			}
		}
	}
}
