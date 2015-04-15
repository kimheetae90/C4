using UnityEngine;
using System.Collections;

public class C4_AutomoveCancleUI : MonoBehaviour {

    C4_Unit unit;
    C4_UnitFeature unitFeature;
    C4_Move moveScript;
    C4_AutomoveCancelButton cancelbt;

	// Use this for initialization
	void Start () {
        unit = this.GetComponentInParent<C4_Unit>();
        unitFeature = this.GetComponentInParent<C4_UnitFeature>();
        moveScript = this.GetComponentInParent<C4_Move>();
        cancelbt = this.GetComponentInChildren<C4_AutomoveCancelButton>();
	}
	
	// Update is called once per frame
    
    public void startAutomoveCancleUI()
    {
        if (unit.canActive == false && Vector3.Distance(moveScript.toMove, unit.transform.position) > 0)
        {
            cancelbt.showButton();
        }
        if (Vector3.Distance(moveScript.toMove, unit.transform.position) > unitFeature.moveRange)
        {
            cancelbt.showButton();
        }
        StartCoroutine("automoveCancleUI");
    }
    IEnumerator automoveCancleUI()
    {
        yield return null;

        
        if (Vector3.Distance(moveScript.toMove, unit.transform.position) < unitFeature.moveSpeed * 0.02f)
        {
            cancelbt.hideButton();
            StopCoroutine("automoveCancleUI");
        }

        StartCoroutine("automoveCancleUI");
    }
}
