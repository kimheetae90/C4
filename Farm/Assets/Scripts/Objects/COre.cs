using UnityEngine;
using System.Collections;

public class COre : CTerrain {

    public bool canHeld;
    GameObject player;
    void Start() {

        canHeld = false;
    }
	// Use this for initialization
    protected override void Complete()
    {
        canHeld = true;
    }

    protected override void UpdateState()
    {
        if (canHeld == false)
        {
            transform.position = player.transform.position + new Vector3(2.0f, 0, 0);
        }

    }

}
