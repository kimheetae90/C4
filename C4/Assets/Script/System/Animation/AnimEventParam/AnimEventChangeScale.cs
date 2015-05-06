using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AnimEventChangeScale : AnimEventParamBase
{
    public Vector3 toScale;
    public Vector3 fromScale;
    public float changeTime;
    public string boneName;

    public float elapsedTime;
#if UNITY_EDITOR
    List<string> listBoneNames;
#endif

#if UNITY_EDITOR
    public AnimEventChangeScale(AnimationEvent animEvent, GameObject gameObject)
        : base(animEvent, gameObject)
    {
        listBoneNames = new List<string>();
        buildBones();
        InitParamControl();
        clear();
    }
#endif

    public AnimEventChangeScale()
        : base()
    {
        clear();
    }

    private void clear()
    {
        boneName = "";
        changeTime = 0.0f;
        toScale = Vector3.one;
        fromScale = Vector3.one;
        elapsedTime = 0.0f;
    }

    public override string Serialize()
    {
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        j.AddField("boneName", boneName);
        j.AddField("changeTime", changeTime);
        j.AddField("toScale", JSONTemplates.FromVector3(toScale));
        j.AddField("fromScale", JSONTemplates.FromVector3(fromScale));
        return j.Print(false);
    }

    public override void Deseralize(string param)
    {
        if (param == "") return;

        JSONObject j = new JSONObject(param);
        boneName = j.GetField("boneName").str;
        changeTime = j.GetField("changeTime").f;
        toScale = JSONTemplates.ToVector3(j.GetField("toScale"));
        fromScale = JSONTemplates.ToVector3(j.GetField("fromScale"));
    }

    public override void InitParamControl()
    {
        base.InitParamControl();

        ParamControlList<string> BoneControl = new ParamControlList<string>("Bone");
        BoneControl.valueGetter = delegate()
        {
            int index = listBoneNames.FindIndex(a => a == boneName);
            index = index == -1 ? 0 : index;
            return index;
        };
        BoneControl.valueSetter = delegate(int i) { boneName = listBoneNames[i]; };
        BoneControl.setContentList(listBoneNames);
        if(listBoneNames.Count > 0)
        {
            BoneControl.valueSetter(0);
        }
        AddControl(BoneControl);

        ParamControlPrimitive<float> TimeControl = new ParamControlPrimitive<float>("ChangeTime");
        TimeControl.valueGetter = delegate() { return changeTime; };
        TimeControl.valueSetter = delegate(float b) { changeTime = b; };
        AddControl(TimeControl);

        ParamControlVector3 fromScaleControl = new ParamControlVector3("fromScale");
        fromScaleControl.valueGetter = delegate() { return fromScale; };
        fromScaleControl.valueSetter = delegate(Vector3 val) { fromScale = val; };
        AddControl(fromScaleControl);

        ParamControlVector3 toScaleControl = new ParamControlVector3("toScale");
        toScaleControl.valueGetter = delegate() { return toScale; };
        toScaleControl.valueSetter = delegate(Vector3 val) { toScale = val; };
        AddControl(toScaleControl);

    }

    private void buildBones()
    {
        Transform t = RefGameObject.transform.FindChild("root");

        if (t == null)
        {
            throw new ToolException("Doesn't have root bone");
        }

        listBoneNames.Add(t.gameObject.name);

        Utils.IterateChildrenUtil.IterateChildren(t.gameObject, delegate(GameObject go) { listBoneNames.Add(go.name); return true; }, true);
    }

}