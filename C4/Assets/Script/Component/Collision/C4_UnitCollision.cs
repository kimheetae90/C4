using UnityEngine;
using System.Collections;

public class C4_UnitCollision : MonoBehaviour {

	bool canAvoid;

	void start(){
		canAvoid = false;
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.layer == 9){
			canAvoid = true;
			return;
		}
		if (other.gameObject.layer == 8)
		{
			return;
		}

		C4_Object collisionObject = other.GetComponentInParent<C4_Object>();
		C4_ControllUnitMove controllUnitMove = GetComponentInParent<C4_ControllUnitMove>();
		C4_Unit unit = GetComponentInParent<C4_Unit>();


		switch(collisionObject.objectAttr.type)
		{
		case GameObjectType.Ally:
		case GameObjectType.Enemy:
			controllUnitMove.stopCompletely();
			//this
			unit.GetComponent<C4_Unit>().moveNoCondition(unit.transform.position
			                                             + (unit.transform.position - collisionObject.transform.position).normalized * 8);
			collisionObject.GetComponent<C4_Unit>().moveNoCondition(unit.transform.position
			                                                        + (collisionObject.transform.position - unit.transform.position).normalized * 8);
			break;
		case GameObjectType.Missile:

            Debug.Log(other.gameObject.ToString());
                //C4_MissileFeature missileFeature = collisionObject.GetComponent<C4_MissileFeature>();
            C4_MissileCollision misslecol = other.gameObject.GetComponent<C4_MissileCollision>();
            unit.damaged(misslecol.power);
			break;
		}
	}

	void OnTriggerExit(Collider other){
		if(canAvoid){
			if (other.gameObject.layer == 9){
				//reward
				canAvoid = false;
				return;
			}
		}
	}

	void reward(){

	}
}