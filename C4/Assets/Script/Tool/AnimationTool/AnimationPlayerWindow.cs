using UnityEngine;
#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;

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
        height = 100;
       
        bShow = true;
	}

    void OnGUI()
    {
        if (bShow == false)
            return;

        if (property.Animator == null)
            return;

        x = Screen.width / 4;
        y = Screen.height / 7 * 5;
        width = Screen.width / 2;

        GUI.Window(7, new Rect(x, y, width, height), onAnimationPlayer, "Animation Play Bar");
    }

    void onAnimationPlayer(int windowID)
    {
		

        stateInfo = property.Animator.GetCurrentAnimatorStateInfo(0);


		AnimationClip clip = property.CurrentClip;

		if (stateInfo.IsName (property.CurrentClip.name))
		{
			GUILayout.Label(property.CurrentClip.name);
		}
		else
		{
			GUILayout.Label("any state");
			//property.Animator.Play(clip.name, -1, 0);
		}



		float time = stateInfo.normalizedTime;

        string curtime = String.Format("{0:n2}", time);

		string length = String.Format("{0:n2}",stateInfo.length);
					
        GUILayout.BeginHorizontal("");
        GUILayout.Label(curtime);
		curAnimTime = GUILayout.HorizontalSlider(time, 0.0F, 1, GUILayout.Width(Screen.width / 3), GUILayout.Height(5));
        GUILayout.Label(length);
        GUILayout.EndHorizontal();

        if (curAnimTime != stateInfo.normalizedTime)
        {
			property.Animator.Play(clip.name, -1, curAnimTime);
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
				property.Animator.Play(clip.name, -1, 0);
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
            stateInfo = property.Animator.GetCurrentAnimatorStateInfo(0);
			//property.Animator.CrossFade(property.CurrentClip.name,0.0f);
			property.Animator.Play(property.CurrentClip.name);

            curAnimTime = 0;
            bShow = true;
        }
	}
}
#endif