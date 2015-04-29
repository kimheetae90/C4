using UnityEngine;
using System.Collections;

public class C4_StraightShot : MonoBehaviour, C4_IntShot {

    C4_UnitFeature unitFeature;
    C4_Missile missile;
    GameObject missileGameObejct;

    Vector3 shotDirection;
    Vector3 missileToMove;

	// Use this for initialization
	void Start () {
        unitFeature = GetComponent<C4_UnitFeature>();
        missileGameObejct = unitFeature.missile;
        missile = missileGameObejct.GetComponent<C4_Missile>();
	}

    public void startShot(Vector3 targetPos)
    {
		Vector3 missilePos = transform.position + (targetPos - transform.position).normalized * (transform.localScale.z + missileGameObejct.transform.GetChild(0).GetChild(0).localScale.z + 1);
		missilePos.y = 0;
		missileGameObejct.transform.position = missilePos;
		shotDirection = (transform.position - targetPos).normalized;
		missileGameObejct.transform.GetChild(0).transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
		missile.startMove (targetPos);

    }
}
