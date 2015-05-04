using UnityEngine;
using System.Collections;

public class ParamControlBase : IParanControl {

    protected string controlName;
	protected bool bEditing;

    public ParamControlBase(string controlName)
    {
        this.controlName = controlName;
        bEditing = false;
    }

	public virtual void Show(int width,int height)
	{

	}

    public virtual void ShowEditingWindow(int width,int height)
	{

	}

    public bool IsEditing()
    {
        return bEditing;
    }
}
