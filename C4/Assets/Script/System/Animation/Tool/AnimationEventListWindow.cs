﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;

public class AnimationEventListWindow : BaseAnimationWindow, IAnimationPropertyListener
{
    Vector2 scrollPosition;

    // Use this for initialization
    override public void Awake()
    {
        base.Awake();

        x = 0;
        y = 0;
        width = 180;
        height = 200;

        bShow = true;
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

        GUI.Window(1, new Rect(curX, curY, width, height), onAnimationEventListWindow, "Animation Event List");
    }

    void onAnimationEventListWindow(int windowID)
    {
		AnimationClip info = property.CurrentClip;

        GUILayout.Label(info.name + " : " + info.length, GUILayout.Width(width));

        AnimationEvent[] events = AnimationUtility.GetAnimationEvents(info);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height / 5 * 3));

        for (int i = 0; i < events.Length; ++i)
        {
            if (GUILayout.Button(events[i].functionName + " " + events[i].time, GUILayout.Width(width / 5 * 4)))
            {
                if (i == property.CurrentselectAnimationEvent)
                {
                    property.CurrentselectAnimationEvent = -1;
					property.CurrentEvent = null;
                }
                else
                {
                    property.CurrentselectAnimationEvent = i;
					property.CurrentEvent = events[i];
                }
            }
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add", GUILayout.Width(160)))
        {
            createNewEvent(info);
        }

    }

    public void onUpdateProperty(AnimationEditorProperty property)
    {
		this.property = property;

		bShow = property.CurrentClip != null ? true : false; 
    }

    void createNewEvent(AnimationClip info)
    {
        var newEvent = new AnimationEvent();
        newEvent.functionName = AnimEventParamFactory.getEventList()[0];
        newEvent.time = 0; 
		info.AddEvent(newEvent);

        AddEvent.DoAddEventImportedClip(info, info);
    }
}
#endif