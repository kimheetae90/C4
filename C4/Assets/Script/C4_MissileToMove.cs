using UnityEngine;
using System.Collections;

public class C4_MissileToMove : MonoBehaviour {

    public GameObject rootObject;

    void Start()
    {
        rootObject = transform.root.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("missile"))
        {
            if (rootObject == other.transform.root.gameObject)
            {
                transform.position = rootObject.transform.position;
            }
        }
    }
}
