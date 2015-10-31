using UnityEngine;
using System.Collections;

public class CAttackRange : MonoBehaviour
{

    protected BoxCollider attackRangeCollider;
    public float attackRange;
    // Use this for initialization

    protected void Start()
    {
        attackRangeCollider = GetComponent<BoxCollider>();
    }

}
