using UnityEngine;
using System;
using System.Collections;

public enum eCustomEventUtilParamType
{
    eCustomEventUtilParamType_NONE = -1,
    eCustomEventUtilParamType_BONE = 0,
    eCustomEventUtilParamType_RES = 1,
    eCustomEventUtilParamType_SCALE = 2,
    eCustomEventUtilParamType_FOLLOW_BONE = 3,
    eCustomEventUtilParamType_MSG = 4,
    eCustomEventUtilParamType_COUNT,
}

public class CustomAnimationEventUtil
{
    static public CustomAnimationEventParam buildParam(AnimationEvent animEvent)
    {
        return buildParam(animEvent.stringParameter);
    }

    static public CustomAnimationEventParam buildParam(string strParam)
    {
        CustomAnimationEventParam param = new CustomAnimationEventParam();

        string[] strParams = strParam.Split('@');

        for (int i = 0; i < strParams.Length; ++i)
        {
            eCustomEventUtilParamType type = (eCustomEventUtilParamType)i;
            switch (type)
            {
                case eCustomEventUtilParamType.eCustomEventUtilParamType_BONE:
                    param.boneName = strParams[i];
                    break;
                case eCustomEventUtilParamType.eCustomEventUtilParamType_RES:
                    param.res = strParams[i];
                    break;
                case eCustomEventUtilParamType.eCustomEventUtilParamType_SCALE:
                    param.scale = GetVectorFromString(strParams[i]);
                    break;
                case eCustomEventUtilParamType.eCustomEventUtilParamType_FOLLOW_BONE:
                    param.followBone = Convert.ToBoolean(strParams[i]);
                    break;
                case eCustomEventUtilParamType.eCustomEventUtilParamType_MSG:
                    param.msg = strParams[i];
                    break;
            }
        }
        return param;
    }

    static public string buildString(CustomAnimationEventParam animParam)
    {
        string str = "";

        str = animParam.boneName + "@" + animParam.res + "@" + animParam.scale.ToString("F3") + "@" + animParam.followBone.ToString() + "@" + animParam.msg;

        return str;
    }

    static public string GetCustomEventParma(eCustomEventUtilParamType type, string param)
    {
        string[] strParams = param.Split('@');

        if ((int)type > strParams.Length || strParams.Length <= 0) return "";

        return strParams[(int)type];
    }

    static public Vector3 GetVectorFromString(string param)
    {
        string[] temp = param.Substring(1, param.Length - 2).Split(',');
        float x = float.Parse(temp[0]);
        float y = float.Parse(temp[1]);
        float z = float.Parse(temp[2]);
        Vector3 rValue = new Vector3(x, y, z);
        return rValue;
    }
}