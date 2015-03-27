using UnityEngine;
using System.Collections.Generic;

public class C4_Controller : MonoBehaviour {

    public List<C4_IControllerListener> listeners;

    public virtual void Start()
    {
        listeners = new List<C4_IControllerListener>();
    }

    public virtual void selectClickObject(GameObject clickGameObject) { }
    public virtual void dispatchData(InputData inputData) { }

    public void addListener(C4_IControllerListener listener)
    {
        listeners.Add(listener);
    }

    public void removeListener(C4_IControllerListener listener)
    {
        listeners.Remove(listener);
    }

    public void notifyEvent(string message, params object[] p)
    {
        for(int i = 0 ; i < listeners.Count ; ++i)
        {
            listeners[i].onEvent(message, p);
        }
    }
}
