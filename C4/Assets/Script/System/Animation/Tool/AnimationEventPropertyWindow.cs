using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;

public class AnimationEventPropertyWindow : BaseAnimationWindow, IAnimationPropertyListener
{
    Vector2 scrollPosition;

    int editEventTypeWidth;
    int editEventTypeHeight;

    AnimationClip AnimClip;
    List<AnimationEvent> AnimEventList = new List<AnimationEvent>();
    
    AnimEventParamBase animParmBase = new AnimEventParamBase();

    ParamControlList<string> typeControl;

    override public void Awake()
    {
        base.Awake();

        x = 0;
        y = 0;
        width = 200;
        height = 200;

        bShow = true;

        editEventTypeWidth = 200;
        editEventTypeHeight = 200;

        typeControl = new ParamControlList<string>("type");
        typeControl.valueGetter = delegate() { return AnimEventParamFactory.getEventList().FindIndex(a => a == animParmBase.RefAnimEvent.functionName); };
        typeControl.valueSetter = delegate(int i)
        {
            if (animParmBase.RefAnimEvent == null || animParmBase.RefGameObject == null) return;

			animParmBase = AnimEventParamFactory.CreateParam(AnimEventParamFactory.getEventList()[i], animParmBase.RefAnimEvent, this.gameObject);

            animParmBase.RefAnimEvent.functionName = AnimEventParamFactory.getEventList()[i];
		
			animParmBase.Deseralize(animParmBase.RefAnimEvent.stringParameter);
        };
        typeControl.setContentList(AnimEventParamFactory.getEventList());
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

        GUI.Window(3, new Rect(curX, curY, width, height), onAnimationEventPropertyWindow, "Property");

        if (typeControl.IsEditing() || animParmBase.IsEditing())
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEdit, "Edit");
        }
    }

    void onAnimationEventPropertyWindow(int windowID)
    {

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height / 6 * 5));

        if (typeControl != null)
        {
            typeControl.Show(width, height);
        }

        if (animParmBase != null)
        {
            animParmBase.OnShowParamControls(width, height);
        }

        Save();

        Remove();

        GUILayout.EndScrollView();
    }


    private void Save()
    {
        if (GUILayout.Button("Save", GUILayout.Width(width / 5 * 4)))
        {     
            animParmBase.RefAnimEvent.stringParameter = animParmBase.Serialize();
            AnimationUtility.SetAnimationEvents(property.CurrentClip, AnimEventList.ToArray());
            AddEvent.DoAddEventImportedClip(property.CurrentClip, property.CurrentClip);
            bShow = false;
        }
    }

    private void Remove()
    {
        if (GUILayout.Button("Remove", GUILayout.Width(width / 5 * 4)))
        {
            AnimEventList.Remove(animParmBase.RefAnimEvent);
            AnimationUtility.SetAnimationEvents(AnimClip, AnimEventList.ToArray());
            AddEvent.DoAddEventImportedClip(AnimClip, AnimClip);
            clear();
        }
    }

    void onEdit(int windowID)
    {
        if(typeControl.IsEditing())
        {
            typeControl.ShowEditingWindow(width, height);
        }
        else
        {
            animParmBase.OnShowEditControl(width, height);
        }
    }

    public void onUpdateProperty(AnimationEditorProperty property)
    {
        this.property = property;

        if (this.property.Animator == null)
        {
            bShow = false;
        }

        if (property.CurrentSelectClipIndex == -1)
        {
            clear();
        }
        else
        {
			AnimClip = property.CurrentClip;
            AnimEventList = new List<AnimationEvent>(AnimationUtility.GetAnimationEvents(AnimClip));
        }

        if (property.CurrentselectAnimationEvent == -1)
        {
            clear();
        }
        else
        {
            bShow = true;
            AnimationEvent AnimEvent = AnimEventList[property.CurrentselectAnimationEvent];
            animParmBase = AnimEventParamFactory.CreateParam(AnimEvent.functionName, AnimEvent,this.gameObject);
            animParmBase.Deseralize(AnimEvent.stringParameter);

			typeControl.valueSetter(typeControl.valueGetter());

        }
    }

    private void clear()
    {
        bShow = false;
    }
}
#endif