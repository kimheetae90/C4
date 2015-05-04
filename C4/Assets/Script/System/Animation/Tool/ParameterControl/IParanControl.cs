using UnityEngine;
using System.Collections;

public interface IParanControl
{
    void Show(int width, int height);
    void ShowEditingWindow(int width, int height);
    bool IsEditing();
}