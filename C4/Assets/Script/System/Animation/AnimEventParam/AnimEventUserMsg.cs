using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AnimEventUserMsg : AnimEventParamBase
{
    public string title;
    public int intVal;
    public float floatVal;
    public string str;
    public Vector3 vec3;
    public string boneName;

#if UNITY_EDITOR
    List<string> listBoneNames;
#endif

#if UNITY_EDITOR
    public AnimEventUserMsg(AnimationEvent animEvent, GameObject gameObject)
        : base(animEvent, gameObject)
    {
        listBoneNames = new List<string>();
        buildBones();
        InitParamControl();
        clear();
    }
#endif

    public AnimEventUserMsg()
        : base()
    {
        clear();
    }

    private void clear()
    {
        title = "";
        boneName = "";
        vec3 = Vector3.zero;
        floatVal = 0.0f;
        intVal = 0;
        str = "";
    }

    public override string Serialize()
    {
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        j.AddField("title", title);
        j.AddField("boneName", boneName);
        j.AddField("floatVal", floatVal);
        j.AddField("intVal", intVal);
        j.AddField("str", str);
        j.AddField("vec3", JSONTemplates.FromVector3(vec3));
        return j.Print(false);
    }

    public override void Deseralize(string param)
    {
        if (param == "") return;

        JSONObject j = new JSONObject(param);

        title = JSONSafeGetter.getString("title",j);
        floatVal = JSONSafeGetter.getFloat("floatVal",j);
        intVal = JSONSafeGetter.getInt("intVal",j);
        str = JSONSafeGetter.getString("str", j);
        boneName = JSONSafeGetter.getString("boneName", j);
        vec3 = JSONSafeGetter.getVector3("vec3", j);
    }

#if UNITY_EDITOR
    public override void InitParamControl()
    {
        base.InitParamControl();

        ParamControlPrimitive<string> msgNameControl = new ParamControlPrimitive<string>("title");
        msgNameControl.valueGetter = delegate() { return title; };
        msgNameControl.valueSetter = delegate(string str) { title = str; };
        AddControl(msgNameControl);

        ParamControlPrimitive<float> floatValControl = new ParamControlPrimitive<float>("float");
        floatValControl.valueGetter = delegate() { return floatVal; };
        floatValControl.valueSetter = delegate(float f) { floatVal = f; };
        AddControl(floatValControl);

        ParamControlPrimitive<int> intValControl = new ParamControlPrimitive<int>("int");
        intValControl.valueGetter = delegate() { return intVal; };
        intValControl.valueSetter = delegate(int f) { intVal = f; };
        AddControl(intValControl);

        ParamControlPrimitive<string> strControl = new ParamControlPrimitive<string>("str");
        strControl.valueGetter = delegate() { return title; };
        strControl.valueSetter = delegate(string str) { title = str; };
        AddControl(strControl);

        ParamControlVector3 vec3Control = new ParamControlVector3("vec3");
        vec3Control.valueGetter = delegate() { return vec3; };
        vec3Control.valueSetter = delegate(Vector3 str) { vec3 = str; };
        AddControl(vec3Control);

        ParamControlList<string> BoneControl = new ParamControlList<string>("Bone");
        BoneControl.valueGetter = delegate()
        {
            int index = listBoneNames.FindIndex(a => a == boneName);
            index = index == -1 ? 0 : index;
            return index;
        };

        BoneControl.valueSetter = delegate(int i) { boneName = listBoneNames[i]; };
        BoneControl.setContentList(listBoneNames);
        if (listBoneNames.Count > 0)
        {
            BoneControl.valueSetter(0);
        }
        AddControl(BoneControl);
    }

    private void buildBones()
    {
        listBoneNames.Clear();

        SkinnedMeshRenderer renderer = RefGameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        Transform root = null;

        if (renderer != null)
        {
            root = renderer.rootBone;

        }

        if (root != null)
        {
            listBoneNames.Add(root.gameObject.name);
            Utils.IterateChildrenUtil.IterateChildren(root.gameObject, delegate(GameObject go) { listBoneNames.Add(go.name); return true; }, true);
        }
    }

#endif
}