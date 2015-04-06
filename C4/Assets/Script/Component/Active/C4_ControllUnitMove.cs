using UnityEngine;
using System.Collections;

public class C4_ControllUnitMove : MonoBehaviour {

    C4_UnitFeature unitFeature;
    C4_StraightMove move;

    void Start()
    {
        unitFeature = GetComponent<C4_UnitFeature>();
        move = GetComponent<C4_StraightMove>();
    }

    IEnumerator checkGageAndControllMove()
    {
        yield return null;
        if()
    }
}
