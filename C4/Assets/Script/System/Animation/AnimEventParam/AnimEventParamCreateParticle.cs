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
    public Vector3 scale;
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
        scale = Vector3.zero;
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
        j.AddField("scale", JSONTemplates.FromVector3(scale));
        return j.Print(false);
    }

    public override void Deseralize(string param)
    {
        if (param == "") return;

        JSONObject j = new JSONObject(param);
        boneName = j.GetField("boneName").str;
        resName = j.GetField("resName").str;
        followBone = j.GetField("followBone").b;
        lifetime = j.GetField("lifetime").f;
        offset = JSONTemplates.ToVector3(j.GetField("offset"));
        scale = JSONTemplates.ToVector3(j.GetField("scale"));
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
        if(listBoneNames.Count > 0)
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

        ParamControlVector3 sizeControl = new ParamControlVector3("size");
        sizeControl.valueGetter = delegate() { return scale; };
        sizeControl.valueSetter = delegate(Vector3 val) { scale = val; };
        AddControl(sizeControl);

        ParamControlVector3 offsetControl = new ParamControlVector3("offset");
        offsetControl.valueGetter = delegate() { return offset; };
        offsetControl.valueSetter = delegate(Vector3 val) { offset = val; };
        AddControl(offsetControl);
    }

    private void buildBones()
    {
        listBoneNames.Clear();


        Transform root = null;

        Utils.IterateChildrenUtil.IterateChildren(RefGameObject, delegate(GameObject go)
        {
            Transform t = go.transform.FindChild("root");

            if (t != null)
            {
                root = t;

                listBoneNames.Add(t.gameObject.name);

                return false;
            }

          
            return true; 
        
        }, true);


        if (root != null)
        {
            Utils.IterateChildrenUtil.IterateChildren(root.gameObject, delegate(GameObject go) { listBoneNames.Add(go.name); return true; }, true);
        }
    }

#endif
}