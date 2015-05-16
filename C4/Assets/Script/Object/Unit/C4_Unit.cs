using UnityEngine;
using System.Collections;

public abstract class C4_Unit : C4_Object
{
	[System.NonSerialized]
	public bool canActive;

	protected C4_UnitFeature unitFeature;
	protected C4_StraightMove moveComponent;
	protected C4_Turn turnComponent;
	protected C4_IntShot shotComponent;
	protected C4_ControllUnitMove moveControlComponent;

	[System.NonSerialized]
	public Vector3 aimPosition;

	protected override void Awake()
	{
		base.Awake();
		moveControlComponent = GetComponent<C4_ControllUnitMove>();
		moveComponent = GetComponent<C4_StraightMove>();
		turnComponent = GetComponentInChildren<C4_Turn>();
		shotComponent = GetComponent<C4_IntShot>();
		unitFeature = GetComponent<C4_UnitFeature>();

        setupAnimationEvent();
	}
	
	void Update()
	{
		checkActive();
	}
	
	protected virtual void checkActive()
	{
		if (unitFeature.gage >= unitFeature.fullGage)
		{
			canActive = true;
		}
		else
		{
			canActive = false;
		}
	}
	
	public void shot(Vector3 targetPos)
	{
		if (canActive)
		{
			unitFeature.activeDone();
			shotComponent.startShot(targetPos);
			if(isType(GameObjectType.Enemy))
				checkAvoid (targetPos);
		}
	}

	public void checkAvoid(Vector3 targetPos)
	{
		GameObject colObject = transform.Find ("avoidCheckCollider").gameObject;
		colObject.SetActive (true);
		
		BoxCollider col = colObject.GetComponent<BoxCollider>();
		Vector3 colDirection = (targetPos - this.transform.position).normalized;
		colDirection.y = 0;
		
		float distance = Vector3.Distance (this.transform.position, targetPos);
		col.size = new Vector3(1,1,distance);
		col.center = new Vector3(col.size.x, col.size.y+1, col.size.z/2.0f);
		col.transform.rotation = Quaternion.LookRotation(colDirection);
	}

	public void move(Vector3 toMove)
	{
		moveControlComponent.startCheckGageAndControlMove();
		moveComponent.toMove = toMove;
		if (canActive)
		{
			unitFeature.activeDone();
			moveComponent.startMove(toMove);
		}
	}
	
	public void moveNoCondition(Vector3 toMove)
	{
		moveControlComponent.startCheckGageAndControlMove();
		moveComponent.toMove = toMove;
		unitFeature.StopUpdate (1.5f);
		moveComponent.startMove(toMove);
	}
	
	public void turn(Vector3 toMove)
	{
		turnComponent.setToTurn(toMove);
	}
	
	
	public void damaged(int damage)
	{
		unitFeature.hp -= damage;
		checkHP();
        //수정바람
		//GetComponent<C4_AnimControl> ().damaged ();
        
        transform.GetComponent<C4_UnitFeature>().rageUp(unitFeature.rageGageChargeInDamage);
	}
	
	protected abstract void checkHP();

    private void setupAnimationEvent()
    {
        Animator animator = GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.transform.gameObject.AddComponent<CustomAnimationEvent>();
        }
    }

    public virtual void onUserEvent(AnimEventUserMsg msg)
    {

    }
}