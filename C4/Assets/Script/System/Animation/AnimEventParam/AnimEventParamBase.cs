using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimEventParamBase 
{
#if UNITY_EDITOR
    AnimationEvent refAnimEvent;
    GameObject refGameObject;

    public UnityEngine.AnimationEvent RefAnimEvent
    {
        get { return refAnimEvent; }
        set { refAnimEvent = value; }
    }

    public UnityEngine.GameObject RefGameObject
    {
        get { return refGameObject; }
        set { refGameObject = value; }
    }

    List<IParanControl> ListControl;
#endif

#if UNITY_EDITOR
    public AnimEventParamBase(AnimationEvent animEvent, GameObject gameObject)
    {
        RefAnimEvent = animEvent;
        RefGameObject = gameObject;
    }
#endif

    public AnimEventParamBase()
	{

    }


    public virtual string Serialize()
    {
        return "";
    }

    public virtual void Deseralize(string param)
    {

    }

#if UNITY_EDITOR
    public virtual void InitParamControl()
    {
        ListControl = new List<IParanControl>();

        ParamControlPrimitive<float> timeControl = new ParamControlPrimitive<float>("time");
        timeControl.valueGetter = delegate() { return RefAnimEvent.time; };
        timeControl.valueSetter = delegate(float f) { RefAnimEvent.time = f; };
        AddControl(timeControl);
    }

    public void OnShowParamControls(int width, int height)
    {
        for (int i = 0; i < ListControl.Count; ++i)
        {
            ListControl[i].Show(width, height);
        }
    }

    public virtual void OnShowEditControl(int width, int height)
    {
        for (int i = 0; i < ListControl.Count; ++i)
        {
            if (ListControl[i].IsEditing())
            {
                ListControl[i].ShowEditingWindow(width,height);
                break;
            }
        }
    }

    public bool IsEditing()
    {
        bool isEditing = false;

        for (int i = 0; i < ListControl.Count; ++i)
        {
            if (ListControl[i].IsEditing())
            {
                isEditing = true;
                break;
            }
        }

        return isEditing;
    }

    public void AddControl(IParanControl control)
    {
        ListControl.Add(control);
    }


    protected int getCurEventType()
    {
        int index = 0;

        List<string> list = AnimEventParamFactory.getEventList();

        for (int i = 0; i < list.Count; ++i)
        {
            if (RefAnimEvent.functionName == list[i])
            {
                index = i;
            }
        }

        return index;
    }

#endif
}
