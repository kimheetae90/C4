using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AimUI : MonoBehaviour {

    public Image aimUIImage;
    GameObject aimUIGameObejct;

    void Start()
    {
        aimUIGameObejct = aimUIImage.gameObject;
        aimUIGameObejct.SetActive(false);
    }

    public void showUI(Vector3 clickPosition)
    {
        aimUIGameObejct.SetActive(true);
        C4_Player selectedBoat = C4_ManagerMaster.Instance.sceneMode.getController(GameObjectType.Player).GetComponent<C4_PlayerController>().selectedBoat;
        float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;

        aimUIImage.transform.position = selectedBoat.transform.position;
        aimUIImage.transform.rotation = Quaternion.LookRotation(-aimDirection);
        aimUIImage.transform.Rotate(Vector3.right, 90);
        aimUIImage.transform.localScale = new Vector3(1, distance / 2, 1);
    }

    public void hideUI()
    {
        aimUIGameObejct.SetActive(false);
    }
}
