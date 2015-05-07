using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimEventChangeTexture : AnimEventParamBase
{
    public string textureName;
    public string changeObjectName;
    public int nameId;
#if UNITY_EDITOR
    List<string> listRendererNames;
#endif

#if UNITY_EDITOR
    public AnimEventChangeTexture(AnimationEvent animEvent, GameObject gameObject)
        : base(animEvent, gameObject)
    {
        listRendererNames = new List<string>();
        buildChild();
        InitParamControl();
        clear();
        
    }
#endif
    public AnimEventChangeTexture()
        : base()
    {
        clear();
    }

    private void clear()
    {
        textureName = "";
        changeObjectName = "";
        nameId = 0;
    }

    public override string Serialize()
    {
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        j.AddField("textureName", textureName);
        j.AddField("changeObjectName", changeObjectName);
        j.AddField("nameId", nameId);
        return j.Print(false);
    }

    public override void Deseralize(string param)
    {
        if (param == "") return;

        JSONObject j = new JSONObject(param);
        textureName = j.GetField("textureName").str;
        changeObjectName = j.GetField("changeObjectName").str;
        nameId = (int)j.GetField("nameId").n;
    }

#if UNITY_EDITOR
    public override void InitParamControl()
    {
        base.InitParamControl();

        ParamControlList<string> rendererControl = new ParamControlList<string>("renderer");
        rendererControl.valueGetter = delegate()
        {
            int index = listRendererNames.FindIndex(a => a == changeObjectName);
            index = index == -1 ? 0 : index;
            return index;
        };
        rendererControl.valueSetter = delegate(int i) { changeObjectName = listRendererNames[i]; };
        rendererControl.setContentList(listRendererNames);
        if(listRendererNames.Count > 0)
        {
            rendererControl.valueSetter(0);
        }
        AddControl(rendererControl);

        ParamControlPrimitive<string> textureControl = new ParamControlPrimitive<string>("texture");
        textureControl.valueGetter = delegate() { return textureName; };
        textureControl.valueSetter = delegate(string str) { textureName = str; };
        AddControl(textureControl);

		ParamControlPrimitive<int> nameIdControl = new ParamControlPrimitive<int>("nameId");
        nameIdControl.valueGetter = delegate() { return nameId; };
        nameIdControl.valueSetter = delegate(int i) { nameId = i; };
        AddControl(nameIdControl);
    }

    private void buildChild()
    {
        listRendererNames.Clear();

        Utils.IterateChildrenUtil.IterateChildren(RefGameObject.gameObject,
            delegate(GameObject go)
            {
                if (go.GetComponent<SkinnedMeshRenderer>() != null)
                {
                    listRendererNames.Add(go.name);
                }

                return true;
            }, true);
    }
#endif

}
