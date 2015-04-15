using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_AutomoveCancleUI : MonoBehaviour {

    C4_Unit unit;
    C4_UnitFeature unitFeature;
    C4_Move moveScript;
    public Button cancelbt;

	// Use this for initialization
	void Start () {
        unit = this.GetComponentInParent<C4_Unit>();
        unitFeature = this.GetComponentInParent<C4_UnitFeature>();
        moveScript = this.GetComponentInParent<C4_Move>();
        hideUI();
        
	}
	
	// Update is called once per frame
    
    public void startAutomoveCancleUI()
    {
        if (unit.canActive == false && Vector3.Distance(moveScript.toMove, unit.transform.position) > 0)
        {
            cancelbt.gameObject.SetActive(true);
        }
        if (Vector3.Distance(moveScript.toMove, unit.transform.position) > unitFeature.moveRange)
        {
            cancelbt.gameObject.SetActive(true);
        }
        StartCoroutine("automoveCancleUI");
    }
    IEnumerator automoveCancleUI()
    {
        yield return null;


        if (Vector3.Distance(moveScript.toMove, unit.transform.position) < unitFeature.moveSpeed * 0.02f)
        {
            hideUI();
            StopCoroutine("automoveCancleUI");
        }

        StartCoroutine("automoveCancleUI");
    }

    public void hideUI()
    {
        cancelbt.gameObject.SetActive(false);
    }
}
