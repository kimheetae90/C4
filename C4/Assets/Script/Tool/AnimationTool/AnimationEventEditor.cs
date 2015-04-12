using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;


public class AnimationEventEditor : MonoBehaviour
{
	bool bAnimationEventPropertyWindow = false;
	string editingText = "";
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
        cbEventList = new GUIContent[3];
        
        cbEventList[0] = new GUIContent("CreateParticle");
        cbEventList[1] = new GUIContent("EventMessage"); 
		cbEventList[2] = new GUIContent("PlaySound"); 

		listStyle.normal.textColor = Color.white;
        listStyle.onHover.background =
        listStyle.hover.background = new Texture2D(2, 2);
        listStyle.padding.left =
        listStyle.padding.right =
        listStyle.padding.top =
        listStyle.padding.bottom = 2;

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
            GUI.Window(0, new Rect(10, 30, 200, 130), AnimationClipsWindow, "AnimationClips");


        if (currentClipIndex >= 0 && AnimaitonClips)
        {
            GUI.Window(1, new Rect(220, 30, 200, 130), AnimationEventsWindow, "Animation Events");
        }

		if (animator.GetCurrentAnimatorClipInfo (0).Length <= currentClipIndex)
			return;

        if(currentClipIndex >= 0)
        {
            GUI.Window(2, new Rect(Screen.width / 4, Screen.height / 5 * 4, Screen.width / 2, 80), AnimationPlayBarWindow, "");
        }

        if (currentEventIndex >= 0 && currentClipIndex >= 0 && AnimaitonClips)
        {
            GUI.Window(3, new Rect(430, 30, 200, 300), AnimationEventPropertyWindow, "Animation Event");
        }
    }

    void AnimationClipsWindow(int windowID)
    {
        AnimatorClipInfo[] infos = animator.GetCurrentAnimatorClipInfo(0);

        clipListWindowScrollPosition = GUILayout.BeginScrollView(clipListWindowScrollPosition, GUILayout.Width(200), GUILayout.Height(100));
	
        for (int i = 0; i < infos.Length; ++i)
        {
            if (GUILayout.Button(infos[i].clip.name, GUILayout.Width(160)))
            {
				SelectIndex (ref currentClipIndex,i);
            }
        }

        GUILayout.EndScrollView();
    }

	void SelectIndex (ref int index, int selectedIndex)
	{
		if (index == selectedIndex) {
			index = -1;
		}
		else {
			index = selectedIndex;
		}
	}

    void AnimationEventsWindow(int windowID)
    {
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];

		AnimationEvent[] events =  AnimationUtility.GetAnimationEvents(info.clip);

		clipWindowScrollPosition = GUILayout.BeginScrollView(clipWindowScrollPosition, GUILayout.Width(200), GUILayout.Height(80));

        for (int i = 0; i < events.Length; ++i)
        {
            if (GUILayout.Button(events[i].functionName + " " + events[i].time, GUILayout.Width(160)))
            {
				SelectIndex(ref currentEventIndex,i);
				bAnimationEventPropertyWindow = false;
            }
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add", GUILayout.Width(160)))
        {
			createNewEvent (info);
        }
    }

	void createNewEvent (AnimatorClipInfo info)
	{
		var throwObjectEvent = new AnimationEvent ();
		throwObjectEvent.functionName = "EventMessage";
		throwObjectEvent.time = curAnimTime;
		info.clip.AddEvent (throwObjectEvent);
		AnimationUtility.SetAnimationEvents (info.clip, AnimationUtility.GetAnimationEvents (info.clip));
	}

    void AnimationPlayBarWindow(int windowID)
    {
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

		float time =  (stateInfo.normalizedTime * stateInfo.length) > stateInfo.length ? stateInfo.length : (stateInfo.normalizedTime * stateInfo.length);

		string curtime = String.Format("{0:n2}", time);

		string length = String.Format("{0:n2}", stateInfo.length);

        GUILayout.BeginHorizontal("");
        GUILayout.Label(curtime);
        curAnimTime = GUILayout.HorizontalSlider(stateInfo.normalizedTime, 0.0F, 1, GUILayout.Width(Screen.width / 3), GUILayout.Height(5));
        GUILayout.Label(length);
        GUILayout.EndHorizontal();

        if (curAnimTime != stateInfo.normalizedTime)
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

    void AnimationEventPropertyWindow(int windowID)
    {

        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[currentClipIndex];
        
        List<AnimationEvent> list = new List<AnimationEvent>(AnimationUtility.GetAnimationEvents(info.clip));

        AnimationEvent animEvent = list[currentEventIndex];

        animEventScrollPosition = GUILayout.BeginScrollView(animEventScrollPosition, GUILayout.Width(200), GUILayout.Height(230));

		int curEventTypeIndex = 0;
		if (bAnimationEventPropertyWindow) {
			curEventTypeIndex = cbEventTypeControl.GetSelectedItemIndex();
		}
		else {
			bAnimationEventPropertyWindow = true;
			curEventTypeIndex = getCurEventType (animEvent);
			editingText = animEvent.time.ToString();
		}

		int selectedBoneIndex = cbChildControl.GetSelectedItemIndex();
	
        // 발생 시간
        // 생성 프리팹 // 본 // 옵셋 //
        GUILayout.BeginHorizontal("");
        GUILayout.Label("type : ");
		curEventTypeIndex = cbEventTypeControl.List(new Rect(50, 5, 100, 20), cbEventList[curEventTypeIndex].text, cbEventList, listStyle);     
        GUILayout.EndHorizontal();

		GUILayout.Space (50);

        GUILayout.BeginHorizontal("");
		GUILayout.Label("time : ",GUILayout.Width(40));
		editingText = GUILayout.TextField( editingText,GUILayout.Width(80));
        GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("");
		GUILayout.Label("resource : ",GUILayout.Width(65));
		editingText = GUILayout.TextField( editingText,GUILayout.Width(80));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("");
		GUILayout.Label("bone : ",GUILayout.Width(40));
		selectedBoneIndex = cbChildControl.List(new Rect(50, 145, 100, 20), cbChildList[selectedBoneIndex].text, cbChildList, listStyle);
        GUILayout.EndHorizontal();

        Selection.activeGameObject = childs[selectedBoneIndex];

		GUILayout.EndScrollView();
        
		if (GUILayout.Button ("Save", GUILayout.Width (160))) 
		{
			updateAnimEventFunctionName (info, list, animEvent, curEventTypeIndex);
			
			updateAnimEventTime (info, list, animEvent, editingText);
		}

        if (GUILayout.Button("Remove", GUILayout.Width(160)))
        {
            list.Remove(animEvent);

            AnimationUtility.SetAnimationEvents(info.clip, list.ToArray());

            currentEventIndex = -1;
        }
    }

	void updateAnimEventFunctionName (AnimatorClipInfo info, List<AnimationEvent> list, AnimationEvent animEvent, int curEventTypeIndex)
	{
		if (animEvent.functionName != cbEventList [curEventTypeIndex].text) {
			animEvent.functionName = cbEventList [curEventTypeIndex].text;
			AnimationUtility.SetAnimationEvents (info.clip, list.ToArray ());
		}
	}

	void updateAnimEventTime (AnimatorClipInfo info, List<AnimationEvent> list, AnimationEvent animEvent, string stringToEdit)
	{
		if (animEvent.time.ToString () != stringToEdit) {
			animEvent.time = Convert.ToSingle (stringToEdit);
			AnimationUtility.SetAnimationEvents (info.clip, list.ToArray ());
		}
	}

	int getCurEventType (AnimationEvent animEvent)
	{
		int index = 0;

		for (int i = 0; i < cbEventList.Length; ++i) {
			if (animEvent.functionName == cbEventList [i].text) {
				index = i;
			}
		}

		return index;
	}
}
