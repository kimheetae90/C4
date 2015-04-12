using UnityEngine;
using System.Collections;

public class AnimationPlayerWindow : BaseAnimationWindow, IAnimationPropertyListener {
	
	override public void Awake()
	{
		base.Awake();
	}

	public void onUpdateProperty(AnimationEditorProperty property)
	{

	}
}
