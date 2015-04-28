using UnityEngine;
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
        AnimatorClipInfo info = property.Animator.GetCurrentAnimatorClipInfo(0)[property.CurrentSelectClipIndex];

        AnimationEvent[] events = AnimationUtility.GetAnimationEvents(info.clip);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height / 4 * 3));

        for (int i = 0; i < events.Length; ++i)
        {
            if (GUILayout.Button(events[i].functionName + " " + events[i].time, GUILayout.Width(width / 5 * 4)))
            {
                if (i == property.CurrentselectAnimationEvent)
                {
                    property.CurrentselectAnimationEvent = -1;
                }
                else
                {
                    property.CurrentselectAnimationEvent = i;
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

        if (this.property.Animator == null)
        {
            bShow = false;
        }

        if (property.CurrentSelectClipIndex == -1)
        {
            bShow = false;
        }
        else
        {
            bShow = true;
        }
    }

    void createNewEvent(AnimatorClipInfo info)
    {
        var newEvent = new AnimationEvent();
        newEvent.functionName = "EventMessage";
        newEvent.time = property.CurrentAnimationTime; 
        info.clip.AddEvent(newEvent);

        AddEvent.DoAddEventImportedClip(info.clip, info.clip);
    }
}
#endif