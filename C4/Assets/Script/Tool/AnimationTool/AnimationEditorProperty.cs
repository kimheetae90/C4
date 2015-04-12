using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationEditorProperty {

	int currentSelectClipIndex;

	public int CurrentSelectClipIndex {
		get {
			return currentSelectClipIndex;
		}
		set {
			currentSelectClipIndex = value;
			NotiyChangeProperty();
		}
	}

	int currentselectAnimationEvent;

	public int CurrentselectAnimationEvent {
		get {
			return currentselectAnimationEvent;
		}
		set {
			currentselectAnimationEvent = value;
			NotiyChangeProperty();
		}
	}

	Animator animator;
	
	public Animator Animator {
		get {
			return animator;
		}
		set {
			animator = value;
			NotiyChangeProperty();
		}
	}
	
	List<IAnimationPropertyListener> listenerList;


	public AnimationEditorProperty()
	{
		listenerList = new List<IAnimationPropertyListener> ();

		currentSelectClipIndex = -1;
		currentselectAnimationEvent = -1;
	}
	
	void NotiyChangeProperty()
	{
		foreach (var temp in listenerList) 
		{
			temp.onUpdateProperty(this);
		}
	}

	public void AddPropertyListener(IAnimationPropertyListener listener)
	{
		listenerList.Add (listener);
	}
	
}
