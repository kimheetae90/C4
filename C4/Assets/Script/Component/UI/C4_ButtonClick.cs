using UnityEngine;
using System.Collections;

public class C4_ButtonClick : MonoBehaviour {


    public GameObject myCharacter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void selectAlly()
    {

        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.selectClickObject(myCharacter);
    }

    public void movetoselect()
    {
        Camera.main.GetComponentInParent<C4_PlaySceneCamera>().moveToSomeObject();
        //Camera.main.gameObject.GetComponent<C4_PlaySceneCamera>().moveToSomeObject();
        //C4_GameManager.Instance.GetComponentInChildren<C4_PlaySceneCamera>().moveToSomeObject();
    }
}
