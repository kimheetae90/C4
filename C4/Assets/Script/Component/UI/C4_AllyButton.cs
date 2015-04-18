using UnityEngine;
using System.Collections;

public class C4_AllyButton : MonoBehaviour {

    public GameObject myCharacter;
	// Use this for initialization
    public void selectAlly()
    {
        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.selectClickObject(myCharacter);
    }

    public void movetoselect()
    {
        Camera.main.GetComponentInParent<C4_PlaySceneCamera>().moveToSomeObject();
    }
}
