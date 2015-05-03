using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_TargetBarUI : C4_TargetUI
{
    public override void showUI(Vector3 targetPos)
    {
        targetUIImage.SetActive(true);
        float distance = Vector3.Distance(transform.position, targetPos);
		Vector3 direction = targetPos - transform.position;

        targetUIImage.transform.rotation = Quaternion.LookRotation(direction);
        targetUIImage.transform.Rotate(Vector3.right, 90);
        targetUIImage.transform.GetChild(0).localPosition = new Vector3(0, distance*0.52f, 0);
        targetUIImage.transform.GetChild(0).localScale = new Vector3(15, distance*0.28f, 1);
    }

}