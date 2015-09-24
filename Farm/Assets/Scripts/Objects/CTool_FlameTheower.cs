using UnityEngine;
using System.Collections;

public class CTool_FlameTheower : CTool {

    ParticleSystem particle;

    void Start() {

        particle = GetComponentInChildren<ParticleSystem>();
        base.Start();
        
    }

    public override void Reset() {

        base.Reset();
        particle.Stop();
    }

    protected override void ToolReady()
    {
        base.ToolReady(); 
        particle.Stop();
    }

    protected override void ToolMove()
    {
        base.ToolMove();
        particle.Stop();

    }

    protected override void ToolReadyToShot()
    {
        base.ToolReadyToShot();
        particle.Stop();

    }

   
    protected override void ToolShot()
    {
        base.ToolShot();
        particle.Play();
    }

   
    protected override void ToolDie()
    {
        base.ToolDie();
        particle.Stop();
    }

}
