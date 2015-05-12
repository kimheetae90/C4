using UnityEngine;
using System.Collections;

public class C4_DashAttack : MonoBehaviour, C4_IntShot
{
    C4_UnitFeature unitFeature;
    C4_StraightMove move;
    C4_Missile missile;
    GameObject missileGameObejct;
	C4_MissileFeature missileFeature;

    Vector3 shotDirection;
    Vector3 missileToMove;

    // Use this for initialization
    void Start()
    {
        unitFeature = GetComponent<C4_UnitFeature>();
        move = GetComponent<C4_StraightMove>();
        missileGameObejct = unitFeature.missile;
        missile = missileGameObejct.GetComponent<C4_Missile>();
		missileFeature = missile.GetComponent<C4_MissileFeature> ();
       
    }
    public void startShot(Vector3 targetPos)
	{
		missileGameObejct.transform.position = missileFeature.startPosition;
        move.startMove(targetPos);
        missile.startMove(targetPos);
        unitFeature.activeDone();

    }
}
