﻿using UnityEngine;
using System.Collections;

public abstract class C4_Unit : C4_Object
{
    [System.NonSerialized]
    public bool canMove;
    [System.NonSerialized]
    public bool canShot;
    
    protected C4_UnitFeature unitFeature;
    protected C4_StraightMove moveComponent;
    protected C4_Turn turnComponent;
    protected C4_DistanceCheck distCheckComponent;
    protected C4_IntShot shotComponent;

    protected override void Start()
    {
        base.Start();
        moveComponent = GetComponent<C4_StraightMove>();
        turnComponent = GetComponentInChildren<C4_Turn>();
        shotComponent = GetComponent<C4_IntShot>();
        distCheckComponent = GetComponent<C4_DistanceCheck>();
        unitFeature = GetComponent<C4_UnitFeature>();
    }

    void Update()
    {
        checkActiveAndStack();
    }

    void checkActiveAndStack()
    {
        checkCanMove();
        checkCanShot();
    }

    void checkCanMove()
    {
        if (unitFeature.stackCount >= 1)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    void checkCanShot()
    {
        if (unitFeature.stackCount >= 2)
        {
            canShot = true;
        }
        else
        {
            canShot = false;
        }
    }

    public void shot(Vector3 click)
    {
        if (canShot)
        {
            shotComponent.startShot(click);
        }
    }

    public void move(Vector3 toMove)
    {
        if (canMove)
        {
            distCheckComponent.distCheck();
            moveComponent.setMoving(toMove);
        }
    }

    public void turn(Vector3 toMove)
    {
        if (canMove)
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