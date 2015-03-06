using UnityEngine;
using System.Collections;

public class C4_MissileColider : MonoBehaviour {

    public GameObject missile;
    public GameObject rootObject;

    Move moveScript;

    Vector3 toMove;

    void Start()
    {
        moveScript = missile.GetComponent<Move>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject == rootObject)
        {
            toMove = rootObject.transform.position;
            toMove.y = -15;
            missile.transform.position = toMove;
            moveScript.isMove = false;
        }
    }

}
