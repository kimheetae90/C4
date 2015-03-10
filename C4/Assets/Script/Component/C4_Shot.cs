using UnityEngine;
using System.Collections;

public class C4_Shot : MonoBehaviour {

    C4_Boat boatFeature;
    C4_Missile missileFeature;
    GameObject missile;

    Vector3 shotDirection;
    Vector3 missileToMove;
	// Use this for initialization
	void Start () {
        boatFeature = GetComponent<C4_Boat>();
        missile = boatFeature.missile;
        missileFeature = missile.GetComponent<C4_Missile>();
	}

    public void startShot(Vector3 click)
    {
        missile.transform.position = transform.position + (transform.position - click).normalized * (transform.localScale.z + missile.transform.localScale.z + 1);
        shotDirection = (transform.position - click).normalized;
        missileToMove = 4 * transform.position - 3 * click;
        shotDirection.y = 0;
        missileToMove.y = 0;
        missile.transform.GetChild(0).transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
        missileFeature.startMove(missileToMove);
        boatFeature.gageDown(boatFeature.needGageStackToShot);
    }
}
