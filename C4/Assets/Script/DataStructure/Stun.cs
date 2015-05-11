using UnityEngine;
using System.Collections;

public class Stun : stAilment
{
    
    // Use this for initialization
    public override void execute(GameObject unit)
    {
        
        unit.GetComponent<C4_Unit>().canActive = false;
        time -= 0.1f;
        if (time <= 0)
        {
            if (unit.GetComponent<C4_UnitFeature>().gage == unit.GetComponent<C4_UnitFeature>().fullGage)
            {
                unit.GetComponent<C4_Unit>().canActive = true;
            }
            
        }
        
    }
   
}
