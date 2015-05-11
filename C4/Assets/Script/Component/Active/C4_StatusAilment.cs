using UnityEngine;
using System.Collections;

public class C4_StatusAilment : MonoBehaviour
{

    public StatusAilmentType type;
    public float time;
	stAilment statusAilment;

	void Start()
    {
        
		switch (type) 
		{
		case StatusAilmentType.Stun:
            stAilment stun = new Stun();
			statusAilment = stun;
			break;

		}
	}

	void OnTriggerEnter(Collider other)
	{
        C4_ListenStatusAilment listen = other.GetComponentInParent<C4_ListenStatusAilment>();
        statusAilment.time = time;
        listen.AddtoList(statusAilment);
        
	}
}

