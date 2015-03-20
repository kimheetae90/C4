using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AimUI : MonoBehaviour {

    public Image aimUI;
    [System.NonSerialized]
    public C4_Player selectedBoat;
    public GameObject aimImage;

    void Start()
    {
        aimImage.SetActive(false);
    }

    public void selectBoat(C4_Player inputSelectedBoat)
    {
        selectedBoat = inputSelectedBoat;
    }

    public void showAimUI(Vector3 clickPosition)
    {
        aimImage.SetActive(true);
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
