using UnityEngine;
using System.Collections;

public interface IAnimationPropertyListener 
{
	void onUpdateProperty(AnimationEditorProperty property);
}