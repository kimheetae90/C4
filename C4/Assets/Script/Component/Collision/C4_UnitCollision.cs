using UnityEngine;
using System.Collections;

public class C4_UnitCollision : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
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
			C4_MissileFeature missileFeature = collisionObject.GetComponent<C4_MissileFeature>();
			unit.damaged(missileFeature.power);
			break;
		}
	}
	
}