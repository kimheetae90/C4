using UnityEngine;
using System.Collections;

public class CustomAnimationEventParam
{
    public string boneName;
    public Vector3 scale;
    public string res;
    public string msg;
    public bool followBone;

    public CustomAnimationEventParam()
    {
        scale = new Vector3(1, 1, 1);
        msg = "";
        res = "";
        followBone = false;
    }
}