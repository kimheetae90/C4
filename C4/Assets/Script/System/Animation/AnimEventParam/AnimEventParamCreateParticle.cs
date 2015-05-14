using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AnimEventParamCreateParticle : AnimEventParamBase
{

    public string boneName;
    public string resName;
    public bool followBone;
    public Vector3 offset;
    public float scale;
    public float lifetime;
    public float elapsedTime;
#if UNITY_EDITOR
    List<string> listBoneNames;
#endif

#if UNITY_EDITOR
    public AnimEventParamCreateParticle(AnimationEvent animEvent, GameObject gameObject)
        : base(animEvent, gameObject)
    {
        listBoneNames = new List<string>();
        buildBones();
        InitParamControl();
        clear();
    }
#endif

    public AnimEventParamCreateParticle()
        : base()
    {
        clear();
    }

    private void clear()
    {
        resName = "";
        boneName = "";
        offset = Vector3.zero;
        scale = 1.0f;
        elapsedTime = 0.0f;
        lifetime = 0.0f;
    }

    public override string Serialize()
    {
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        j.AddField("boneName", boneName);
        j.AddField("resName", resName);
        j.AddField("followBone", followBone);
        j.AddField("lifetime", lifetime);
        j.AddField("offset", JSONTemplates.FromVector3(offset));
        j.AddField("scale", scale);
        return j.Print(false);
    }

    public override void Deseralize(string param)
    {
        if (param == "") return;

        JSONObject j = new JSONObject(param);

        if (j == null) clear();

        boneName = JSONSafeGetter.getString("boneName", j);
        resName = JSONSafeGetter.getString("resName", j);
        followBone = JSONSafeGetter.getBool("followBone", j);
        lifetime = JSONSafeGetter.getFloat("lifetime", j);
        offset = JSONSafeGetter.getVector3("offset", j);
        scale = JSONSafeGetter.getFloat("scale", j);
    }

#if UNITY_EDITOR
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
        if (listBoneNames.Count > 0)
        {
            BoneControl.valueSetter(0);
        }
        AddControl(BoneControl);

        ParamControlPrimitive<bool> followBoneControl = new ParamControlPrimitive<bool>("followBone");
        followBoneControl.valueGetter = delegate() { return followBone; };
        followBoneControl.valueSetter = delegate(bool b) { followBone = b; };
        AddControl(followBoneControl);

        ParamControlPrimitive<float> lifeTimeControl = new ParamControlPrimitive<float>("lifeTime");
        lifeTimeControl.valueGetter = delegate() { return lifetime; };
        lifeTimeControl.valueSetter = delegate(float f) { lifetime = f; };
        AddControl(lifeTimeControl);

        ParamControlPrimitive<string> resControl = new ParamControlPrimitive<string>("res");
        resControl.valueGetter = delegate() { return resName; };
        resControl.valueSetter = delegate(string str) { resName = str; };
        AddControl(resControl);

        ParamControlPrimitive<float> sizeControl = new ParamControlPrimitive<float>("size");
        sizeControl.valueGetter = delegate() { return scale; };
        sizeControl.valueSetter = delegate(float val) { scale = val; };
        AddControl(sizeControl);

        ParamControlVector3 offsetControl = new ParamControlVector3("offset");
        offsetControl.valueGetter = delegate() { return offset; };
        offsetControl.valueSetter = delegate(Vector3 val) { offset = val; };
        AddControl(offsetControl);
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