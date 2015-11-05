using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CChecker : MonoBehaviour {

	public int checkNum;
	Image img;

	void Awake()
	{
		img = GetComponent<Image> ();
	}

	public void ChangeToSelectedColor()
	{
		img.color = new Color (100, 0, 0);
	}

	public void ChangeToNonSelectedColor()
	{
		img.color = new Color (255, 255, 255);
	}
	
}
