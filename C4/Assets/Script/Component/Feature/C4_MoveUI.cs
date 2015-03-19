using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour {

    public Image[] moveRangeUI;
    [System.NonSerialized]
    public bool showUI;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;
    [System.NonSerialized]
    public GameObject selectedBoat;
    [System.NonSerialized]
    public GameObject[] moveImage;
    void Start () {
        showUI = false;
        moveImage = new GameObject[3];
        moveImage[0] = GameObject.Find("MoveRange1");
        moveImage[1] = GameObject.Find("MoveRange2");
        moveImage[2] = GameObject.Find("MoveRange3");
        
            Debug.Log(moveImage[0].transform.root.name);
        
    }
	
	// Update is called once per frame
	void Update () {
        //checkIsSelected();
        showMoveUI();
    }

    public void checkIsSelected()
    {
        selectedBoat = C4_PlayManager.Instance.selectedBoat.gameObject;

        if (selectedBoat != null)
            showUI = true;
        else
        {
            showUI = false;
        }
    }

    public void showMoveUI()
    {
        checkIsSelected();
        if (showUI)
        {
            boatFeature = selectedBoat.GetComponent<C4_BoatFeature>();
            for (int i = 0; i < moveRangeUI.Length; i++)
            {
                moveRangeUI[i].transform.position = selectedBoat.transform.position;
                moveRangeUI[i].transform.localScale = new Vector3(boatFeature.moveRange * (i + 1) , (boatFeature.moveRange * (i + 1)) , 1);
            }

            switch (boatFeature.stackCount)
            {
                case 0:
                    moveImage[0].SetActive(false);
                    moveImage[1].SetActive(false);
                    moveImage[2].SetActive(false);
                    break;
                case 1:
                    moveImage[0].SetActive(true);
                    moveImage[1].SetActive(false);
                    moveImage[2].SetActive(false);
                    break;
                case 2:
                    moveImage[0].SetActive(true);
                    moveImage[1].SetActive(true);
                    moveImage[2].SetActive(false);
                    break;
                case 3:
                    moveImage[0].SetActive(true);
                    moveImage[1].SetActive(true);
                    moveImage[2].SetActive(true);
                    break;
            }
        }
        else
            hideMoveUI();
    }
    public void hideMoveUI()
    {
        for (int i = 0; i < moveRangeUI.Length; i++)
        {
            moveImage[i].SetActive(false);
        }
    }

}
