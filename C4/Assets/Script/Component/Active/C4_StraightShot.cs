using UnityEngine;
using System.Collections;

public class C4_StraightShot : MonoBehaviour, C4_IntShot {

    C4_BoatFeature boatFeature;
    C4_MissileFeature missileFeature;
    GameObject missile;

    Vector3 shotDirection;
    Vector3 missileToMove;

	// Use this for initialization
	void Start () {
        boatFeature = GetComponent<C4_BoatFeature>();
        missile = boatFeature.missile;
        missileFeature = missile.GetComponent<C4_MissileFeature>();
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
