using UnityEngine;
using System.Collections.Generic;

public class ParamControlList<T> : ParamControlBase
{
    int editingIndex;

    ComboBox cb;
    GUIContent[] cbContents;

    GUIStyle listStyle = new GUIStyle();

    List<T> contentList;
    
    public delegate void ValueSetter(int idex);
    public ValueSetter valueSetter;

    public delegate int ValueGetter();
    public ValueGetter valueGetter;

    public ParamControlList(string controlName)
        : base(controlName)
    {
        editingIndex = 0;

        cb = new ComboBox();

        listStyle.normal.textColor = Color.white;
        listStyle.onHover.background =
        listStyle.hover.background = new Texture2D(2, 2);
        listStyle.padding.left =
        listStyle.padding.right =
        listStyle.padding.top =
        listStyle.padding.bottom = 2;
    }

    public override void Show(int width, int height)
    {
        GUILayout.BeginHorizontal("");

        GUILayout.Label(controlName + " : ", GUILayout.Width(width / 5));

        int index = valueGetter();

        if (GUILayout.Button(contentList[index].ToString(), GUILayout.Width(width / 5 * 3)))
        {
            bEditing = !bEditing;
            editingIndex = valueGetter();
        }

        GUILayout.EndHorizontal();
    }


    public override void ShowEditingWindow(int width, int height)
    {
        editingIndex = cb.List(new Rect(width / 10, height / 8, width / 5 * 4, height / 5), contentList[editingIndex].ToString(), cbContents, listStyle);

        if (GUI.Button(new Rect(width / 10, 150, width / 5 * 4, height / 5), "OK"))
        {
            bEditing = false;

            valueSetter(editingIndex);
        }
    }

    public void setContentList(List<T> contentList)
    {
        this.contentList = contentList;

        cbContents = new GUIContent[contentList.Count];

        for (int i = 0; i < contentList.Count; ++i)
        {
            cbContents[i] = new GUIContent(contentList[i].ToString());
        }
    }
}