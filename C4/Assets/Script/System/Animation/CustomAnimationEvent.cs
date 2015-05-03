using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomAnimationEvent : MonoBehaviour
{
    // Use this for initialization
    Dictionary<string, Transform> dicBones;

    void Start()
    {
        dicBones = new Dictionary<string, Transform>();

		Transform t = null;

		Utils.IterateChildrenUtil.IterateChildren(this.gameObject, delegate(GameObject go) { 
		
			if(go.name == "root")
			{
				t = go.transform;
				return false;
			}

			return true;

		}, true);

        if (t == null)
        {
            Debug.LogError("Bone Object Not Found");
        }

        dicBones.Add(t.name, t);

		Utils.IterateChildrenUtil.IterateChildren(t.gameObject, delegate(GameObject go) { dicBones.Add(go.name, go.transform); return true;}, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void CreateParticle(string strParam)
    {
        CustomAnimationEventParam eventParam = CustomAnimationEventUtil.buildParam(strParam);

        GameObject ps = null;

        if (eventParam.res == "") return;

        GameObject o = (GameObject)Resources.Load(eventParam.res, typeof(GameObject));

        ps = Instantiate(o, this.transform.position, Quaternion.identity) as GameObject;

        Transform bone = dicBones[eventParam.boneName];

        if (bone != null)
        {
            if(eventParam.followBone)
            {
                ps.transform.position = bone.position;

                ParticleWrapper pw = ps.GetComponent<ParticleWrapper>();
       
                if(pw)
                {
                    pw.setFollowObject(bone);
                }
            }
            else
            {
                ps.transform.position = bone.position;
            }
        }
    }

    protected virtual void EventMessage(string strParam)
    {

    }

    protected virtual void PlaySound(string strParam)
    {

    }

    protected virtual void CreateColision(string strParam)
    {

    }
}