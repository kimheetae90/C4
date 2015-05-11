using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

#if UNITY_EDITOR
public class AnimEventParamFactory
{
    static List<string> animEventList;

    static AnimEventParamFactory()
    {
        animEventList = new List<string>();
        animEventList.Add("CreateParticle");
        animEventList.Add("ChangeMaterial");
        animEventList.Add("ChangeScale");
		animEventList.Add("ChangeTexture");
    }

    public static AnimEventParamBase CreateParam(string eventName,AnimationEvent animEvent,GameObject gameObject)
    {
        AnimEventParamBase animEventParam = null;

        switch(eventName)
        {
            case "CreateParticle":
                {
                    animEventParam = new AnimEventParamCreateParticle(animEvent, gameObject);
                }
                break;
            case "ChangeMaterial":
                {
                    animEventParam = new AnimEventChangeMaterial(animEvent, gameObject);
                }
                break;
            case "ChangeScale":
                {
                    animEventParam = new AnimEventChangeScale(animEvent, gameObject);
                }
                break;
			case "ChangeTexture":
				{
					animEventParam = new AnimEventChangeTexture(animEvent, gameObject);
				}
			break;
        }

        return animEventParam;
    }

    public static List<string> getEventList()
    {
        return animEventList;
    }

}
#endif