using UnityEngine;
using System.Collections;

public class C4_PlayerUI : MonoBehaviour, C4_IControllerListener
{
    C4_AimUI aimUI;
    C4_MoveUI moveUI;
    C4_SelectUI selectUI;

    void Start()
    {
        aimUI = GetComponent<C4_AimUI>();
        moveUI = GetComponent<C4_MoveUI>();
        selectUI = GetComponent<C4_SelectUI>();
    }

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
       // aimUI.gameObject.SetActive(false);
       // moveUI.gameObject.SetActive(false);
       // selectUI.gameObject.SetActive(false);
    }

    public void moveToSelectedPlayer()
    {
        transform.position = C4_ManagerMaster.Instance.sceneMode.getController(GameObjectType.Player).GetComponent<C4_PlayerController>().selectedBoat.transform.position;
    }

    public void onEvent(string message, params object[] p)
    {
        switch (message)
        {
            case "ActiveDone":
                {
                    activeDone();
                }
                break;
            case "StartAim":
                {
                    startAim();
                }
                break;
            case "Aming":
                {
                    Vector3 pos = (Vector3)p[0];
                    aiming(pos);
                }
                break;
            case "Select":
                {
                    Vector3 pos = (Vector3)p[0];
                    transform.position = pos;
                    select();
                    moveToSelectedPlayer();
                }
                break;
        }
    }
}