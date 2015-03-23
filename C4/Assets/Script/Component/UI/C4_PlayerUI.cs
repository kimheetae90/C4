using UnityEngine;
using System.Collections;

public class C4_PlayerUI : MonoBehaviour {

    public C4_AimUI aimUI;
    public C4_MoveUI moveUI;
    public C4_SelectUI selectUI;

    public void aiming(Vector3 clickPosition)
    {
        aimUI.showAimUI(clickPosition);
    }

    public void startAim()
    {
        moveUI.gameObject.SetActive(false);
    }

    public void select()
    {
        moveUI.selectBoat();
    }

    public void activeDone()
    {
        aimUI.gameObject.SetActive(false);
        moveUI.gameObject.SetActive(false);
        selectUI.gameObject.SetActive(false);
    }
}
