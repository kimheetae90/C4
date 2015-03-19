using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_ArrowUI : MonoBehaviour {

    public Image aimUI;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;
    [System.NonSerialized]
    public bool showUI;
    [System.NonSerialized]
    public GameObject selectedBoat;
    // Use this for initialization
    void Start()
    {
        showUI = false;
        boatFeature = transform.GetComponent<C4_BoatFeature>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIsSelected();
    }

    void checkIsSelected()
    {

        selectedBoat = C4_PlayManager.Instance.selectedBoat.gameObject;
        if (selectedBoat != null)
        {
            if (C4_PlayManager.Instance.isAim)
                showUI = true;
        }
        else
        {
            showUI = false;
            hideAimUI();
        }
    }

    void showAimUI(Vector3 clickPosition)
    {
        if (showUI)
        {
            float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
            Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
            aimDirection.y = 0;

            aimUI.transform.position = selectedBoat.transform.position;
            aimUI.transform.rotation = Quaternion.LookRotation(-aimDirection);
            aimUI.transform.Rotate(Vector3.right, 90);
            aimUI.transform.localScale = new Vector3(1, distance, 1);

            aimUI.fillAmount = 1;
        }
    }

    void hideAimUI()
    {
        aimUI.fillAmount = 0;
    }
}
