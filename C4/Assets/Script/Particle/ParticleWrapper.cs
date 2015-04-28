using UnityEngine;
using System.Collections;

public class ParticleWrapper : MonoBehaviour {
    
    public float fLifeTime = 0.0f;

    Transform followObject;
    Transform thisTransForm;
    bool bFollowObject;

    void Awake()
    {
        followObject = null;
        bFollowObject = false;
        followObject = null;
        thisTransForm = this.transform;
        Destroy(gameObject, fLifeTime);
    }
	
	// Update is called once per frame
	void Update () {
        if(bFollowObject == true && followObject != null)
        {
            thisTransForm.position = followObject.position;
        }
	}

    public void setFollowObject(Transform targetFollowObject)
    {
        followObject = targetFollowObject;
        bFollowObject = true;
    }
}
