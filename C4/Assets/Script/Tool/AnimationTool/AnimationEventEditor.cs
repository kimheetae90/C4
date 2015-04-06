using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class AnimationEventEditor : MonoBehaviour
{

    public bool AnimaitonClips = true;
    Animator animator;
    Animation anim;

    Vector2 clipListWindowScrollPosition;
    Vector2 clipWindowScrollPosition;

    int currentClipIndex;
    int currentEventIndex;
    float scrollPos;
    bool isPause = false;

    // Use this for initialization
    void Start()
    {
        currentClipIndex = -1;
        currentEventIndex = -1;

        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        
        if (animator == null)
        {
            throw new ToolException("Doesn't have Animator Component");
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnGUI()
    {

        AnimaitonClips = GUI.Toggle(new Rect(10, 10, 100, 20), AnimaitonClips, "AnimationClips");

        if (AnimaitonClips)
            GUI.Window(0, new Rect(10, 30, 200, 130), AnimationClipListWindow, "AnimationClips");


        if (currentClipIndex >= 0 && AnimaitonClips)
        {
            GUI.Window(1, new Rect(220, 30, 200, 130), AnimationClipPropertyWindow, "Animation Events");
        }

        if(currentClipIndex >= 0)
        {
            GUI.Window(2, new Rect(Screen.width / 4, Screen.height / 5 * 4, Screen.width / 2, 80), AnimationPlayBarWindow, "");
        }
    }

    void AnimationClipListWindow(int windowID)
    {
        AnimatorClipInfo[] infos = animator.GetCurrentAnimatorClipInfo(0);

        clipListWindowScrollPosition = GUILayout.BeginScrollView(clipListWindowScrollPosition, GUILayout.Width(200), GUILayout.Height(100));


        for (int i = 0; i < infos.Length; ++i)
        {
            if (GUILayout.Button(infos[i].clip.name, GUILayout.Width(160)))
            {
                if (currentClipIndex == i)
                {
                   // currentClipIndex = -1;
                   // anim.RemoveClip(infos[i].clip.name);
                }
                else
                {
                    currentClipIndex = i;
                   // anim.AddClip(infos[i].clip, infos[i].clip.name);
                }
            }
        }

        GUILayout.EndScrollView();
    }

    void AnimationClipPropertyWindow(int windowID)
    {
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];

        clipWindowScrollPosition = GUILayout.BeginScrollView(clipWindowScrollPosition, GUILayout.Width(200), GUILayout.Height(80));

/*
        for(int i = 0; i < info.clip.events.Length ; ++i)
        {
            //if (GUILayout.Button(info.clip.events[i].stringParameter, GUILayout.Width(160)))
            {
                //excute
            }
        }*/

        GUILayout.EndScrollView();

    }

    void AnimationPlayBarWindow(int windowID)
    {
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];

        AnimatorStateInfo i = animator.GetCurrentAnimatorStateInfo(0);

        GUILayout.BeginHorizontal("");

        float t = (i.normalizedTime * i.length);

        if(t > i.length)
        {
            t = i.length;
        }

        string curtime = String.Format("{0:n2}", t);
        GUILayout.Label(curtime);
        float temp = GUILayout.HorizontalSlider(i.normalizedTime, 0.0F, 1, GUILayout.Width(Screen.width / 3), GUILayout.Height(5));
        string length = String.Format("{0:n2}", i.length);
        GUILayout.Label(length);
        GUILayout.EndHorizontal();

        if (temp != i.normalizedTime)
        {
            animator.Play(info.clip.name, -1, temp);
            animator.speed = 0;
        }

        GUILayout.BeginHorizontal("");
        if(GUILayout.Button("▶"))
        {
            if (animator.speed == 0)
            {
                animator.speed = 1;
            }
            else
            {
                animator.Play(info.clip.name, -1, 0);
            }            
        }
        if (GUILayout.Button("||"))
        {
            animator.speed = 0;
        }
        GUILayout.EndHorizontal();
    }
}
