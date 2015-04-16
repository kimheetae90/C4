using UnityEngine;
using System;
using System.Collections;

public class AnimationPlayerWindow : BaseAnimationWindow, IAnimationPropertyListener {

    AnimatorClipInfo AnimClipInfo;
    AnimatorStateInfo stateInfo;

    float curAnimTime;

	override public void Awake()
	{
		base.Awake();

        x = Screen.width / 4;
        y = Screen.height / 5 * 4;
        width = Screen.width / 2;
        height = 80;
       
        bShow = true;
	}

    void OnGUI()
    {
        if (bShow == false)
            return;

        if (property.Animator == null)
            return;

        x = Screen.width / 4;
        y = Screen.height / 5 * 4;
        width = Screen.width / 2;

        GUI.Window(7, new Rect(x, y, width, height), onAnimationPlayer, "Animation Player Bar");
    }

    void onAnimationPlayer(int windowID)
    {
        stateInfo = property.Animator.GetCurrentAnimatorStateInfo(0);

        float time = (stateInfo.normalizedTime * stateInfo.length) > stateInfo.length ? stateInfo.length : (stateInfo.normalizedTime * stateInfo.length);

        string curtime = String.Format("{0:n2}", time);

        string length = String.Format("{0:n2}", stateInfo.length);

        GUILayout.BeginHorizontal("");
        GUILayout.Label(curtime);
        curAnimTime = GUILayout.HorizontalSlider(stateInfo.normalizedTime, 0.0F, 1, GUILayout.Width(Screen.width / 3), GUILayout.Height(5));
        GUILayout.Label(length);
        GUILayout.EndHorizontal();

        if (curAnimTime != stateInfo.normalizedTime)
        {
            property.Animator.Play(AnimClipInfo.clip.name, -1, curAnimTime);
            property.Animator.speed = 0;
        }

        GUILayout.BeginHorizontal("");
        if (GUILayout.Button("▶"))
        {
            if (property.Animator.speed == 0)
            {
                property.Animator.speed = 1;
            }
            else
            {
                property.Animator.Play(AnimClipInfo.clip.name, -1, 0);
            }
        }
        if (GUILayout.Button("||"))
        {
            property.Animator.speed = 0;
        }
        GUILayout.EndHorizontal();
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
            AnimClipInfo = property.Animator.GetCurrentAnimatorClipInfo(0)[property.CurrentSelectClipIndex];
            stateInfo = property.Animator.GetCurrentAnimatorStateInfo(0);
            curAnimTime = 0;
            bShow = true;
        }
	}
}
