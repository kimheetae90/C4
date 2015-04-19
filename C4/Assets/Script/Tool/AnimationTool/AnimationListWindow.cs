using UnityEngine;
#if UNITY_EDITOR
using System.Collections;

public class AnimationListWindow : BaseAnimationWindow , IAnimationPropertyListener {

	// Use this for initialization
	Vector2 scrollPosition;

	override public void Awake()
	{
		base.Awake();

		x = 0;
		y = 0;
		width = 200;
		height = 200;

		bShow = true;
	}
	
	void Start () 
	{
		
	}
	
	void OnGUI()
	{
		if (bShow == false)
			return;

		if (property.Animator == null)
			return;

        if (parentWindow != null && parentWindow.bShow)
        {
            marginX = parentWindow.x + parentWindow.width + 5 + parentWindow.marginX;
        }
        else
        {
            marginX = 0;
        }

		int curX = x + marginX;
		int curY = y + marginY;

		GUI.Window(0, new Rect(curX, curY, width, height), onAnimationEventWindow, "Animation List");

	}

	void onAnimationEventWindow(int windowID)
	{
		AnimatorClipInfo[] infos = property.Animator.GetCurrentAnimatorClipInfo(0);
		
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height));
		
		for (int i = 0; i < infos.Length; ++i)
		{
			if (GUILayout.Button(infos[i].clip.name, GUILayout.Width(width/5*4)))
			{
				if (i == property.CurrentSelectClipIndex) {
					property.CurrentSelectClipIndex = -1;
				}
				else {
					property.CurrentSelectClipIndex = i;
				}
			}
		}

		GUILayout.EndScrollView ();
	}

	public void onUpdateProperty(AnimationEditorProperty property)
	{
		this.property = property;

		if(this.property.Animator == null)
		{
			bShow = false;
		}
	}
}
#endif
