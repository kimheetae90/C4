using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationEventParamBase
{	
	#if UNITY_EDITOR 
	List<ParamControlBase> ListControl;
	#endif

	#if UNITY_EDITOR 
	public AnimationEventParamBase(string functionName)
	{

	}
	#else
	public AnimationEventParamBase()
	{
		
	}
	#endif

	public virtual void Serialize()
	{

	}

	public virtual void Deseralize()
	{
		
	}

	#if UNITY_EDITOR 
	public virtual void InitParamControl()
	{
		ListControl = new List<ParamControlBase> ();

	}

	public void OnShowParamControls(int parentWidth, int parentHeight)
	{
		for (int i = 0; i < ListControl.Count; ++i) 
		{
			ListControl[i].Show(parentWidth,parentHeight);
		}
	}

	public virtual void OnDrawEditControl(int windowId)
	{
		for (int i = 0; i < ListControl.Count; ++i) 
		{
			if(ListControl[i].Editing)
			{
				ListControl[i].ShowEditingWindow(windowId);
				break;
			}
		}
	}
	#endif
}
