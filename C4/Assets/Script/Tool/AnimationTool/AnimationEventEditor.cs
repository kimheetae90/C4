using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;


public class AnimationEventEditor : MonoBehaviour
{
    GUIContent[] cbEventList;
    private ComboBox cbEventTypeControl = new ComboBox();
    private GUIStyle listStyle = new GUIStyle();


    GUIContent[] cbChildList;
    private ComboBox cbChildControl = new ComboBox();
    List<GameObject> childs;
    
    public bool AnimaitonClips = true;
    Animator animator;
    
    Vector2 clipListWindowScrollPosition;
    Vector2 clipWindowScrollPosition;
    Vector2 animEventScrollPosition;

    int currentClipIndex;
    int currentEventIndex;

    
    float scrollPos;
    float curAnimTime;
    
    // Use this for initialization
    void Start()
    {
        cbEventList = new GUIContent[2];
        
        cbEventList[0] = new GUIContent("CreateParticle");
        cbEventList[1] = new GUIContent("EventMessage"); 
        
        listStyle.normal.textColor = Color.white;
        listStyle.onHover.background =
        listStyle.hover.background = new Texture2D(2, 2);
        listStyle.padding.left =
        listStyle.padding.right =
        listStyle.padding.top =
        listStyle.padding.bottom = 4;

        //cbEventTypeControl = new ComboBox(new Rect(50, 5, 100, 20), cbEventList[0], cbEventList, "button", "box", listStyle);

        currentClipIndex = -1;
        currentEventIndex = -1;

        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            throw new ToolException("Doesn't have Animator Component");
        }

        childs = new List<GameObject>();
        Transform t  =  this.transform.FindChild("root");

        if(t == null)
        {
            throw new ToolException("Doesn't have root bone");
        }

        childs.Add(t.gameObject);


        Utils.IterateChildrenUtil.IterateChildren(t.gameObject, delegate(GameObject go) { childs.Add(go); }, true);

        cbChildList = new GUIContent[childs.Count];

        for(int i = 0 ; i < childs.Count ; ++i)
        {
            cbChildList[i] = new GUIContent(childs[i].name);
        }

      //  cbChildControl = new ComboBox(new Rect(50, 80, 100, 20), cbChildList[0], cbChildList, "button", "box", listStyle);
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

        if (currentEventIndex >= 0 && currentClipIndex >= 0 && AnimaitonClips)
        {
            GUI.Window(3, new Rect(430, 30, 200, Screen.height / 6 * 4), AnimationEventWindow, "Animation Event");
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
                    currentClipIndex = -1;
                }
                else
                {
                    currentClipIndex = i;
                }
            }
        }

        GUILayout.EndScrollView();
    }

    void AnimationClipPropertyWindow(int windowID)
    {
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];

        clipWindowScrollPosition = GUILayout.BeginScrollView(clipWindowScrollPosition, GUILayout.Width(200), GUILayout.Height(80));

        AnimationEvent[] events =  AnimationUtility.GetAnimationEvents(info.clip);

        for (int i = 0; i < events.Length; ++i)
        {
            if (GUILayout.Button(events[i].functionName, GUILayout.Width(160)))
            {
                currentEventIndex = i;
            }
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add", GUILayout.Width(160)))
        {
            var throwObjectEvent = new AnimationEvent();

            throwObjectEvent.functionName = "EventMessage";

            throwObjectEvent.time = curAnimTime;

            info.clip.AddEvent(throwObjectEvent);

            AnimationUtility.SetAnimationEvents(info.clip, AnimationUtility.GetAnimationEvents(info.clip));
        }

        
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
        curAnimTime = GUILayout.HorizontalSlider(i.normalizedTime, 0.0F, 1, GUILayout.Width(Screen.width / 3), GUILayout.Height(5));
        string length = String.Format("{0:n2}", i.length);
        GUILayout.Label(length);
        GUILayout.EndHorizontal();

        if (curAnimTime != i.normalizedTime)
        {
            animator.Play(info.clip.name, -1, curAnimTime);
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

    void AnimationEventWindow(int windowID)
    {
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];
        
        List<AnimationEvent> list = new List<AnimationEvent>(AnimationUtility.GetAnimationEvents(info.clip));

        AnimationEvent animEvent = list[currentEventIndex];

        animEventScrollPosition = GUILayout.BeginScrollView(animEventScrollPosition, GUILayout.Width(200), GUILayout.Height(Screen.height / 6 * 3));

        // 발생 시간
        // 생성 프리팹 // 본 // 옵셋 //
        GUILayout.BeginHorizontal("");
        GUILayout.Label("type : ");
        int selectedEventIndex = cbEventTypeControl.GetSelectedItemIndex();
        selectedEventIndex = cbEventTypeControl.List(
      new Rect(50, 5, 100, 20), cbEventList[selectedEventIndex].text, cbEventList, listStyle);
          
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        GUILayout.Label("time : ");
        string stringToEdit = animEvent.time.ToString();
        stringToEdit = GUI.TextField(new Rect(50, 35, 200, 20), stringToEdit, 25);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("");
        GUILayout.Label("attachBone : ");
        int selectedBoneIndex = cbChildControl.GetSelectedItemIndex();
        selectedBoneIndex = cbChildControl.List(
        new Rect(50, 80, 100, 20), cbChildList[selectedBoneIndex].text, cbChildList, listStyle);
        GUILayout.EndHorizontal();


        if(animEvent.functionName != cbEventList[selectedEventIndex].text)
        {
            animEvent.functionName = cbEventList[selectedEventIndex].text;
            AnimationUtility.SetAnimationEvents(info.clip, list.ToArray());
        }

        if (animEvent.time.ToString() != stringToEdit)
        {
            animEvent.time = Convert.ToSingle(stringToEdit);
            AnimationUtility.SetAnimationEvents(info.clip, list.ToArray());
        }

        Selection.activeGameObject = childs[selectedBoneIndex];

        GUILayout.EndScrollView();
        
        if (GUILayout.Button("Remove", GUILayout.Width(160)))
        {
            list.Remove(animEvent);

            AnimationUtility.SetAnimationEvents(info.clip, list.ToArray());

            currentEventIndex = -1;
        }
    }
}
