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

    protected override void Start()
    {
        base.Start();
        moveControlComponent = GetComponent<C4_ControllUnitMove>();
        moveComponent = GetComponent<C4_StraightMove>();
        turnComponent = GetComponentInChildren<C4_Turn>();
        shotComponent = GetComponent<C4_IntShot>();
        unitFeature = GetComponent<C4_UnitFeature>();
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

    public void shot(Vector3 click)
    {
        if (canActive)
        {
            unitFeature.activeDone();
            shotComponent.startShot(click);
        }
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

    public void turn(Vector3 toMove)
    {
        if (canActive)
        {
            turnComponent.setToTurn(toMove);
        }
    }


    public void damaged(int damage)
    {
        unitFeature.hp -= damage;
        checkHP();
    }

	protected abstract void checkHP();
}
