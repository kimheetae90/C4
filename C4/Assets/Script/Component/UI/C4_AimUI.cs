using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AimUI : MonoBehaviour {

    Image aimUI;
    GameObject aimImage;

    void Start()
    {
        aimUI = GetComponentInChildren<Image>();
        aimImage = aimUI.gameObject;
        aimImage.SetActive(false);
    }

    public void showAimUI(Vector3 clickPosition)
    {
        aimImage.SetActive(true);
        C4_Player selectedBoat = C4_ManagerMaster.Instance.sceneMode.getController(GameObjectType.Player).GetComponent<C4_PlayerController>().selectedBoat;
        float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;

        aimUI.transform.position = selectedBoat.transform.position;
        aimUI.transform.rotation = Quaternion.LookRotation(-aimDirection);
        aimUI.transform.Rotate(Vector3.right, 90);
        aimUI.transform.localScale = new Vector3(1, distance / 2, 1);
    }

    public void hideAimUI()
    {
        aimImage.SetActive(false);
    }
}
