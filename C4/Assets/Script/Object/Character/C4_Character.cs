using UnityEngine;
using System.Collections;

public abstract class C4_Character : C4_Object
{
    [System.NonSerialized]
    public bool canMove;
    [System.NonSerialized]
    public bool canShot;
    [System.NonSerialized]
    
    protected C4_BoatFeature boatFeature;
    protected C4_StraightMove moveComponent;
    protected C4_Turn turnComponent;
    protected C4_IntShot shotComponent;

    public virtual void Start()
    {
        moveComponent = GetComponent<C4_StraightMove>();
        turnComponent = GetComponentInChildren<C4_Turn>();
        shotComponent = GetComponent<C4_IntShot>();
        boatFeature = GetComponent<C4_BoatFeature>();
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
        if (boatFeature.stackCount >= 1)
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
        if (boatFeature.stackCount >= 2)
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


    public bool damaged(int damage)
    {
        boatFeature.hp -= damage;
        return checkHP();
    }

    protected abstract bool checkHP();
}
