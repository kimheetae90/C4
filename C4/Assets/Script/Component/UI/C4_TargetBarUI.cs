using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_TargetBarUI : C4_TargetUI
{
    public override void showUI(Vector3 targetPos)
    {
        targetUIImage.gameObject.SetActive(true);
        float distance = Vector3.Distance(transform.position, targetPos);
		Vector3 direction = targetPos - transform.position;

        targetUIImage.gameObject.transform.rotation = Quaternion.LookRotation(direction);
        targetUIImage.gameObject.transform.Rotate(Vector3.right, 90);
        targetUIImage.gameObject.transform.localScale = new Vector3(1, distance / targetUIImage.rectTransform.rect.height, 1);
    }

}