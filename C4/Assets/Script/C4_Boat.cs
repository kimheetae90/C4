using UnityEngine;
using System.Collections;

public class C4_Boat : C4_Object
{
    public GameObject missile;
    public Move moveScript;
    public Turn turnScript;
    public C4_Missile missileFeature;
    public GameObject toMoveObject;
    public int fullHP;
    public int fullGage;


    [System.NonSerialized]
    public int gage;
    public int hp;
    public bool isReady;

    Vector3 missileToMove;
    Vector3 shotDirection;

    void Start()
    {
        gage = 0;
        hp = fullHP;
        isReady = false;
    }

    void Update()
    {
        gageUp();
    }

    public void shot(Vector3 click)
    {
        missile.transform.position = transform.position + (transform.position - click).normalized * (transform.localScale.z + missile.transform.localScale.z + 1);
        shotDirection = (transform.position - click).normalized;
        missileToMove = 4 * transform.position - 3 * click;
        shotDirection.y = 0;
        missileToMove.y = 0;
        toMoveObject.transform.position = missileToMove;
        missile.GetComponentInChildren<Turn>().transform.gameObject.transform.rotation = Quaternion.LookRotation(shotDirection);
        missileFeature.startMove(missileToMove);
        resetActive();
    }

    public void startMove(Vector3 toMove)
    {
        moveScript.setToMove(toMove);
        resetActive();
    }

    public void startTurn(Vector3 toMove)
    {
        turnScript.setToTurn(toMove);
    }

    public void damaged(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            if (transform.CompareTag("enemy"))
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    void gageUp()
    {
        if (gage == fullGage)
        {
            isReady = true;
            return;
        }

        if (gage < fullGage)
        {
            gage++;
        }
    }

    public void resetActive()
    {
        gage = 0;
        isReady = false;
    }
}
