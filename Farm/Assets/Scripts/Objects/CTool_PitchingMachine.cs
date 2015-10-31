using UnityEngine;
using System.Collections;

public class CTool_PitchingMachine : CTool {

    protected override void ToolShot()
    {
        base.ToolShot();
        Shoot();
    }
}
