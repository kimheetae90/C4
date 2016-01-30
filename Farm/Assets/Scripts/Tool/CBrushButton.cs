using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CBrushButton : MonoBehaviour {

	public int id;

	public void SetSelectedMode()
	{
		this.GetComponent<Image> ().color = new Color (255, 0, 0);
	}

	public void SetUnSelectedMode()
	{
		this.GetComponent<Image> ().color = new Color (255, 255, 255);
	}
}
