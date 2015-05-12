using UnityEngine;
using System.Collections;

public class C4_StraightShot : MonoBehaviour, C4_IntShot {

    C4_UnitFeature unitFeature;
    C4_Missile missile;
    GameObject missileGameObejct;
	C4_MissileFeature missileFeature;

    Vector3 shotDirection;
    Vector3 missileToMove;

	// Use this for initialization
	void Start () {
        unitFeature = GetComponent<C4_UnitFeature>();
        missileGameObejct = unitFeature.missile;
        missile = missileGameObejct.GetComponent<C4_Missile>();
		missileFeature = missileGameObejct.GetComponent<C4_MissileFeature> ();
	}

    public void startShot(Vector3 targetPos)
    {
		missileGameObejct.transform.position = missileFeature.startPosition.position;
		shotDirection = (transform.position - targetPos).normalized;
		missileGameObejct.transform.GetChild(0).transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
		missile.startMove (targetPos);

    }
}
