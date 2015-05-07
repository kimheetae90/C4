using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomAnimationEvent : MonoBehaviour
{
    // Use this for initialization
    Dictionary<string, Transform> dicChildObject;
    Transform myTransform;

    void Awake()
    {
        dicChildObject = new Dictionary<string, Transform>();
        myTransform = this.transform;
        Utils.IterateChildrenUtil.IterateChildren(this.gameObject, delegate(GameObject go) { dicChildObject.Add(go.name, go.transform); return true; }, true);
    }

    protected virtual void CreateParticle(string strParam)
    {
        AnimEventParamCreateParticle param = new AnimEventParamCreateParticle();
        param.Deseralize(strParam);
        
        GameObject ps = null;

        if (param.resName == "") return;

        GameObject o = (GameObject)Resources.Load(param.resName, typeof(GameObject));

        ps = Instantiate(o, this.transform.position, Quaternion.identity) as GameObject;

        Transform bone = dicChildObject[param.boneName];

        if (bone != null)
        {
            ps.transform.position = bone.position;

            StartCoroutine(PlayParticle(param, ps));
        }
    }

    IEnumerator PlayParticle(AnimEventParamCreateParticle param, GameObject ps)
    {
        while(param.elapsedTime < param.lifetime)
        {
            ps.transform.position = dicChildObject[param.boneName].position;
            param.elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(ps);

        yield return null;
    }    

    protected virtual void ChangeMaterial(string strParam)
    {
        AnimEventChangeMaterial param = new AnimEventChangeMaterial();

        param.Deseralize(strParam);

        if (param.materialName == "") return;

        Material mat = Resources.Load(param.materialName, typeof(Material)) as Material;

        Transform changeObject = dicChildObject[param.changeObjectName];

        if(mat != null && changeObject !=null)
        {
           SkinnedMeshRenderer renderer = changeObject.GetComponent<SkinnedMeshRenderer>();

           if (renderer == null) return;

           renderer.material = mat;
        }
    }

    protected virtual void ChangeScale(string strParam)
    {
        AnimEventChangeScale param = new AnimEventChangeScale();

        param.Deseralize(strParam);

        if (param.boneName == "") return;

        Transform changeObject = dicChildObject[param.boneName];

        if (changeObject != null)
        {
            changeObject.localScale = param.fromScale;
            StartCoroutine(ScaleAnimation(param,changeObject));
        }
    }

    protected virtual void ChangeTexture(string strParam)
    {
        AnimEventChangeTexture param = new AnimEventChangeTexture();

        param.Deseralize(strParam);

        if (param.textureName == "") return;

        Texture tex = Resources.Load(param.textureName, typeof(Texture)) as Texture;

        Transform changeObject = dicChildObject[param.changeObjectName];

        if (tex != null && changeObject != null)
        {
            SkinnedMeshRenderer renderer = changeObject.GetComponent<SkinnedMeshRenderer>();

            if (renderer == null) return;

            renderer.material.SetTexture(param.nameId,tex);
        }
    }


    IEnumerator ScaleAnimation(AnimEventChangeScale param, Transform changeObject)
    {
        while (changeObject.localScale != param.toScale)
        {
            if(param.changeTime == 0.0f)
            {
                changeObject.localScale = param.toScale;
            }
            else
            {
                changeObject.localScale = Vector3.Slerp(param.fromScale, param.toScale, param.elapsedTime / param.changeTime);
            }

            param.elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;
    }    
}