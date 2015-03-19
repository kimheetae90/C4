﻿using UnityEngine;
using System.Collections;

public class C4_StraightShot : MonoBehaviour, C4_IntShot {

    C4_BoatFeature boatFeature;
    C4_Missile missile;
    GameObject missileGameObejct;

    Vector3 shotDirection;
    Vector3 missileToMove;

	// Use this for initialization
	void Start () {
        boatFeature = GetComponent<C4_BoatFeature>();
        missileGameObejct = boatFeature.missile;
        missile = missileGameObejct.GetComponent<C4_Missile>();
	}

    public void startShot(Vector3 click)
    {
        missileGameObejct.SetActive(true);
        missileGameObejct.transform.position = transform.position + (transform.position - click).normalized * (transform.localScale.z + missileGameObejct.transform.localScale.z + 1);
        shotDirection = (transform.position - click).normalized;
        missileToMove = 4 * transform.position - 3 * click;
        shotDirection.y = 0;
        missileToMove.y = 0;
        missileGameObejct.transform.GetChild(0).transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
        missile.startMove(missileToMove);
        boatFeature.gageDown(boatFeature.needGageStackToShot);
    }
}
