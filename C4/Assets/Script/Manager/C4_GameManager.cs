using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_GameManager : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        C4_ObjectManager.Instance.initInstance();
        C4_PlayManager.Instance.initInstance();
        C4_InputManager.Instance.initInstance();
	}
}
