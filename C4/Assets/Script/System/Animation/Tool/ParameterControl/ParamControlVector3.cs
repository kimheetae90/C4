using UnityEngine;
using System;
using System.Collections;

public class ParamControlVector3 : ParamControlBase
{
    string X;
    string Y;
    string Z;

    public delegate void ValueSetter(Vector3 str);
    public ValueSetter valueSetter;

    public delegate Vector3 ValueGetter();
    public ValueGetter valueGetter;

    public ParamControlVector3(string controlName)
        : base(controlName)
    {

    }

    public override void Show(int width, int height)
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label(controlName + " : ", GUILayout.Width(width / 5));

        if (GUILayout.Button(valueGetter().ToString(), GUILayout.Width(width / 5 * 3)))
        {
            bEditing = !bEditing;

            X = valueGetter().x.ToString();
            Y = valueGetter().y.ToString();
            Z = valueGetter().z.ToString();
        }

        GUILayout.EndHorizontal();
    }

    public override void ShowEditingWindow(int width, int height)
    {
        GUILayout.Label(controlName + " : ", GUILayout.Width(width / 5));

        X = GUILayout.TextField(X, GUILayout.Width(width / 5 * 4));
        Y = GUILayout.TextField(Y, GUILayout.Width(width / 5 * 4));
        Z = GUILayout.TextField(Z, GUILayout.Width(width / 5 * 4));

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {

            Vector3 value;

            value.x = Convert.ToSingle(X);
            value.y = Convert.ToSingle(Y);
            value.z = Convert.ToSingle(Z);

            valueSetter(value);

            bEditing = false;
        }
    }
}