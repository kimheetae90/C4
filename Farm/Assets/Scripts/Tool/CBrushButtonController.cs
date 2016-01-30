using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CBrushButtonController : Controller {

	public Dictionary<int,CBrushButton> brushButtonDic;
	CBrushButton selectedBrush;
	public Texture nullGridImage;

	void Awake()
	{
		InitBrushButtonList ();
	}

	protected override void Start () {
		base.Start ();
	}
	
	public override void DispatchGameMessage (GameMessage _gameMessage)
	{
		switch (_gameMessage.messageName) 
		{
		case MessageName.Tool_DrawGrid:
			DrawGrid(_gameMessage.Get("grid") as GameObject);
			break;
		case MessageName.Tool_EraseGrid:
			EraseGrid(_gameMessage.Get("grid") as GameObject);
			break;
		}
	}

	void DrawGrid(GameObject _gridGOBJ)
	{
		CGrid tempGrid = _gridGOBJ.GetComponent<CGrid> ();

		if (selectedBrush != null) 
		{
			tempGrid.SetMarker (selectedBrush.GetComponent<Image> ().mainTexture, selectedBrush.id);
			GameMessage drawGrid = GameMessage.Create(MessageName.Tool_DrawGrid);
			drawGrid.Insert("grid",tempGrid);
			drawGrid.Insert("id",selectedBrush.id);
			SendGameMessage(drawGrid);
		}
	}

	void EraseGrid(GameObject _gridGOBJ)
	{
		CGrid tempGrid = _gridGOBJ.GetComponent<CGrid> ();
		tempGrid.SetMarkerNull (nullGridImage);

		GameMessage drawGrid = GameMessage.Create(MessageName.Tool_EraseGrid);
		drawGrid.Insert("grid",tempGrid);
		SendGameMessage(drawGrid);
	}

	void InitBrushButtonList()
	{
		brushButtonDic = new Dictionary<int, CBrushButton> ();

		CBrushButton[] buttons = GameObject.FindObjectsOfType<CBrushButton> ();

		foreach (CBrushButton tempObject in buttons) 
		{
			brushButtonDic.Add(tempObject.id,tempObject);
		}
	}

	public void SelectBrush(CBrushButton _brushButton)
	{
		if (selectedBrush != null) 
		{
			selectedBrush.SetUnSelectedMode ();
		}

		selectedBrush = _brushButton;
		selectedBrush.SetSelectedMode ();
	}

}
