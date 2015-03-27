using UnityEngine;
using System.Collections;

public interface C4_IControllerListener
{
    void onEvent(string message, params object[] p);
}