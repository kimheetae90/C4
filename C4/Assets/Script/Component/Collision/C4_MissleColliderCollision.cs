using UnityEngine;
using System.Collections;

public class C4_MissleColliderCollision : C4_MissileCollision
{
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
        power = transform.GetComponentInParent<C4_MissileFeature>().splashpower;
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
                
                C4_WaterParkMissleCollision misslecol = transform.GetComponentInParent<C4_MissileFeature>().transform.GetChild(0).transform.GetComponent<C4_WaterParkMissleCollision>();
                if (missleFeature.unit.GetComponent<C4_UnitFeature>().israge)
                {
                    Debug.Log("ragemode 포를 쏜당");
                    /* 레이지 모드. lister 추가하고 사용해야함
                    C4_ListenStatusAilment listen = other.GetComponentInParent<C4_ListenStatusAilment>();
                    statusAilment.time = stuntime;
                    listen.AddtoList(statusAilment);
                     */
                    missleFeature.unit.GetComponent<C4_UnitFeature>().israge = false;
                }
                if (unit != null && isfirst)
                {
                    unit.rageUp(unit.GetComponent<C4_UnitFeature>().rageGageChargeInAttack);
                    isfirst = false;

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
