using UnityEngine;
using System.Collections;

public class C4_MissleColliderCollision : MonoBehaviour {

    
    public bool isfirst;
    C4_MissileFeature missleFeature;
    Vector3 misslepos;

    public float stuntime;
    stAilment statusAilment;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        missleFeature = GetComponentInParent<C4_MissileFeature>();
        isfirst = true;

        stAilment stun = new Stun();
        statusAilment = stun;
	}
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 8)
        {
            return;
        }
        C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
        switch (collisionObject.objectAttr.type)
        {   
            case GameObjectType.Ally:
            case GameObjectType.Enemy:
                C4_UnitFeature unit = GetComponentInParent<C4_MissileFeature>().unit.GetComponent<C4_UnitFeature>();
                if (unit != null&&isfirst)
                {
                    unit.rageUpAtt();
                    isfirst = false;
             
                }

                if (missleFeature.unit.GetComponent<C4_UnitFeature>().israge)
                {
                    //C4_ListenStatusAilment listen = other.GetComponentInParent<C4_ListenStatusAilment>();
                    //statusAilment.time = stuntime;
                   // listen.AddtoList(statusAilment);
                    missleFeature.unit.GetComponent<C4_UnitFeature>().israge = false;
                }
                C4_StraightMove move = collisionObject.GetComponent<C4_StraightMove>();
                Vector3 unitpos = collisionObject.transform.position;
                misslepos.y = 0;
                Vector3 tomove = misslepos + ((unitpos - misslepos).normalized)*transform.localScale.x;
                move.startMove(tomove);
                break;

        }
    }
    public void checkpoint(Vector3 missle)
    {
        misslepos = missle;
    }
	
}
