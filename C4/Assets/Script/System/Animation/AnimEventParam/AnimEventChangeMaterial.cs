using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimEventChangeMaterial : AnimEventParamBase
{
    public string materialName;
    public string changeObjectName;

#if UNITY_EDITOR
    List<string> listRendererNames;
#endif

#if UNITY_EDITOR
    public AnimEventChangeMaterial(AnimationEvent animEvent, GameObject gameObject)
        : base(animEvent, gameObject)
    {
        listRendererNames = new List<string>();
        buildChild();
        InitParamControl();
        clear();
        
    }
#endif
    public AnimEventChangeMaterial()
        : base()
    {
        clear();
    }

    private void clear()
    {
        materialName = "";
        changeObjectName = "";
    }

    public override string Serialize()
    {
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        j.AddField("materialName", materialName);
        j.AddField("changeObjectName", changeObjectName);
        return j.Print(false);
    }

    public override void Deseralize(string param)
    {
        if (param == "") return;

        JSONObject j = new JSONObject(param);
        materialName = JSONSafeGetter.getString("materialName", j);
        changeObjectName = JSONSafeGetter.getString("changeObjectName", j);
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

        ParamControlPrimitive<string> materialControl = new ParamControlPrimitive<string>("material");
        materialControl.valueGetter = delegate() { return materialName; };
        materialControl.valueSetter = delegate(string str) { materialName = str; };
        AddControl(materialControl);
    }


    private void buildChild()
    {
        listRendererNames.Clear();

        Utils.IterateChildrenUtil.IterateChildren(RefGameObject.gameObject,
            delegate(GameObject go) 
            {
                if(go.GetComponent<SkinnedMeshRenderer>() != null)
                {
                    listRendererNames.Add(go.name);
                }

                return true;
            }, true);
    }
#endif
}