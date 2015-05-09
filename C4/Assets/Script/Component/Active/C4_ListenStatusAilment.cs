using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_ListenStatusAilment : MonoBehaviour {

    List<stAilment> listeners;
    int check;

    public virtual void Awake()
    {
        listeners = new List<stAilment>();
        check = 0;
    }
	// Use this for initialization
	
    public void AddtoList(stAilment type)
    {
       
        check = checkType(listeners, type);
        if(check!=0){
            
            switch(type.GetType().ToString()){
                case "Stun":
                    if (listeners[check - 1].time <= type.time)
                    {
                        listeners[check - 1].time = type.time;
                    }
                    
                    break;

            }

        }
        else
        {
            listeners.Add(type);
            StartCoroutine("execute");
        }

        
        
    }
    int checkType(List<stAilment> listeners, stAilment type)
    {
        for (int i = 0; i < listeners.Count; i++)
        {

            if (listeners[i].GetType() == type.GetType())
            {
                return i+1;
            }
        }
        return 0;
    }

    IEnumerator execute()
    {
        

        for (int i = listeners.Count; i >0; i--)
        {
            listeners[i-1].execute(this.gameObject);
            
            if (listeners[i-1].time <= 0)
            {
                listeners.Remove(listeners[i-1]);
            }
            
        }
        yield return new WaitForSeconds(0.1f);

        if (listeners.Count == 0)
        {
            StopCoroutine("execute");
        }
        else
        {
            StartCoroutine("execute");
        }

        

    }
}
