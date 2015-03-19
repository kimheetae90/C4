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
    [System.NonSerialized]
    public GameObject aimImage;


    // Use this for initialization
    void Start()
    {
        showUI = false;
        boatFeature = transform.GetComponent<C4_BoatFeature>();
        aimImage = GameObject.Find("Aim");
    }

    public void checkIsSelected()
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
        }
    }

    public void showAimUI(Vector3 clickPosition)
    {
        checkIsSelected();
        if (showUI)
        {
            aimImage.SetActive(true);
            float distance = Vector3.Distance(selectedBoat.transform.position, clickPosition);
            Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
            aimDirection.y = 0;

            aimUI.transform.position = selectedBoat.transform.position;
            aimUI.transform.rotation = Quaternion.LookRotation(-aimDirection);
            aimUI.transform.Rotate(Vector3.right, 90);
            aimUI.transform.localScale = new Vector3(1, distance/2, 1);
            
        }
        else
            hideAimUI();
        
    }

    public void hideAimUI()
    {
        aimImage.SetActive(false);
    }
}
