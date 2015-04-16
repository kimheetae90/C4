using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;

public class AnimationEventPropertyWindow : BaseAnimationWindow, IAnimationPropertyListener
{
    Vector2 scrollPosition;

    ComboBox cbEventTypeControl = new ComboBox();
    GUIContent[] cbEventType;

    List<GameObject> Bones = new List<GameObject>();
    private ComboBox cbBoneControl = new ComboBox();
    GUIContent[] cbBoneNames;


    GUIStyle listStyle = new GUIStyle();

    bool bEditEventType;
    int curEventTypeIndex = 0;

 
    bool bEditEventTime;
    string editEventTime;

    bool bEditAttachBone;
    int curBoneIndex = 0;

    bool bEditRes;

    bool bEditFollowBone;

    bool bEditScale;
    string scaleX;
    string scaleY;
    string scaleZ;

    bool bEditMsg;

    int editEventTypeWidth;
    int editEventTypeHeight;

    AnimatorClipInfo AnimClipInfo;
    List<AnimationEvent> AnimEventList = new List<AnimationEvent>();
    AnimationEvent AnimEvent;
    CustomAnimationEventParam animParam;

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


        listStyle.normal.textColor = Color.white;
        listStyle.onHover.background =
        listStyle.hover.background = new Texture2D(2, 2);
        listStyle.padding.left =
        listStyle.padding.right =
        listStyle.padding.top =
        listStyle.padding.bottom = 2;


        bEditEventType = false;
        bEditEventTime = false;
        bEditAttachBone = false;
        bEditRes = false;
        bEditFollowBone = false;
        bEditScale = false;
        bEditMsg = false;

        cbEventType = new GUIContent[4];
        cbEventType[0] = new GUIContent("CreateParticle");
        cbEventType[1] = new GUIContent("EventMessage");
        cbEventType[2] = new GUIContent("PlaySound");
        cbEventType[3] = new GUIContent("CreateColision");
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


        if (bEditEventType)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditAnimationEventType, "EditEventType");
        }

        if (bEditEventTime)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditAnimationEventTime, "EditEventTime");
        }

        if (bEditAttachBone)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditAttachBone, "EditAttachBone");
        }

        if (bEditRes)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditRes, "EditRes");
        }

        if (bEditFollowBone)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditFollowBone, "FollowBone?");
        }

        if (bEditScale)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditScale, "Scale X,Y,Z");
        }

        if (bEditMsg)
        {
            GUI.Window(4, new Rect(Screen.width / 2 - editEventTypeWidth / 2, Screen.height / 2 - editEventTypeHeight / 2, editEventTypeWidth, editEventTypeHeight), onEditMsg, "EditMsg");
        }


    }

    void onAnimationEventPropertyWindow(int windowID)
    {

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height / 6 * 5));

        eventFuncTypeProperty();

        eventTimeProperty();

        eventBoneProperty();

        eventResProperty();

        eventFollowBoneProperty();

        eventScaleProperty();

        eventMsgProperty();

        Save();

        Remove();


        GUILayout.EndScrollView();
    }


    private void Save()
    {
        if (GUILayout.Button("Save", GUILayout.Width(width / 5 * 4)))
        {
            AnimEvent.time = AnimEvent.time / AnimClipInfo.clip.length;
            AnimEvent.stringParameter = CustomAnimationEventUtil.buildString(animParam);
            AnimationUtility.SetAnimationEvents(AnimClipInfo.clip, AnimEventList.ToArray());
            AddEvent.DoAddEventImportedClip(AnimClipInfo.clip, AnimClipInfo.clip);
            bShow = false;
        }
    }

    private void Remove()
    {
        if (GUILayout.Button("Remove", GUILayout.Width(width / 5 * 4)))
        {
            AnimEventList.Remove(AnimEvent);
            AnimationUtility.SetAnimationEvents(AnimClipInfo.clip, AnimEventList.ToArray());
            AddEvent.DoAddEventImportedClip(AnimClipInfo.clip, AnimClipInfo.clip);
            clear();
        }
    }

    private void eventTimeProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("time : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(AnimEvent.time.ToString(), GUILayout.Width(width / 5 * 3)))
        {
            bEditEventTime = !bEditEventTime;
            editEventTime = AnimEvent.time.ToString();
        }

        GUILayout.EndHorizontal();
    }

    private void eventFuncTypeProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("type : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(AnimEvent.functionName, GUILayout.Width(width / 5 * 3)))
        {
            bEditEventType = !bEditEventType;

            curEventTypeIndex = getCurEventType();
        }

        GUILayout.EndHorizontal();
    }

    private void eventBoneProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("Bone : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(animParam.boneName, GUILayout.Width(width / 5 * 3)))
        {
            bEditAttachBone = !bEditAttachBone;
        }

        GUILayout.EndHorizontal();
    }

    private void eventResProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("res : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(animParam.res, GUILayout.Width(width / 5 * 3)))
        {
            bEditRes = !bEditRes;
        }

        GUILayout.EndHorizontal();
    }

    private void eventFollowBoneProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("followBone : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(animParam.followBone.ToString(), GUILayout.Width(width / 5 * 3)))
        {
            bEditFollowBone = !bEditFollowBone;
        }

        GUILayout.EndHorizontal();
    }

    private void eventScaleProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("scale : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(animParam.scale.ToString("F3"), GUILayout.Width(width / 5 * 3)))
        {
            bEditScale = !bEditScale;
            scaleX = animParam.scale.x.ToString();
            scaleY = animParam.scale.y.ToString();
            scaleZ = animParam.scale.z.ToString();
        }

        GUILayout.EndHorizontal();
    }

    private void eventMsgProperty()
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label("msg : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(animParam.msg, GUILayout.Width(width / 5 * 3)))
        {
            bEditMsg = !bEditMsg;
        }

        GUILayout.EndHorizontal();
    }

    void onEditAnimationEventType(int windowID)
    {
        curEventTypeIndex = cbEventTypeControl.List(new Rect(width / 10, height / 8, width / 5 * 4, height / 5), cbEventType[curEventTypeIndex].text, cbEventType, listStyle);

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            AnimEvent.functionName = cbEventType[curEventTypeIndex].text;

            bEditEventType = false;

            AnimationUtility.SetAnimationEvents(AnimClipInfo.clip, AnimEventList.ToArray());
        }
    }


    void onEditAnimationEventTime(int windowID)
    {

        editEventTime = GUILayout.TextField(editEventTime, GUILayout.Width(width / 5 * 4));

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            AnimEvent.time = Convert.ToSingle(editEventTime);

            bEditEventTime = false;

            AnimationUtility.SetAnimationEvents(AnimClipInfo.clip, AnimEventList.ToArray());
        }
    }

    void onEditAttachBone(int windowID)
    {
        curBoneIndex = cbBoneControl.List(new Rect(width / 10, height / 8, width / 5 * 4, height / 5), cbBoneNames[curBoneIndex].text, cbBoneNames, listStyle);

        Selection.activeGameObject = Bones[curBoneIndex];

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            animParam.boneName = cbBoneNames[curBoneIndex].text;
            bEditAttachBone = false;
        }
    }

    void onEditRes(int windowID)
    {
        animParam.res = GUILayout.TextField(animParam.res, GUILayout.Width(width / 5 * 4));

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            bEditRes = false;
        }
    }

    void onEditFollowBone(int windowID)
    {
        if (GUILayout.Button("Yes"))
        {
            animParam.followBone = true;
            bEditFollowBone = false;
        }

        if (GUILayout.Button("No"))
        {
            animParam.followBone = false;
            bEditFollowBone = false;
        }
    }

    void onEditScale(int windowID)
    {
        scaleX = GUILayout.TextField(scaleX, GUILayout.Width(width / 5 * 4));
        scaleY = GUILayout.TextField(scaleY, GUILayout.Width(width / 5 * 4));
        scaleZ = GUILayout.TextField(scaleZ, GUILayout.Width(width / 5 * 4));

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            bEditScale = false;

            animParam.scale.x = Convert.ToSingle(scaleX);
            animParam.scale.y = Convert.ToSingle(scaleY);
            animParam.scale.z = Convert.ToSingle(scaleZ);
        }
    }

    void onEditMsg(int windowID)
    {
        animParam.msg = GUILayout.TextField(animParam.msg, GUILayout.Width(width / 5 * 4));

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            bEditMsg = false;
        }
    }


    int getCurEventType()
    {
        int index = 0;

        for (int i = 0; i < cbEventType.Length; ++i)
        {
            if (AnimEvent.functionName == cbEventType[i].text)
            {
                index = i;
            }
        }

        return index;
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
            AnimClipInfo = property.Animator.GetCurrentAnimatorClipInfo(0)[property.CurrentSelectClipIndex];
            AnimEventList = new List<AnimationEvent>(AnimationUtility.GetAnimationEvents(AnimClipInfo.clip));
        }

        if (property.CurrentselectAnimationEvent == -1)
        {
            clear();
        }
        else
        {
            bShow = true;
            AnimEvent = AnimEventList[property.CurrentselectAnimationEvent];
            animParam = CustomAnimationEventUtil.buildParam(AnimEvent);
            buildFBXBone();
        }
    }

    private void clear()
    {
        bShow = false;
        bEditEventType = false;
        bEditEventTime = false;
        bEditAttachBone = false;
    }

    private void buildFBXBone()
    {
        Bones.Clear();
        Transform t = this.transform.FindChild("root");
        if (t == null)
        {
            throw new ToolException("Doesn't have root bone");
        }
        Bones.Add(t.gameObject);

        Utils.IterateChildrenUtil.IterateChildren(t.gameObject, delegate(GameObject go) { Bones.Add(go); }, true);

        cbBoneNames = new GUIContent[Bones.Count];

        for (int i = 0; i < Bones.Count; ++i)
        {
            cbBoneNames[i] = new GUIContent(Bones[i].name);
        }
    }
}
