﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour {

    public Image[] moveRangeUI;
    [System.NonSerialized]
    public GameObject[] moveImage;
    bool isSelect;
    C4_BoatFeature boatFeature;
    C4_Player selectedBoat;
    void Start()
    {
        moveImage = new GameObject[3];
        moveImage[0] = GameObject.Find("MoveRange1");
        moveImage[1] = GameObject.Find("MoveRange2");
        moveImage[2] = GameObject.Find("MoveRange3");
        isSelect = false;
    }

    void Update()
    {
        if (isSelect)
        {
            showMoveUI();
        }
    }

    public void selectBoat(C4_Player inputSelectedBoat)
    {
        isSelect = true;
        selectedBoat = inputSelectedBoat;
        boatFeature = selectedBoat.GetComponent<C4_BoatFeature>();
    }

    public void showMoveUI()
    {
        for (int i = 0; i < moveRangeUI.Length; i++)
        {
            moveRangeUI[i].transform.position = selectedBoat.transform.position;
            moveRangeUI[i].transform.localScale = new Vector3(boatFeature.moveRange * (i + 1), (boatFeature.moveRange * (i + 1)), 1);
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
