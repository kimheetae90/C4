using UnityEngine;
using System.Collections;

public class ParticleWrapper : MonoBehaviour {
    
    ParticleSystem particleSystem;
	// Use this for initialization
    public float fLifeTime = 0.0f;

    Transform followObject;
    Transform thisTransForm;
    bool bFollowObject;

    void Awake()
    {
        followObject = null;
        bFollowObject = false;
        followObject = null;
        particleSystem = GetComponent<ParticleSystem>();
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
