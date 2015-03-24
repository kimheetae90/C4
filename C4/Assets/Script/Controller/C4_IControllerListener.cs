using UnityEngine;
using System.Collections;

public interface C4_IControllerListener
{
    void getMessage(string msg);
    void getMessage(string msg, int val);
    void getMessage(string msg, float val);
    void getMessage(string msg, Vector3 vec);
}
