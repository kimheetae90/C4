using UnityEngine;
using System.Collections;

public class JSONSafeGetter {

    public static string getString(string name,JSONObject obj)
    {
        if (obj == null) return "";

        if (obj.HasField(name) == false) return "";

        return obj.GetField(name).str;
    }

    public static float getFloat(string name, JSONObject obj)
    {
        if (obj == null) return 0.0f;

        if (obj.HasField(name) == false) return 0.0f;

        return obj.GetField(name).f;
    }

    public static int getInt(string name, JSONObject obj)
    {
        if (obj == null) return 0;

        if (obj.HasField(name) == false) return 0;

        return (int)obj.GetField(name).n;
    }

    public static bool getBool(string name, JSONObject obj)
    {
        if (obj == null) return false;

        if (obj.HasField(name) == false) return false;

        return obj.GetField(name).b;
    }

    public static Vector3 getVector3(string name, JSONObject obj)
    {
        if (obj == null) return Vector3.zero;

        if (obj.HasField(name) == false) return Vector3.zero;

        return JSONTemplates.ToVector3(obj.GetField(name));
    }
}
