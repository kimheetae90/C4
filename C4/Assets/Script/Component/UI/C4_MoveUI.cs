using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour {

    public Image[] moveRangeUI;
    [System.NonSerialized]
    public GameObject[] moveImage;
    bool isSelect;
    void Start()
    {
        moveImage = new GameObject[3];
        moveImage[0] = GameObject.Find("MoveRange1");
        moveImage[1] = GameObject.Find("MoveRange2");
        moveImage[2] = GameObject.Find("MoveRange3");
        isSelect = false;
        moveImage[0].SetActive(false);
        moveImage[1].SetActive(false);
        moveImage[2].SetActive(false);
    }

    void Update()
    {
        if (isSelect)
        {
            showMoveUI();
        }
    }

    public void selectBoat()
    {
        isSelect = true;
    }

    public void showMoveUI()
    {
        C4_Player selectedBoat = C4_ManagerMaster.Instance.sceneManager.getController(GameObjectType.Player).GetComponent<C4_PlayerController>().selectedBoat;
        C4_BoatFeature boatFeature = selectedBoat.GetComponent<C4_BoatFeature>();

        for (int i = 0; i < moveRangeUI.Length; i++)
        {
            moveRangeUI[i].transform.position = selectedBoat.transform.position;
            moveRangeUI[i].transform.localScale = new Vector3(boatFeature.moveRange * (i + 1)/2, (boatFeature.moveRange * (i + 1)/2), 1);
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

    public void hideMoveUI()
    {
        isSelect = false;
        moveImage[0].SetActive(false);
        moveImage[1].SetActive(false);
        moveImage[2].SetActive(false);
    }
}
