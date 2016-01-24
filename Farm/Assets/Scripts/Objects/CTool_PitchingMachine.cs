using UnityEngine;
using System.Collections;

public class CTool_PitchingMachine : CTool {

    ParticleSystem particle;

    void Start()
    {

        particle = GetComponentInChildren<ParticleSystem>();
        base.Start();

    }


    protected override void ToolShot()
    {
        base.ToolShot();
        particle.Play();
        Shoot();
    }
}
