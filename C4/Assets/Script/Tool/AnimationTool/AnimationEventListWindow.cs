using UnityEngine;
using System.Collections;

public class AnimationEventListWindow : BaseAnimationWindow, IAnimationPropertyListener {

	// Use this for initialization
	override public void Awake()
	{
		base.Awake();
	}

	void OnGUI()
	{

	}

	public void onUpdateProperty(AnimationEditorProperty property)
	{

	}
}
