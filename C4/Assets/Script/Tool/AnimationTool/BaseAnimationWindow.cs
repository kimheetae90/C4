using UnityEngine;
using System.Collections;

public class BaseAnimationWindow : MonoBehaviour {

	public int x;
	public int y;
	public int marginX;
	public int marginY;
	public int width;
	public int height;
	public bool bShow;
	public BaseAnimationWindow parentWindow;

	protected AnimationEditorProperty property;

	virtual public void Awake()
	{
		x = 0;
		y = 0;
		width = 0;
		height = 0;
		bShow = false;
		parentWindow = null;
	}
	
}
