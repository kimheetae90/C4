using UnityEngine;
using System;
using System.Collections;

public class ParamControlPrimitive<T> : ParamControlBase
{
    string strValue;

    public delegate void ValueSetter(T str);
    public ValueSetter valueSetter;

    public delegate T ValueGetter();
    public ValueGetter valueGetter;
    
    public ParamControlPrimitive(string controlName)
        : base(controlName)
    {

    }

    public override void Show(int width, int height)
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label(controlName+" : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(valueGetter().ToString(), GUILayout.Width(width / 5 * 3)))
        {
            bEditing = !bEditing;

            strValue = valueGetter().ToString();
        }

        GUILayout.EndHorizontal();
    }

    public override void ShowEditingWindow(int width,int height)
    {
        GUILayout.Label(controlName + " : ", GUILayout.Width(width / 5));

        strValue = GUILayout.TextField(strValue, GUILayout.Width(width / 5 * 4));

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            Type typeT = typeof(T);

            valueSetter((T)Convert.ChangeType(strValue, typeT));

            bEditing = false;
        }
    }


}