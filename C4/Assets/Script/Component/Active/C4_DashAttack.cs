using UnityEngine;
using System.Collections;

public class C4_DashAttack : MonoBehaviour, C4_IntShot
{

    C4_UnitFeature unitFeature;
    C4_Unit unit;
    C4_StraightMove move;
    C4_Missile missile;
    GameObject missileGameObejct;

    Vector3 shotDirection;
    Vector3 missileToMove;

    // Use this for initialization
    void Start()
    {
        unitFeature = GetComponent<C4_UnitFeature>();
        unit = GetComponent<C4_Unit>();
        move = GetComponent<C4_StraightMove>();
        missileGameObejct = unitFeature.missile;
        missile = missileGameObejct.GetComponent<C4_Missile>();
       
    }

    public void startShot(Vector3 targetPos)
    {
        Vector3 missilePos = transform.position + (targetPos - transform.position).normalized * (transform.localScale.z + missileGameObejct.transform.GetChild(0).localScale.z + 1);
        //missilePos.y = 2;
        missileGameObejct.transform.position = missilePos;
        //shotDirection = (transform.position - targetPos).normalized;
        //this.gameObject.transform.GetChild(0).rotation = Quaternion.LookRotation(shotDirection);
        //unit.moveNoCondition(targetPos);
        //unit.move(targetPos);
        //missile.gameObject.SetActive(true);
        //missile.gameObject.transform.SetParent(this.gameObject.transform);
        //unit.turn(-shotDirection);
        move.startMove(targetPos);
        //missileGameObejct.transform.GetChild(0).transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
        missile.startMove(targetPos);
        unitFeature.activeDone();

    }
}
