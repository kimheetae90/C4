using UnityEngine;
using System.Collections;

public class C4_Boat : C4_Object
{
    public int moveSpeed;
    public int fullHP;
    public int fullGage;
    public int power;

    [System.NonSerialized]
    public int gage;
    public int hp;
    public bool isActive;

    void Start()
    {
        gage = 0;
        hp = fullHP;
        isActive = false;
    }

    void Update()
    {
        gageUp();
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
            isActive = true;
            return;
        }

        if (gage < fullGage)
        {
            gage++;
        }
    }

    void resetActive()
    {
        gage = 0;
        isActive = false;
    }
}
