using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour {

    public Image[] moveRangeUI;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;
    [System.NonSerialized]
    public bool showUI;
    [System.NonSerialized]
    public GameObject selectedBoat;
    void Start () {
        boatFeature = transform.GetComponent<C4_BoatFeature>();
        
    }
	
	// Update is called once per frame
	void Update () {
        checkIsSelected();
        showMoveUI();
    }

    void checkIsSelected()
    {
        selectedBoat = C4_PlayManager.Instance.selectedBoat.gameObject;

        if (selectedBoat != null)
            showUI = true;
        else
            showUI = false;
    }

    void showMoveUI()
    {
        if (showUI)
        {
            for (int i = 0; i < moveRangeUI.Length; i++)
            {
                moveRangeUI[i].transform.position = selectedBoat.transform.position;
                moveRangeUI[i].transform.localScale = new Vector3(boatFeature.moveRange * (i + 1) , (boatFeature.moveRange * (i + 1)) , 1);
            }

            switch (boatFeature.stackCount)
            {
                case 0:
                    moveRangeUI[0].fillAmount = 0;
                    moveRangeUI[1].fillAmount = 0;
                    moveRangeUI[2].fillAmount = 0;
                    break;
                case 1:
                    moveRangeUI[0].fillAmount = 1;
                    moveRangeUI[1].fillAmount = 0;
                    moveRangeUI[2].fillAmount = 0;
                    break;
                case 2:
                    moveRangeUI[0].fillAmount = 1;
                    moveRangeUI[1].fillAmount = 1;
                    moveRangeUI[2].fillAmount = 0;
                    break;
                case 3:
                    moveRangeUI[0].fillAmount = 1;
                    moveRangeUI[1].fillAmount = 1;
                    moveRangeUI[2].fillAmount = 1;
                    break;
            }
        }
        else
            hideMoveUI();
    }
    void hideMoveUI()
    {
        for (int i = 0; i < moveRangeUI.Length; i++)
        {
            moveRangeUI[i].fillAmount = 0;
        }
    }

}
